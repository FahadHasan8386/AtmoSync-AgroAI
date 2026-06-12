using AtmoSync.API.Interfaces.IRepositories;
using AtmoSync.API.Interfaces.IServices;
using AtmoSync.API.Model;
using AtmoSync.Shared;
using AtmoSync.Shared.Models.DtoModels;
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

        public async Task<ResponseModel> RegisterAsync(RegisterDto dto)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(dto.FullName))
                {
                    return new ResponseModel
                    {
                        Code = StatusCodes.Status400BadRequest,
                        Message = "Full Name is required."
                    };
                }

                if (string.IsNullOrWhiteSpace(dto.Email))
                {
                    return new ResponseModel
                    {
                        Code = StatusCodes.Status400BadRequest,
                        Message = "Email is required."
                    };
                }

                if (string.IsNullOrWhiteSpace(dto.Password))
                {
                    return new ResponseModel
                    {
                        Code = StatusCodes.Status400BadRequest,
                        Message = "Password is required."
                    };
                }

                var existingUser =await _userRepository.GetByEmailAsync(dto.Email);

                if (existingUser != null)
                {
                    return new ResponseModel
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
                        InActive = false
                    };

                    result = await _userRepository.CreateAsync(user);

                    transactionScope.Complete();
                }

                if (result > 0)
                {
                    return new ResponseModel
                    {
                        Code = StatusCodes.Status201Created,
                        Message = "Registration successful."
                    };
                }

                return new ResponseModel
                {
                    Code = StatusCodes.Status400BadRequest,
                    Message = "Registration failed."
                };
            }
            catch (Exception ex)
            {
                return new ResponseModel
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Message = ex.Message
                };
            }
        }

        public async Task<ResponseModel> LoginAsync(LoginDto dto)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(dto.Email))
                {
                    return new ResponseModel
                    {
                        Code = StatusCodes.Status400BadRequest,
                        Message = "Email is required."
                    };
                }

                if (string.IsNullOrWhiteSpace(dto.Password))
                {
                    return new ResponseModel
                    {
                        Code = StatusCodes.Status400BadRequest,
                        Message = "Password is required."
                    };
                }

                var user = await _userRepository.GetByEmailAsync(dto.Email);

                if (user == null)
                {
                    return new ResponseModel
                    {
                        Code = StatusCodes.Status401Unauthorized,
                        Message = "Invalid email or password."
                    };
                }

                bool isValidPassword = BCrypt.Net.BCrypt.Verify(dto.Password,user.PasswordHash);

                if (!isValidPassword)
                {
                    return new ResponseModel
                    {
                        Code = StatusCodes.Status401Unauthorized,
                        Message = "Invalid email or password."
                    };
                }

                if (user.InActive)
                {
                    return new ResponseModel
                    {
                        Code = StatusCodes.Status403Forbidden,
                        Message = "User account is inactive."
                    };
                }

                string token =_jwtService.GenerateToken(user);

                return new ResponseModel
                {
                    Code = StatusCodes.Status200OK,
                    Message = "Login successful.",
                    Data = new
                    {
                        UserId = user.Id,
                        user.FullName,
                        user.Email,
                        user.Role,
                        Token = token
                    }
                };
            }
            catch (Exception ex)
            {
                return new ResponseModel
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Message = ex.Message
                };
            }
        }
    }
}