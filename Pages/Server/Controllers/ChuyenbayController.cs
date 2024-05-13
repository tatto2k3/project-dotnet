using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BlueStarMVC.Models;
using Microsoft.EntityFrameworkCore;

namespace BlueStarMVC.Pages.Server.Controllers
{
    [Route("api/chuyenbay")]
    [ApiController]
    public class ChuyenbayController : ControllerBase
    {
        private readonly BluestarContext _dbContext;
        public ChuyenbayController(BluestarContext dbContext)
        {
            _dbContext = dbContext;
        }
        [HttpGet]
        [Route("GetChuyenbays")]
        public IActionResult GetChuyenbays()
        {
            List<Chuyenbay> list = _dbContext.Chuyenbays.ToList();
            return StatusCode(StatusCodes.Status200OK, list);
        }
        [HttpPost]
        [Route("AddChuyenbay")]
        public IActionResult AddChuyenbay([FromBody] Chuyenbay chuyenbay)
        {
            if (chuyenbay == null)
            {
                return BadRequest("Invalid chuyenbay data");
            }

            try
            {
                _dbContext.Chuyenbays.Add(chuyenbay);
                _dbContext.SaveChanges();
                return Ok("chuyenbay added successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpGet]
        [Route("GetChuyenbayDetails")]
        public IActionResult GetChuyenbayDetails([FromQuery] string flyIds)
        {
            try
            {
                if (string.IsNullOrEmpty(flyIds))
                {
                    return BadRequest("Invalid chuyenbay IDs");
                }

                var chuyenbayIds = flyIds.Split(',');

                var chuyenbayDetails = _dbContext.Chuyenbays.Where(c => chuyenbayIds.Contains(c.FlyId)).ToList();

                return Ok(chuyenbayDetails);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpPut]
        [Route("UpdateChuyenbay")]
        public async Task<IActionResult> UpdateChuyenbay(Chuyenbay objChuyenbay)
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
                var existingChuyenbay = await _dbContext.Chuyenbays.FindAsync(objChuyenbay.FlyId);

                if (existingChuyenbay == null)
                {
                    return NotFound("chuyenbay not found");
                }

                // Cập nhật thông tin của khách hàng từ dữ liệu mới
                existingChuyenbay.PlId = objChuyenbay.PlId;
                existingChuyenbay.FromLocation = objChuyenbay.FromLocation;
                existingChuyenbay.ToLocation = objChuyenbay.ToLocation;
                existingChuyenbay.OriginalPrice = objChuyenbay.OriginalPrice;
                existingChuyenbay.ArrivalTime = objChuyenbay.ArrivalTime;
                existingChuyenbay.DepartureTime = objChuyenbay.DepartureTime;
                existingChuyenbay.DepartureDay = objChuyenbay.DepartureDay;

                // Lưu các thay đổi vào cơ sở dữ liệu
                await _dbContext.SaveChangesAsync();

                // Trả về thông tin khách hàng đã được cập nhật
                return Ok(existingChuyenbay);
            }
            catch (Exception ex)
            {
                // Xử lý lỗi và trả về lỗi 500 nếu có lỗi xảy ra
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteChuyenbays([FromBody] List<string> chuyenbayIds)
        {
            if (chuyenbayIds == null || !chuyenbayIds.Any())
            {
                return BadRequest("No chuyenbay IDs provided");
            }

            var chuyenbays = await _dbContext.Chuyenbays.Where(c => chuyenbayIds.Contains(c.FlyId)).ToListAsync();
            if (!chuyenbays.Any())
            {
                return NotFound("No matching chuyenbays found");
            }

            _dbContext.Chuyenbays.RemoveRange(chuyenbays);
            await _dbContext.SaveChangesAsync();
            return Ok("chuyenbays deleted successfully");
        }
        [HttpGet]
        [Route("SearchChuyenbays")]
        public IActionResult SearchChuyenbays([FromQuery] string searchKeyword)
        {
            try
            {
                if (string.IsNullOrEmpty(searchKeyword))
                {
                    return BadRequest("Invalid search keyword");
                }

                // Search customers by name containing the provided keyword
                var searchResults = _dbContext.Chuyenbays
                .Where(c => c.FlyId.Contains(searchKeyword) || c.DepartureDay.Contains(searchKeyword) || c.ArrivalTime.Contains(searchKeyword) || c.FromLocation.Contains(searchKeyword) || c.ToLocation.Contains(searchKeyword) || c.PlId.Contains(searchKeyword) || c.OriginalPrice.ToString().Contains(searchKeyword))
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
