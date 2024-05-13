using Blue_star.Pages.Server.DTOs;
using BlueStarMVC.Models;
using BlueStarMVC.Pages.Server.DTOs;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlueStarMVC.Pages.Server.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class SeatController : ControllerBase
    {
        private readonly BluestarContext _blueContext;
        private readonly ILogger<SeatController> _logger;
        public SeatController(BluestarContext blueStarFrameContext, ILogger<SeatController> logger)
        {
            _blueContext = blueStarFrameContext;
            _logger = logger;
        }
        [HttpGet]
        public async Task<IActionResult> GetSeatByFlight([FromQuery] string FlightId)
        {
            try
            {

                var seats = await _blueContext.Seats
                    .Where(s => s.FlightId == FlightId)
                    .Select(s => new SeatDto
                    {
                        SeatId = s.SeatId,
                        SeatType = s.SeatType,
                        FlightId = s.FlightId,
                        Isbooked = s.IsBooked
                    })
                    .ToListAsync();

                return Ok(seats);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }

    }
}