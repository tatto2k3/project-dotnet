using BlueStarMVC.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlueStarMVC.Pages.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AirportController : ControllerBase
    {
        private readonly BluestarContext BlueContext;
        public AirportController(BluestarContext blueContext)
        {
            BlueContext = blueContext;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllAirport()
        {
            try
            {

                List<Sanbay> airports = await BlueContext.Sanbays.ToListAsync();


                if (airports == null || airports.Count == 0)
                {
                    return NotFound("Không có sân bay nào được tìm thấy");
                }


                return Ok(airports);
            }
            catch (Exception ex)
            {

                return StatusCode(500, $"Lỗi server: {ex.Message}");
            }
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAirportById(string id)
        {
            try
            {

                Sanbay airport = await BlueContext.Sanbays.FirstOrDefaultAsync(s => s.AirportId == id);


                if (airport == null)
                {
                    return NotFound($"Không tìm thấy sân bay với ID {id}");
                }


                return Ok(airport);
            }
            catch (Exception ex)
            {

                return StatusCode(500, $"Lỗi server: {ex.Message}");
            }
        }
    }
}
