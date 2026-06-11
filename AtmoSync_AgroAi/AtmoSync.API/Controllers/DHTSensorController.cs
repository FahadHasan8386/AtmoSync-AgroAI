using AtmoSync.API.Interfaces.IServices;
using AtmoSync.Shared.Models.DtoModels;
using Microsoft.AspNetCore.Mvc;

namespace Atmosync.Api.Controllers 
{ 
    [Route("api/[controller]")]
    [ApiController] 
    public class DHTSensorController : ControllerBase 
    { 
        private readonly IDHTSensorService _dhtSensorService; 
        public DHTSensorController(IDHTSensorService dhtSensorService) 
        { 
            _dhtSensorService = dhtSensorService; 
        } 
        [HttpGet] 
        public async Task<IActionResult> GetAll() 
        { var result = await _dhtSensorService.GetAllAsync(); 
            return Ok(result);
        } 
        
        [HttpPost] public async Task<IActionResult> Create( [FromBody] DHTSensorDto dto) 
        {
            var result = await _dhtSensorService.CreateAsync(dto); 
            return StatusCode(result.Code, result); 
        }
        
        //[HttpPatch("status/{id:long}")] 
        //public async Task<IActionResult> ChangeStatus( long id, [FromQuery] string changedBy) 
        //{ 
        //    var result = await _dhtSensorService.ChangeStatusAsync( id, changedBy); 
        //    return StatusCode(result.Code, result);
        //} 

        [HttpDelete("{id:long}")] 
        public async Task<IActionResult> Delete(long id) 
        { 
            var result = await _dhtSensorService.DeleteAsync(id); 
            return StatusCode(result.Code, result); 
        } 
    } 
}