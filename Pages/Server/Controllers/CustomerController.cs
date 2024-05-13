using BlueStarMVC.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlueStarMVC.Pages.Server.Controllers
{
    [Route("api/customer")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly BluestarContext _dbContext;
        public CustomerController(BluestarContext dbContext)
        {
            _dbContext = dbContext;
        }
        [HttpGet]
        [Route("GetCustomers")]
        public IActionResult GetCustomers()
        {
            List<Customer> list = _dbContext.Customers.ToList();
            return StatusCode(StatusCodes.Status200OK, list);
        }

        [HttpPost]
        [Route("AddCustomer")]
        public IActionResult AddCustomer([FromBody] Customer customer)
        {
            if (customer == null)
            {
                return BadRequest("Invalid customer data");
            }

            try
            {
                _dbContext.Customers.Add(customer);
                _dbContext.SaveChanges();
                return Ok("Customer added successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpGet]
        [Route("GetCustomerDetails")]
        public IActionResult GetCustomerDetails([FromQuery] string cIds)
        {
            try
            {
                if (string.IsNullOrEmpty(cIds))
                {
                    return BadRequest("Invalid customer IDs");
                }

                var customerIds = cIds.Split(',');

                var customerDetails = _dbContext.Customers.Where(c => customerIds.Contains(c.CId)).ToList();

                return Ok(customerDetails);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpPut]
        [Route("UpdateCustomer")]
        public async Task<IActionResult> UpdateCustomer(Customer objCustomer)
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
                var existingCustomer = await _dbContext.Customers.FindAsync(objCustomer.CId);

                if (existingCustomer == null)
                {
                    return NotFound("Customer not found");
                }

                // Cập nhật thông tin của khách hàng từ dữ liệu mới
                existingCustomer.Fullname = objCustomer.Fullname;
                existingCustomer.Mail = objCustomer.Mail;
                existingCustomer.Point = objCustomer.Point;
                existingCustomer.NumId = objCustomer.NumId;

                // Lưu các thay đổi vào cơ sở dữ liệu
                await _dbContext.SaveChangesAsync();

                // Trả về thông tin khách hàng đã được cập nhật
                return Ok(existingCustomer);
            }
            catch (Exception ex)
            {
                // Xử lý lỗi và trả về lỗi 500 nếu có lỗi xảy ra
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteCustomers([FromBody] List<string> customerIds)
        {
            if (customerIds == null || !customerIds.Any())
            {
                return BadRequest("No customer IDs provided");
            }

            var customers = await _dbContext.Customers.Where(c => customerIds.Contains(c.CId)).ToListAsync();
            if (!customers.Any())
            {
                return NotFound("No matching customers found");
            }

            _dbContext.Customers.RemoveRange(customers);
            await _dbContext.SaveChangesAsync();
            return Ok("Customers deleted successfully");
        }

        [HttpGet]
        [Route("SearchCustomers")]
        public IActionResult SearchCustomers([FromQuery] string searchKeyword)
        {
            try
            {
                if (string.IsNullOrEmpty(searchKeyword))
                {
                    return BadRequest("Invalid search keyword");
                }

                // Search customers by name containing the provided keyword
                var searchResults = _dbContext.Customers
                .Where(c => c.Fullname.Contains(searchKeyword) || c.NumId.Contains(searchKeyword) || c.CId.Contains(searchKeyword))
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