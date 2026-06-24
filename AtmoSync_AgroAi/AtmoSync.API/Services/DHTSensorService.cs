using AtmoSync.API.Interfaces.IRepositories;
using AtmoSync.API.Interfaces.IServices;
using AtmoSync.API.Model;
using AtmoSync.Shared;
using AtmoSync.Shared.Models.DtoModels;
using Microsoft.AspNetCore.Http;
using System.Transactions;

public class DHTSensorService : IDHTSensorService
{
    private readonly IDHTSensorRepository _repository;

    public DHTSensorService(IDHTSensorRepository repository)
    {
        _repository = repository;
    }

    // GET ALL
    public async Task<ResponseModel<List<DHTSensor>>> GetAllAsync()
    {
        try
        {
            var data = await _repository.GetAllAsync();

            return new ResponseModel<List<DHTSensor>>
            {
                Code = StatusCodes.Status200OK,
                Message = "Data Found",
                Data = data
            };
        }
        catch (Exception ex)
        {
            return new ResponseModel<List<DHTSensor>>
            {
                Code = StatusCodes.Status500InternalServerError,
                Message = ex.Message
            };
        }
    }

    // GET LATEST 
    public async Task<ResponseModel<DHTSensor>> GetLatestAsync()
    {
        try
        {
            var result = await _repository.GetLatestAsync();

            if (result == null)
            {
                return new ResponseModel<DHTSensor>
                {
                    Code = StatusCodes.Status404NotFound,
                    Message = "No latest sensor data found."
                };
            }

            return new ResponseModel<DHTSensor>
            {
                Code = StatusCodes.Status200OK,
                Message = "Latest data retrieved successfully.",
                Data = result
            };
        }
        catch (Exception ex)
        {
            return new ResponseModel<DHTSensor>
            {
                Code = StatusCodes.Status500InternalServerError,
                Message = ex.Message
            };
        }
    }

    // GET LAST N READINGS
    public async Task<ResponseModel<List<DHTSensor>>> GetLatestReadingsAsync(int count)
    {
        try
        {
            if (count <= 0)
            {
                return new ResponseModel<List<DHTSensor>>
                {
                    Code = StatusCodes.Status400BadRequest,
                    Message = "Count must be greater than zero."
                };
            }

            var result = await _repository.GetLatestReadingsAsync(count);

            return new ResponseModel<List<DHTSensor>>
            {
                Code = StatusCodes.Status200OK,
                Message = "Data retrieved successfully.",
                Data = result
            };
        }
        catch (Exception ex)
        {
            return new ResponseModel<List<DHTSensor>>
            {
                Code = StatusCodes.Status500InternalServerError,
                Message = ex.Message
            };
        }
    }

    // CREATE 
    public async Task<ResponseModel<long>> CreateAsync(DHTSensorDto dto)
    {
        try
        {
            if (dto.Temperature < -20 || dto.Temperature > 50)
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

    // GET BY DATE RANGE 
    public async Task<ResponseModel<List<DHTSensor>>> GetByDateRangeAsync(DateTime fromDate,DateTime toDate)
    {
        try
        {
            if (fromDate > toDate)
            {
                return new ResponseModel<List<DHTSensor>>
                {
                    Code = StatusCodes.Status400BadRequest,
                    Message = "FromDate cannot be greater than ToDate."
                };
            }

            var result = await _repository.GetByDateRangeAsync(fromDate, toDate);

            return new ResponseModel<List<DHTSensor>>
            {
                Code = StatusCodes.Status200OK,
                Message = "Data retrieved successfully.",
                Data = result
            };
        }
        catch (Exception ex)
        {
            return new ResponseModel<List<DHTSensor>>
            {
                Code = StatusCodes.Status500InternalServerError,
                Message = ex.Message
            };
        }
    }
}