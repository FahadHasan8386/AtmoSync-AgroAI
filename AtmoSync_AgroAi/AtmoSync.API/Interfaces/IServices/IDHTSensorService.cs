using AtmoSync.API.Model;
using AtmoSync.Shared;
using AtmoSync.Shared.Models.DtoModels;

namespace AtmoSync.API.Interfaces.IServices
{
    public interface IDHTSensorService
    {
        Task<ResponseModel<List<DHTSensor>>> GetAllAsync();

        Task<ResponseModel<DHTSensor>> GetLatestAsync();

        Task<ResponseModel<List<DHTSensor>>> GetLatestReadingsAsync(int count);

        Task<ResponseModel<List<DHTSensor>>> GetByDateRangeAsync( DateTime fromDate,DateTime toDate);

        Task<ResponseModel<long>> CreateAsync(DHTSensorDto dto);

        Task<ResponseModel<int>> DeleteAsync(long id);
    }
}
