using AtmoSync.API.Model;
using AtmoSync.Shared.Models.DtoModels;

namespace AtmoSync.API.Interfaces.IRepositories
{
    public interface IMQ7SensorRepository
    {
        Task<List<MQ7Sensor>> GetAllAsync();

        Task<MQ7Sensor?> GetLatestAsync();

        Task<List<MQ7Sensor>> GetLatestReadingsAsync(int count);

        Task<long> CreateAsync(MQ7SensorDto dto);

        Task<int> DeleteAsync(long id);

        Task<List<MQ7Sensor>> GetByDateRangeAsync(DateTime fromDate, DateTime toDate);
    }
}
