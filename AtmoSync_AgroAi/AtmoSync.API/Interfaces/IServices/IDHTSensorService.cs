using AtmoSync.API.Model;
using AtmoSync.Shared;
using AtmoSync.Shared.Models.DtoModels;

namespace AtmoSync.API.Interfaces.IServices
{
    public interface IDHTSensorService
    {
        Task<ResponseModel> GetAllAsync();

        Task<ResponseModel> GetLatestAsync();

        Task<ResponseModel> GetLatestReadingsAsync(int count);

        Task<ResponseModel> GetByDateRangeAsync(DateTime fromDate, DateTime toDate);

        Task<ResponseModel> CreateAsync(DHTSensorDto dto);

        Task<ResponseModel> DeleteAsync(long id);
    }
}
