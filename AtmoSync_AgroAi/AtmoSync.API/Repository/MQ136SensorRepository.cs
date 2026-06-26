using AtmoSync.API.Interfaces.IRepositories;
using AtmoSync.API.Model;
using AtmoSync.Shared.Models.DtoModels;
using Dapper;
using System.Data;

namespace AtmoSync.API.Repository
{
    public class MQ136SensorRepository : IMQ136SensorRepository
    {
        private readonly IDbConnection _connection;

        public MQ136SensorRepository(IDbConnection connection)
        {
            _connection = connection;
        }

        // GET ALL
        public async Task<List<MQ136Sensor>> GetAllAsync()
        {
            const string sql = @"SELECT Id, H2SLevel,CreatedBy, CreatedAt,InActive,ModifiedBy,ModifiedAt
                FROM MQ136Sensor
                ORDER BY CreatedAt DESC";

            var result = await _connection.QueryAsync<MQ136Sensor>(sql);
            return result.ToList();
        }

        // GET LATEST
        public async Task<MQ136Sensor?> GetLatestAsync()
        {
            const string sql = @"
                SELECT TOP 1 *
                FROM MQ136Sensor
                ORDER BY CreatedAt DESC";

            return await _connection.QueryFirstOrDefaultAsync<MQ136Sensor>(sql);
        }

        // GET LAST N READINGS
        public async Task<List<MQ136Sensor>> GetLatestReadingsAsync(int count)
        {
            const string sql = @"
                SELECT TOP (@Count)
                    Id,
                    H2SLevel,
                    CreatedBy,
                    CreatedAt,
                    InActive,
                    ModifiedBy,
                    ModifiedAt
                FROM MQ136Sensor
                ORDER BY CreatedAt DESC";

            var result = await _connection.QueryAsync<MQ136Sensor>(
                sql,
                new { Count = count });

            return result.ToList();
        }

        // CREATE
        public async Task<long> CreateAsync(MQ136SensorDto dto)
        {
            const string sql = @"
                INSERT INTO MQ136Sensor
                    (H2SLevel, CreatedBy, CreatedAt, InActive)
                OUTPUT INSERTED.Id
                VALUES
                    (@H2SLevel, @CreatedBy, @CreatedAt, @InActive);";

            return await _connection.ExecuteScalarAsync<long>(sql, dto);
        }

        // DELETE
        public async Task<int> DeleteAsync(long id)
        {
            const string sql = @"
                DELETE FROM MQ136Sensor
                WHERE Id = @Id";

            return await _connection.ExecuteAsync(sql, new { Id = id });
        }
        //Update InActive
        public async Task<int> UpdateStatusAsync(long id, bool inActive)
        {
            const string sql = @"UPDATE MQ136Sensor SET
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
        public async Task<List<MQ136Sensor>> GetByDateRangeAsync(DateTime fromDate, DateTime toDate)
        {
            const string sql = @"
                SELECT *
                FROM MQ136Sensor
                WHERE CreatedAt BETWEEN @FromDate AND @ToDate
                ORDER BY CreatedAt DESC";

            var result = await _connection.QueryAsync<MQ136Sensor>(sql, new
            {
                FromDate = fromDate,
                ToDate = toDate
            });

            return result.ToList();
        }
    }
}
