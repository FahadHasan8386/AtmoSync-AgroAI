using AtmoSync.API.Model;
using AtmoSync.Shared.Models.DtoModels;

namespace AtmoSync.API.Interfaces.IRepositories
{
    public interface IMQ136SensorRepository
    {
        Task<List<MQ136Sensor>> GetAllAsync();

        Task<MQ136Sensor?> GetLatestAsync();

        Task<List<MQ136Sensor>> GetLatestReadingsAsync(int count);

        Task<long> CreateAsync(MQ136SensorDto dto);

        Task<int> DeleteAsync(long id);

        Task<List<MQ136Sensor>> GetByDateRangeAsync(DateTime fromDate, DateTime toDate);
    }
}
