using AtmoSync.API.Model;
using AtmoSync.Shared;
using AtmoSync.Shared.Models.DtoModels;

namespace AtmoSync.API.Interfaces.IServices
{
    public interface IMQ7SensorService
    {
        Task<List<MQ7Sensor>> GetAllAsync();

        Task<ResponseModel> GetLatestAsync();

        Task<ResponseModel> GetLatestReadingsAsync(int count);

        Task<ResponseModel> GetByDateRangeAsync(DateTime fromDate, DateTime toDate);

        Task<ResponseModel> CreateAsync(MQ7SensorDto dto);

        Task<ResponseModel> DeleteAsync(long id);
    }
}
