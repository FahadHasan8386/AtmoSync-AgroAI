using AtmoSync.API.Interfaces.IRepositories;
using AtmoSync.API.Model;
using AtmoSync.Shared.Models.DtoModels;
using Dapper;
using System.Data;

namespace AtmoSync.API.Repository
{
    public class DHTSensorRepository : IDHTSensorRepository
    {
        private readonly IDbConnection _connection;

        public DHTSensorRepository(IDbConnection connection)
        {
            _connection = connection;
        }

        // GET ALL
        public async Task<List<DHTSensor>> GetAllAsync()
        {
            const string sql = @"
                SELECT
                    Id,
                    Temperature,
                    Humidity,
                    CreatedBy,
                    CreatedAt,
                    InActive,
                    ModifiedBy,
                    ModifiedAt
                FROM DHTSensor
                ORDER BY CreatedAt DESC";

            var result = await _connection.QueryAsync<DHTSensor>(sql);
            return result.ToList();
        }

        // GET LATEST
        public async Task<DHTSensor?> GetLatestAsync()
        {
            const string sql = @"
                SELECT TOP 1 *
                FROM DHTSensor
                ORDER BY CreatedAt DESC";

            return await _connection.QueryFirstOrDefaultAsync<DHTSensor>(sql);
        }

        // GET LAST N READINGS
        public async Task<List<DHTSensor>> GetLatestReadingsAsync(int count)
        {
            const string sql = @"
                SELECT TOP (@Count)
                    Id,
                    Temperature,
                    Humidity,
                    CreatedBy,
                    CreatedAt,
                    InActive,
                    ModifiedBy,
                    ModifiedAt
                FROM DHTSensor
                ORDER BY CreatedAt DESC";

            var result = await _connection.QueryAsync<DHTSensor>(
                sql,
                new { Count = count });

            return result.ToList();
        }

        // CREATE
        public async Task<long> CreateAsync(DHTSensorDto dto)
        {
            const string sql = @"
                INSERT INTO DHTSensor
                    (Temperature, Humidity, CreatedBy, CreatedAt, InActive)
                OUTPUT INSERTED.Id
                VALUES
                    (@Temperature, @Humidity, @CreatedBy, @CreatedAt, @InActive);";

            return await _connection.ExecuteScalarAsync<long>(sql, dto);
        }

        public async Task<int> UpdateStatusAsync(long id , bool inActive)
        {
            const string sql = @"UPDATE DHTSensor SET
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


        // DELETE
        public async Task<int> DeleteAsync(long id)
        {
            const string sql = @"
                DELETE FROM DHTSensor
                WHERE Id = @Id";

            return await _connection.ExecuteAsync(sql, new { Id = id });
        }

        // GET BY DATE RANGE
        public async Task<List<DHTSensor>> GetByDateRangeAsync(DateTime fromDate,DateTime toDate)
        {
            const string sql = @"SELECT *
                                FROM DHTSensor
                                WHERE CreatedAt >= @FromDate
                                AND CreatedAt < @ToDate
                                ORDER BY CreatedAt DESC";

            var result = await _connection.QueryAsync<DHTSensor>(sql,
                new
                {
                    FromDate = fromDate.Date,
                    ToDate = toDate.Date.AddDays(1)
                });

            return result.ToList();
        }
    }
}