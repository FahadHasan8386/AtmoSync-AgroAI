using AtmoSync.API.Model;
using AtmoSync.Shared;
using AtmoSync.Shared.Models.DtoModels;

namespace AtmoSync.API.Interfaces.IServices
{
    public interface IMQ136SensorService
    {
        Task<ResponseModel<List<MQ136Sensor>>> GetAllAsync();

        Task<ResponseModel<MQ136Sensor>> GetLatestAsync();

        Task<ResponseModel<List<MQ136Sensor>>> GetLatestReadingsAsync(int count);

        Task<ResponseModel<List<MQ136Sensor>>> GetByDateRangeAsync(DateTime fromDate, DateTime toDate);

        Task<ResponseModel<long>> CreateAsync(MQ136SensorDto dto);

        Task<ResponseModel<int>> DeleteAsync(long id);
    }
}
