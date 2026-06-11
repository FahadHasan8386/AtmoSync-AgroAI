using AtmoSync.API.Interfaces.IRepositories;
using AtmoSync.API.Interfaces.IServices;
using AtmoSync.API.Model;
using AtmoSync.API.Repository;
using AtmoSync.Shared;
using AtmoSync.Shared.Models.DtoModels;
using System.Transactions;

namespace AtmoSync.API.Services
{
    public class DHTSensorService : IDHTSensorService
    {
        private readonly IDHTSensorRepository _dHTSensorRepository;

        public DHTSensorService(IDHTSensorRepository dHTSensorRepository)
        {
            _dHTSensorRepository = dHTSensorRepository;
        }

        public async Task<List<DHTSensor>> GetAllAsync()
        {
            return await _dHTSensorRepository.GetAllAsync();
        }

        public async Task<ResponseModel> CreateAsync(DHTSensorDto dto)
        {
            try
            {
                if(dto.Temperature < -20 ||  dto.Temperature > 50)
                {
                    return new ResponseModel
                    {
                        Code = StatusCodes.Status400BadRequest,
                        Message = "Invaild Humidity Value ."
                    };
                }
                long result;
                using (TransactionScope transactionScope = new (TransactionScopeAsyncFlowOption.Enabled))
                {
                    result = await _dHTSensorRepository.CreateAsync(dto);

                    transactionScope.Complete();
                }

                if(result > 0)
                {
                    return new ResponseModel
                    {
                        Code = StatusCodes.Status201Created,
                        Message = "Sensor data saved successfully."
                    };
                }
                return new ResponseModel
                {
                    Code = StatusCodes.Status400BadRequest,
                    Message = "Failed to save sensor data."
                };
            }
            catch(Exception ex)
            {
                return new ResponseModel
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Message = ex.Message
                };
            }
        }

        public async Task<ResponseModel> DeleteAsync(long id) 
        { 
            try 
            { 
                if (id <= 0) 
                { 
                    return new ResponseModel 
                    {
                        Code = StatusCodes.Status400BadRequest, 
                        Message = "Invalid sensor Id." 
                    }; 
                } 
                int result; 
                using (TransactionScope transactionScope = new(TransactionScopeAsyncFlowOption.Enabled)) 
                { 
                    result = await _dHTSensorRepository.DeleteAsync(id); 

                    transactionScope.Complete(); 
                } 
                if (result > 0) 
                { 
                    return new ResponseModel 
                    { 
                        Code = StatusCodes.Status200OK, 
                        Message = "Deleted successfully."
                    };
                } 
                return new ResponseModel 
                { 
                    Code = StatusCodes.Status404NotFound, 
                    Message = "Record not found." 
                }; 
            } 
            catch (Exception ex) 
            { 
                return new ResponseModel 
                { 
                    Code = StatusCodes.Status500InternalServerError, 
                    Message = ex.Message 
                }; 
            } 
        }
    }
}


