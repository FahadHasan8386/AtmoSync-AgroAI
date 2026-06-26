using AtmoSync.API.Interfaces.IRepositories;
using AtmoSync.API.Model;
using AtmoSync.Shared.Models.DtoModels;
using Dapper;
using System.Data;

namespace AtmoSync.API.Repository
{
    public class MQ7SensorRepository : IMQ7SensorRepository
    {
        private readonly IDbConnection _connection;

        public MQ7SensorRepository(IDbConnection connection)
        {
            _connection = connection;
        }

        // GET ALL
        public async Task<List<MQ7Sensor>> GetAllAsync()
        {
            const string sql = @" SELECT Id,COLevel,CreatedBy, CreatedAt, InActive,ModifiedBy,ModifiedAt
                                FROM MQ7Sensor
                                ORDER BY CreatedAt DESC";

            var result = await _connection.QueryAsync<MQ7Sensor>(sql);
            return result.ToList();
        }
        // GET LATEST
        public async Task<MQ7Sensor?> GetLatestAsync()
        {
            const string sql = @"SELECT TOP 1 *
                                FROM MQ7Sensor
                                ORDER BY CreatedAt DESC";

            return await _connection.QueryFirstOrDefaultAsync<MQ7Sensor>(sql);
        }
        // GET LAST N READINGS
        public async Task<List<MQ7Sensor>> GetLatestReadingsAsync(int count)
        {
            const string sql = @"SELECT TOP (@Count)
                                Id,COLevel,CreatedBy,CreatedAt, InActive, ModifiedBy,ModifiedAt
                                FROM MQ7Sensor
                                ORDER BY CreatedAt DESC";

            var result = await _connection.QueryAsync<MQ7Sensor>(
                sql,
                new { Count = count });

            return result.ToList();
        }
        // CREATE
        public async Task<long> CreateAsync(MQ7SensorDto dto)
        {
            const string sql = @"INSERT INTO MQ7Sensor(COLevel, CreatedBy, CreatedAt, InActive)
                                OUTPUT INSERTED.Id
                                VALUES (@COLevel, @CreatedBy, @CreatedAt, @InActive);";

            return await _connection.ExecuteScalarAsync<long>(sql, dto);
        }
        // DELETE
        public async Task<int> DeleteAsync(long id)
        {
            const string sql = @" DELETE FROM MQ7Sensor
                                WHERE Id = @Id";

            return await _connection.ExecuteAsync(sql, new { Id = id });
        }
        //Update InActive
        public async Task<int> UpdateStatusAsync(long id, bool inActive)
        {
            const string sql = @"UPDATE MQ7Sensor SET
                                 InActive = @InActive,
                                    ModifiedAt = @ModifiedAt,
                                    ModifiedBy = @ModifiedBy
                                WHERE Id = @Id";
            return await _connection.ExecuteAsync(sql, new
            {
                Id = id,
                InActive = inActive,
                ModifiedAt = DateTime.UtcNow,
                ModifiedBy = "Admin"
            });
        }
        // GET BY DATE RANGE
        public async Task<List<MQ7Sensor>> GetByDateRangeAsync(DateTime fromDate, DateTime toDate)
        {
            const string sql = @"SELECT * FROM MQ7Sensor
                                WHERE CreatedAt BETWEEN @FromDate AND @ToDate
                                ORDER BY CreatedAt DESC";

            var result = await _connection.QueryAsync<MQ7Sensor>(sql, new
            {
                FromDate = fromDate,
                ToDate = toDate
            });

            return result.ToList();
        }
    }
}

