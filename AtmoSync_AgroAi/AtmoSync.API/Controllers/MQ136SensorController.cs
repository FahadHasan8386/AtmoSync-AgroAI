using AtmoSync.API.Interfaces.IServices;
using AtmoSync.Shared.Models.DtoModels;
using Microsoft.AspNetCore.Mvc;

namespace AtmoSync.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MQ136SensorController : ControllerBase
    {
        private readonly IMQ136SensorService _service;

        public MQ136SensorController(IMQ136SensorService service)
        {
            _service = service;
        }

        // GET ALL
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _service.GetAllAsync();
            return Ok(result);
        }

        // GET LATEST
        [HttpGet("latest")]
        public async Task<IActionResult> GetLatest()
        {
            var result = await _service.GetLatestAsync();
            return StatusCode(result.Code, result);
        }

        // GET LAST N READINGS
        [HttpGet("latest/{count}")]
        public async Task<IActionResult> GetLatestReadings(int count)
        {
            var result = await _service.GetLatestReadingsAsync(count);
            return StatusCode(result.Code, result);
        }

        // GET BY DATE RANGE
        [HttpGet("range")]
        public async Task<IActionResult> GetByDateRange(
            [FromQuery] DateTime fromDate,
            [FromQuery] DateTime toDate)
        {
            var result = await _service.GetByDateRangeAsync(fromDate, toDate);
            return StatusCode(result.Code, result);
        }

        // CREATE
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] MQ136SensorDto dto)
        {
            var result = await _service.CreateAsync(dto);
            return StatusCode(result.Code, result);
        }

        // DELETE
        [HttpDelete("{id:long}")]
        public async Task<IActionResult> Delete(long id)
        {
            var result = await _service.DeleteAsync(id);
            return StatusCode(result.Code, result);
        }
    }
}
