using BlueStarMVC.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlueStarMVC.Pages.Server.Controllers
{
    [Route("api/sanbay")]
    [ApiController]
    public class SanBayController : ControllerBase
    {
        private readonly BluestarContext _dbContext;
        public SanBayController(BluestarContext dbContext)
        {
            _dbContext = dbContext;
        }
        [HttpGet]
        [Route("GetSanbays")]
        public IActionResult GetSanbays()
        {
            List<Sanbay> list = _dbContext.Sanbays.ToList();
            return StatusCode(StatusCodes.Status200OK, list);
        }
        [HttpPost]
        [Route("AddSanbay")]
        public IActionResult AddCustomer([FromBody] Sanbay sanbay)
        {
            if (sanbay == null)
            {
                return BadRequest("Invalid sanbay data");
            }

            try
            {
                _dbContext.Sanbays.Add(sanbay);
                _dbContext.SaveChanges();
                return Ok("Sanbay added successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpGet]
        [Route("GetSanbayDetails")]
        public IActionResult GetCustomerDetails([FromQuery] string airportIds)
        {
            try
            {
                if (string.IsNullOrEmpty(airportIds))
                {
                    return BadRequest("Invalid customer IDs");
                }

                var sanbayIds = airportIds.Split(',');

                var sanbayDetails = _dbContext.Sanbays.Where(c => sanbayIds.Contains(c.AirportId)).ToList();

                return Ok(sanbayDetails);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpPut]
        [Route("UpdateSanbay")]
        public async Task<IActionResult> UpdateSanbay(Sanbay objSanbay)
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
                var existingSanbay = await _dbContext.Sanbays.FindAsync(objSanbay.AirportId);

                if (existingSanbay == null)
                {
                    return NotFound("Customer not found");
                }

                // Cập nhật thông tin của khách hàng từ dữ liệu mới
                existingSanbay.AirportName = objSanbay.AirportName;
                existingSanbay.Place = objSanbay.Place;


                // Lưu các thay đổi vào cơ sở dữ liệu
                await _dbContext.SaveChangesAsync();

                // Trả về thông tin khách hàng đã được cập nhật
                return Ok(existingSanbay);
            }
            catch (Exception ex)
            {
                // Xử lý lỗi và trả về lỗi 500 nếu có lỗi xảy ra
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteSanbays([FromBody] List<string> sanbayIds)
        {
            if (sanbayIds == null || !sanbayIds.Any())
            {
                return BadRequest("No sanbay IDs provided");
            }

            var sanbays = await _dbContext.Sanbays.Where(c => sanbayIds.Contains(c.AirportId)).ToListAsync();
            if (!sanbays.Any())
            {
                return NotFound("No matching customers found");
            }

            _dbContext.Sanbays.RemoveRange(sanbays);
            await _dbContext.SaveChangesAsync();
            return Ok("Customers deleted successfully");
        }
        [HttpGet]
        [Route("SearchSanbays")]
        public IActionResult SearchSanbays([FromQuery] string searchKeyword)
        {
            try
            {
                if (string.IsNullOrEmpty(searchKeyword))
                {
                    return BadRequest("Invalid search keyword");
                }

                // Search customers by name containing the provided keyword
                var searchResults = _dbContext.Sanbays
                .Where(c => c.AirportId.Contains(searchKeyword) || c.AirportName.Contains(searchKeyword) || c.Place.Contains(searchKeyword))
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
