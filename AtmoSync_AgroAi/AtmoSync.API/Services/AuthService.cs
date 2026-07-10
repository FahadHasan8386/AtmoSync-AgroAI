using AtmoSync.API.Interfaces.IRepositories;
using AtmoSync.API.Interfaces.IServices;
using AtmoSync.API.Model;
using AtmoSync.Shared;
using AtmoSync.Shared.Models.DtoModels;
using System.Globalization;
using System.Transactions;

namespace AtmoSync.API.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly JwtService _jwtService;

        public AuthService(IUserRepository userRepository,JwtService jwtService)
        {
            _userRepository = userRepository;
            _jwtService = jwtService;
        }

        public async Task<ResponseModel<string>> RegisterAsync(RegisterDto dto)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(dto.FullName))
                {
                    return new ResponseModel<string>
                    {
                        Code = StatusCodes.Status400BadRequest,
                        Message = "Full Name is required."
                    };
                }

                if (string.IsNullOrWhiteSpace(dto.Email))
                {
                    return new ResponseModel<string>
                    {
                        Code = StatusCodes.Status400BadRequest,
                        Message = "Email is required."
                    };
                }

                if (string.IsNullOrWhiteSpace(dto.Password))
                {
                    return new ResponseModel<string>
                    {
                        Code = StatusCodes.Status400BadRequest,
                        Message = "Password is required."
                    };
                }

                var existingUser =await _userRepository.GetByEmailAsync(dto.Email);

                if (existingUser != null)
                {
                    return new ResponseModel<string>
                    {
                        Code = StatusCodes.Status409Conflict,
                        Message = "Email already exists."
                    };
                }

                long result;

                using (TransactionScope transactionScope = new(TransactionScopeAsyncFlowOption.Enabled))
                {
                    User user = new()
                    {
                        FullName = dto.FullName,
                        Email = dto.Email,
                        PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password),
                        Role = "User",
                        CreatedAt = DateTime.Now,
                        CreatedBy = "AtmoSync Agro Ai",
                        InActive = false
                    };

                    result = await _userRepository.CreateAsync(user);

                    transactionScope.Complete();
                }

                if (result > 0)
                {
                    return new ResponseModel<string>
                    {
                        Code = StatusCodes.Status201Created,
                        Message = "Registration successful."
                    };
                }

                return new ResponseModel<string>
                {
                    Code = StatusCodes.Status400BadRequest,
                    Message = "Registration failed."
                };
            }
            catch (Exception ex)
            {
                return new ResponseModel<string>
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Message = ex.Message
                };
            }
        }

        public async Task<ResponseModel<LoginResponseDto>> LoginAsync(LoginDto dto)
        {
            try
            {
                var user = await _userRepository.GetByEmailAsync(dto.Email);

                if(user == null)
                {
                    return new ResponseModel<LoginResponseDto>
                    {
                        Code = 401,
                        Message = "Invalid Email or Password"
                    };
                }

                bool passwordValid = BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash);

                if(!passwordValid)
                {
                    return new ResponseModel<LoginResponseDto>
                    {
                        Code = 401,
                        Message = "Invalid Password"
                    };
                }

                if(user.InActive)
                {
                    return new ResponseModel<LoginResponseDto>
                    {
                        Code = 403,
                        Message = "Account InActive"
                    };
                }
                string accessToken = _jwtService.GenerateToken(user);

                user.RefreshToken = GenerateRefreshToken();

                user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);

                return new ResponseModel<LoginResponseDto>
                {

                    Code = 200,

                    Message = "Login successful.",


                    Data = new LoginResponseDto
                    {

                        UserId = user.Id,
                        FullName = user.FullName,
                        Email = user.Email,
                        Role = user.Role,
                        Token = accessToken,
                        RefreshToken = user.RefreshToken
                    }
                };
            }
            catch (Exception ex)
            {
                return new ResponseModel<LoginResponseDto>
                {
                    Code = 500,
                    Message = ex.Message
                };
            }
        }

        public async Task<ResponseModel<LoginResponseDto>> RefreshTokenAsync(RefreshTokenRequestDto dto)
        {
            var user = await _userRepository.GetByIdAsync(dto.UserId);

            if (user == null)
            {

                return new ResponseModel<LoginResponseDto>
                {
                    Code = 404,
                    Message = "User not found."
                };

            }

            if (user.RefreshToken != dto.RefreshToken)
            {

                return new ResponseModel<LoginResponseDto>
                {
                    Code = 401,
                    Message = "Invalid refresh token."
                };

            }

            if (user.RefreshTokenExpiryTime < DateTime.UtcNow)
            {

                return new ResponseModel<LoginResponseDto>
                {
                    Code = 401,
                    Message = "Refresh token expired."
                };

            }

            string newToken =_jwtService.GenerateToken(user);

            user.RefreshToken =GenerateRefreshToken();

            user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7); 

            await _userRepository.UpdateAsync(user);

            return new ResponseModel<LoginResponseDto>
            {

                Code = 200,
                Message = "Token refreshed.",
                Data = new LoginResponseDto
                {
                    UserId = user.Id,
                    FullName = user.FullName,
                    Email = user.Email,
                    Role = user.Role,
                    Token = newToken,
                    RefreshToken = user.RefreshToken
                }
            };
        }

        public async Task<ResponseModel<string>> LogoutAsync(long userId)
        {
            try
            {
                var user = await _userRepository.GetByIdAsync(userId);

                if (user == null)
                {
                    return new ResponseModel<string>
                    {
                        Code = StatusCodes.Status404NotFound,
                        Message = "User not found."
                    };
                }
                user.RefreshToken = null;

                user.RefreshTokenExpiryTime = null;


                await _userRepository.UpdateAsync(user);

                return new ResponseModel<string>
                {
                    Code = StatusCodes.Status200OK,
                    Message = "Logout successful."
                };
            }
            catch (Exception ex)
            {
                return new ResponseModel<string>
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Message = ex.Message
                };
            }
        }

        private string GenerateRefreshToken()
        {
            return Guid.NewGuid().ToString();
        }

    }
}