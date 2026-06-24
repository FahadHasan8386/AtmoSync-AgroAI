using AtmoSync.API.Model;
using AtmoSync.Shared;
using AtmoSync.Shared.Models.DtoModels;

namespace AtmoSync.API.Interfaces.IServices
{
    public interface IMQ7SensorService
    {
        Task<ResponseModel<List<MQ7Sensor>>> GetAllAsync();

        Task<ResponseModel<MQ7Sensor>> GetLatestAsync();

        Task<ResponseModel<List<MQ7Sensor>>> GetLatestReadingsAsync(int count);

        Task<ResponseModel<List<MQ7Sensor>>> GetByDateRangeAsync(DateTime fromDate, DateTime toDate);

        Task<ResponseModel<long>> CreateAsync(MQ7SensorDto dto);

        Task<ResponseModel<int>> DeleteAsync(long id);
    }
}
