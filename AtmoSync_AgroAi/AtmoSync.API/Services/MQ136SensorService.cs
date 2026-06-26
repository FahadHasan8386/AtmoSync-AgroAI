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
        public async Task<ResponseModel<List<MQ136Sensor>>> GetAllAsync()
        {
            try
            {
                var data = await _repository.GetAllAsync();

                return new ResponseModel<List<MQ136Sensor>>
                {
                    Code = StatusCodes.Status200OK,
                    Message = "Data Found",
                    Data = data
                };
            }
            catch (Exception ex)
            {
                return new ResponseModel<List<MQ136Sensor>>
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Message = ex.Message
                };
            }
        }

        // GET LATEST
        public async Task<ResponseModel<MQ136Sensor>> GetLatestAsync()
        {
            try
            {
                var result = await _repository.GetLatestAsync();

                if (result == null)
                {
                    return new ResponseModel<MQ136Sensor>
                    {
                        Code = StatusCodes.Status404NotFound,
                        Message = "No latest sensor data found."
                    };
                }

                return new ResponseModel<MQ136Sensor>
                {
                    Code = StatusCodes.Status200OK,
                    Message = "Latest data retrieved successfully.",
                    Data = result
                };
            }
            catch (Exception ex)
            {
                return new ResponseModel<MQ136Sensor>
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Message = ex.Message
                };
            }
        }

        // GET LAST N READINGS
        public async Task<ResponseModel<List<MQ136Sensor>>> GetLatestReadingsAsync(int count)
        {
            try
            {
                if (count <= 0)
                {
                    return new ResponseModel<List<MQ136Sensor>>
                    {
                        Code = StatusCodes.Status400BadRequest,
                        Message = "Count must be greater than zero."
                    };
                }

                var result = await _repository.GetLatestReadingsAsync(count);

                return new ResponseModel<List<MQ136Sensor>>
                {
                    Code = StatusCodes.Status200OK,
                    Message = "Data retrieved successfully.",
                    Data = result
                };
            }
            catch (Exception ex)
            {
                return new ResponseModel<List<MQ136Sensor>>
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Message = ex.Message
                };
            }
        }

        // CREATE 
        public async Task<ResponseModel<long>> CreateAsync(MQ136SensorDto dto)
        {
            try
            {
                if (dto.H2SLevel < 0 || dto.H2SLevel > 5000)
                {
                    return new ResponseModel<long>
                    {
                        Code = StatusCodes.Status400BadRequest,
                        Message = "Invalid Temperature Value."
                    };
                }

                var result = await _repository.CreateAsync(dto);

                return new ResponseModel<long>
                {
                    Code = StatusCodes.Status201Created,
                    Message = "Sensor data saved successfully.",
                    Data = result
                };
            }
            catch (Exception ex)
            {
                return new ResponseModel<long>
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Message = ex.Message
                };
            }
        }

        // DELETE
        public async Task<ResponseModel<int>> DeleteAsync(long id)
        {
            try
            {
                if (id <= 0)
                {
                    return new ResponseModel<int>
                    {
                        Code = StatusCodes.Status400BadRequest,
                        Message = "Invalid sensor Id."
                    };
                }

                var result = await _repository.DeleteAsync(id);

                return new ResponseModel<int>
                {
                    Code = StatusCodes.Status200OK,
                    Message = "Deleted successfully.",
                    Data = result
                };
            }
            catch (Exception ex)
            {
                return new ResponseModel<int>
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Message = ex.Message
                };
            }
        }

        // DATE RANGE
        public async Task<ResponseModel<List<MQ136Sensor>>> GetByDateRangeAsync(DateTime fromDate, DateTime toDate)
        {
            try
            {
                if (fromDate > toDate)
                {
                    return new ResponseModel<List<MQ136Sensor>>
                    {
                        Code = StatusCodes.Status400BadRequest,
                        Message = "FromDate cannot be greater than ToDate."
                    };
                }

                var result = await _repository.GetByDateRangeAsync(fromDate, toDate);

                return new ResponseModel<List<MQ136Sensor>>
                {
                    Code = StatusCodes.Status200OK,
                    Message = "Data retrieved successfully.",
                    Data = result
                };
            }
            catch (Exception ex)
            {
                return new ResponseModel<List<MQ136Sensor>>
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Message = ex.Message
                };
            }
        }
        //Update Status
        public async Task<ResponseModel<int>> UpdateStatusAsync(long id, bool inActive)
        {
            try
            {
                var result = await _repository.UpdateStatusAsync(id, inActive);

                return new ResponseModel<int>
                {
                    Code = StatusCodes.Status200OK,
                    Message = inActive
                        ? "Sensor marked as inactive successfully."
                        : "Sensor activated successfully.",
                    Data = result
                };
            }
            catch (Exception ex)
            {
                return new ResponseModel<int>
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Message = ex.Message
                };
            }
        }
    }
}