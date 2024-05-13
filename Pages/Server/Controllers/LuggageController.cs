using BlueStarMVC.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlueStarMVC.Pages.Server.Controllers
{
    [Route("api/luggage")]
    [ApiController]
    public class LuggageController : ControllerBase
    {
        private readonly BluestarContext _dbContext;
        public LuggageController(BluestarContext dbContext)
        {
            _dbContext = dbContext;
        }
        [HttpGet]
        [Route("GetLuggages")]
        public IActionResult GetLuggages()
        {
            List<Luggage> list = _dbContext.Luggage.ToList();
            return StatusCode(StatusCodes.Status200OK, list);
        }
        [HttpPost]
        [Route("AddLuggage")]
        public IActionResult Addluggage([FromBody] Luggage luggage)
        {
            if (luggage == null)
            {
                return BadRequest("Invalid luggage data");
            }

            try
            {
                _dbContext.Luggage.Add(luggage);
                _dbContext.SaveChanges();
                return Ok("luggage added successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpGet]
        [Route("GetLuggageDetails")]
        public IActionResult GetLuggageDetails([FromQuery] string luggageCode)
        {
            try
            {
                if (string.IsNullOrEmpty(luggageCode))
                {
                    return BadRequest("Invalid luggage IDs");
                }

                var luggageIds = luggageCode.Split(',');

                var luggageDetails = _dbContext.Luggage.Where(c => luggageIds.Contains(c.LuggageCode)).ToList();

                return Ok(luggageDetails);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpPut]
        [Route("Updateluggage")]
        public async Task<IActionResult> Updateluggage(Luggage objLuggage)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                    {
                        Console.WriteLine(error.ErrorMessage);
                    }
                    return BadRequest(ModelState);
                }
                // Tìm kiếm khách hàng dựa trên id (hoặc mã khách hàng, tùy thuộc vào cách bạn xác định)
                var existingluggage = await _dbContext.Luggage.FindAsync(objLuggage.LuggageCode);

                if (existingluggage == null)
                {
                    return NotFound("luggage not found");
                }

                // Cập nhật thông tin của khách hàng từ dữ liệu mới
                existingluggage.LuggageCode = objLuggage.LuggageCode;
                existingluggage.Mass = objLuggage.Mass;
                existingluggage.Price = objLuggage.Price;


                // Lưu các thay đổi vào cơ sở dữ liệu
                await _dbContext.SaveChangesAsync();

                // Trả về thông tin khách hàng đã được cập nhật
                return Ok(existingluggage);
            }
            catch (Exception ex)
            {
                // Xử lý lỗi và trả về lỗi 500 nếu có lỗi xảy ra
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete]
        public async Task<ActionResult> Deleteluggages([FromBody] List<string> luggageIds)
        {
            if (luggageIds == null || !luggageIds.Any())
            {
                return BadRequest("No luggage IDs provided");
            }

            var luggages = await _dbContext.Luggage.Where(c => luggageIds.Contains(c.LuggageCode)).ToListAsync();
            if (!luggages.Any())
            {
                return NotFound("No matching luggages found");
            }

            _dbContext.Luggage.RemoveRange(luggages);
            await _dbContext.SaveChangesAsync();
            return Ok("luggages deleted successfully");
        }
        [HttpGet]
        [Route("SearchLuggages")]
        public IActionResult SearchLuggages([FromQuery] string searchKeyword)
        {
            try
            {
                if (string.IsNullOrEmpty(searchKeyword))
                {
                    return BadRequest("Invalid search keyword");
                }

                // Search customers by name containing the provided keyword
                var searchResults = _dbContext.Luggage
                .Where(c => c.LuggageCode.Contains(searchKeyword))
                .ToList();

                return Ok(searchResults);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
