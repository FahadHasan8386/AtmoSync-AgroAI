using AtmoSync.API.Model;
using AtmoSync.Shared;
using AtmoSync.Shared.Models.DtoModels;

namespace AtmoSync.API.Interfaces.IServices
{
    public interface IMQ136SensorService
    {
        Task<List<MQ136Sensor>> GetAllAsync();

        Task<ResponseModel> GetLatestAsync();

        Task<ResponseModel> GetLatestReadingsAsync(int count);

        Task<ResponseModel> GetByDateRangeAsync(DateTime fromDate, DateTime toDate);

        Task<ResponseModel> CreateAsync(MQ136SensorDto dto);

        Task<ResponseModel> DeleteAsync(long id);
    }
}
