using AtmoSync.API.Interfaces.IRepositories;
using AtmoSync.API.Model;
using Dapper;
using System.Data;

namespace AtmoSync.API.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly IDbConnection _connection;

        public UserRepository(IDbConnection connection)
        {
            _connection = connection;
        }

        public async Task<User?> GetByEmailAsync(string email)
        {
            const string sql = @"SELECT TOP 1 Id,FullName,Email,PasswordHash,Role,RefreshToken,RefreshTokenExpiryTime,CreatedBy,CreatedAt, InActive, ModifiedBy,ModifiedAt
                FROM Users
                WHERE Email = @Email";

            return await _connection.QueryFirstOrDefaultAsync<User>(sql,new { Email = email });
        }

        public async Task<User?> GetByIdAsync(long id)
        {
            const string sql = @"SELECT TOP 1 Id, FullName, Email, PasswordHash, Role,RefreshToken,RefreshTokenExpiryTime,CreatedBy, CreatedAt,InActive,ModifiedBy, ModifiedAt
                FROM Users
                WHERE Id = @Id";

            return await _connection.QueryFirstOrDefaultAsync<User>(sql,new { Id = id });
        }

        public async Task<long> CreateAsync(User user)
        {
            const string sql = @"INSERT INTO Users(FullName, Email, PasswordHash, Role,CreatedBy, CreatedAt,InActive)
                OUTPUT INSERTED.Id
                VALUES(@FullName,@Email,@PasswordHash,@Role,@CreatedBy,@CreatedAt,@InActive);";

            return await _connection.ExecuteScalarAsync<long>(sql,user);
        }

        public async Task UpdateAsync(User user)
        {
            const string sql = @"UPDATE Users SET 
                                RefreshToken = @RefreshToken,
                                RefreshTokenExpiryTime = @RefreshTokenExpiryTime,
                                ModifiedBy = @ModifiedBy,
                                ModifiedAt = GETDATE()
                                WHERE Id = @Id";

            await _connection.ExecuteAsync(sql, user);
        }
    }
}