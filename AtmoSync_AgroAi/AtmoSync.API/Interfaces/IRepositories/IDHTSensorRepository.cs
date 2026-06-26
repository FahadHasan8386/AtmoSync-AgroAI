using AtmoSync.API.Model;
using AtmoSync.Shared.Models.DtoModels;

namespace AtmoSync.API.Interfaces.IRepositories
{
    public interface IDHTSensorRepository
    {
        Task<List<DHTSensor>> GetAllAsync();

        Task<DHTSensor?> GetLatestAsync();

        Task<List<DHTSensor>> GetLatestReadingsAsync(int count);

        Task<long> CreateAsync(DHTSensorDto dto);

        Task<int> DeleteAsync(long id);

        Task<List<DHTSensor>> GetByDateRangeAsync(DateTime fromDate, DateTime toDate);

        Task<int> UpdateStatusAsync(int id, bool inActive);
    }
}
