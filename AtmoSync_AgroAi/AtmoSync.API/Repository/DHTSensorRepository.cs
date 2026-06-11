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
        ///Get all 
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
        ///Latest Data
        public async Task<DHTSensor?> GetLatestAsync()
        {
            const string sql = @"SELECT TOP 1 *
                                FROM DHTSensor
                                ORDER BY CreatedAt DESC";

            return await _connection.QueryFirstOrDefaultAsync<DHTSensor>(sql);
        }
        ///Create
        public async Task<long> CreateAsync(DHTSensorDto dto)
        {
            const string sql = @"INSERT INTO DHTSensor(Temperature,Humidity,CreatedBy,CreatedAt,InActive)
                                 OUTPUT INSERTED.Id
                                 VALUES(@Temperature,@Humidity,@CreatedBy, @CreatedAt,@InActive);";

            return await _connection.ExecuteScalarAsync<long>(sql, dto);
        }
        ///Delete
        public async Task<int> DeleteAsync(long id)
        {
            const string sql = @"DELETE FROM DHTSensor
                                 WHERE Id = @Id";

            return await _connection.ExecuteAsync(sql,new { Id = @id });
        }
        ///Date Range Search
        public async Task<List<DHTSensor>> GetByDateRangeAsync(DateTime fromDate, DateTime toDate)
        {
            const string sql = @"SELECT * FROM DHTSensor
                                 WHERE CreatedAt BETWEEN @FromDate AND @ToDate
                                ORDER BY CreatedAt DESC";

            var result = await _connection.QueryAsync<DHTSensor>(sql, new
            {
                FromDate = fromDate,
                ToDate = toDate
            });
            return result.ToList();
        }
    }
}
