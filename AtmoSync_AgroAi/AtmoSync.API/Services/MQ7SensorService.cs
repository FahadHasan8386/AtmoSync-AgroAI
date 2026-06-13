using AtmoSync.API.Interfaces.IRepositories;
using AtmoSync.API.Interfaces.IServices;
using AtmoSync.API.Model;
using AtmoSync.Shared;
using AtmoSync.Shared.Models.DtoModels;

namespace AtmoSync.API.Services
{
    public class MQ7SensorService : IMQ7SensorService
    {
        private readonly IMQ7SensorRepository _repository;

        public MQ7SensorService(IMQ7SensorRepository repository)
        {
            _repository = repository;
        }

        // GET ALL
        public async Task<List<MQ7Sensor>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        // GET LATEST
        public async Task<ResponseModel> GetLatestAsync()
        {
            try
            {
                var result = await _repository.GetLatestAsync();

                if (result == null)
                {
                    return new ResponseModel
                    {
                        Code = StatusCodes.Status404NotFound,
                        Message = "No latest MQ7 sensor data found."
                    };
                }

                return new ResponseModel
                {
                    Code = StatusCodes.Status200OK,
                    Message = "Latest MQ7 data retrieved successfully.",
                    Data = result
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

        // GET LAST N READINGS
        public async Task<ResponseModel> GetLatestReadingsAsync(int count)
        {
            try
            {
                if (count <= 0)
                {
                    return new ResponseModel
                    {
                        Code = StatusCodes.Status400BadRequest,
                        Message = "Count must be greater than zero."
                    };
                }

                var result = await _repository.GetLatestReadingsAsync(count);

                if (result == null || !result.Any())
                {
                    return new ResponseModel
                    {
                        Code = StatusCodes.Status404NotFound,
                        Message = "No MQ7sensor data found."
                    };
                }

                return new ResponseModel
                {
                    Code = StatusCodes.Status200OK,
                    Message = "Data retrieved successfully.",
                    Data = result
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

        // CREATE
        public async Task<ResponseModel> CreateAsync(MQ7SensorDto dto)
        {
            try
            {
                if (dto.COLevel < 0 || dto.COLevel > 1000)
                {
                    return new ResponseModel
                    {
                        Code = StatusCodes.Status400BadRequest,
                        Message = "Invalid CO Gas Value."
                    };
                }

                var result = await _repository.CreateAsync(dto);

                if (result > 0)
                {
                    return new ResponseModel
                    {
                        Code = StatusCodes.Status201Created,
                        Message = "MQ7 sensor data saved successfully.",
                        Data = result
                    };
                }

                return new ResponseModel
                {
                    Code = StatusCodes.Status400BadRequest,
                    Message = "Failed to save sensor data."
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

        // DELETE
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

                var result = await _repository.DeleteAsync(id);

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

        // DATE RANGE
        public async Task<ResponseModel> GetByDateRangeAsync(DateTime fromDate, DateTime toDate)
        {
            try
            {
                if (fromDate > toDate)
                {
                    return new ResponseModel
                    {
                        Code = StatusCodes.Status400BadRequest,
                        Message = "FromDate cannot be greater than ToDate."
                    };
                }

                var result = await _repository.GetByDateRangeAsync(fromDate, toDate);

                if (result == null || !result.Any())
                {
                    return new ResponseModel
                    {
                        Code = StatusCodes.Status404NotFound,
                        Message = "No MQ7 data found in this date range."
                    };
                }

                return new ResponseModel
                {
                    Code = StatusCodes.Status200OK,
                    Message = "Data retrieved successfully.",
                    Data = result
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
