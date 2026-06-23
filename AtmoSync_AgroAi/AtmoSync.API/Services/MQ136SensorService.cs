using AtmoSync.API.Interfaces.IRepositories;
using AtmoSync.API.Interfaces.IServices;
using AtmoSync.API.Model;
using AtmoSync.Shared;
using AtmoSync.Shared.Models.DtoModels;

namespace AtmoSync.API.Services
{
    public class MQ136SensorService : IMQ136SensorService
    {
        private readonly IMQ136SensorRepository _repository;

        public MQ136SensorService(IMQ136SensorRepository repository)
        {
            _repository = repository;
        }

        // GET ALL
        public async Task<ResponseModel> GetAllAsync()
        {
            try
            {
                var data = await _repository.GetAllAsync();

                return new ResponseModel
                {
                    Code = StatusCodes.Status200OK,
                    Message = "Data Found",
                    Data = data
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
                        Message = "No latest MQ136 sensor data found."
                    };
                }

                return new ResponseModel
                {
                    Code = StatusCodes.Status200OK,
                    Message = "Latest MQ136 data retrieved successfully.",
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
                        Message = "No MQ136 sensor data found."
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
        public async Task<ResponseModel> CreateAsync(MQ136SensorDto dto)
        {
            try
            {
                if (dto.H2SLevel < 0 || dto.H2SLevel > 5000)
                {
                    return new ResponseModel
                    {
                        Code = StatusCodes.Status400BadRequest,
                        Message = "Invalid MQ136 Gas Value."
                    };
                }

                var result = await _repository.CreateAsync(dto);

                if (result > 0)
                {
                    return new ResponseModel
                    {
                        Code = StatusCodes.Status201Created,
                        Message = "MQ136 sensor data saved successfully.",
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
                        Message = "No MQ136 data found in this date range."
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