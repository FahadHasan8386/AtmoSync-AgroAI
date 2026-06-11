using AtmoSync.API.Model;
using AtmoSync.Shared;
using AtmoSync.Shared.Models.DtoModels;

namespace AtmoSync.API.Interfaces.IServices
{
    public interface IDHTSensorService
    {
        Task<List<DHTSensor>> GetAllAsync();
        Task<ResponseModel> CreateAsync(DHTSensorDto dto);
        Task<ResponseModel> DeleteAsync(long id);
    }
}
