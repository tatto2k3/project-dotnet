using BlueStarMVC.Models;
using BlueStarMVC.Pages.Server.Options;
using BlueStarMVC.Pages.Server;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Syncfusion.Pdf;
using Syncfusion.Pdf.Graphics;
using System.Net.Mail;
using System.Text.Json;


namespace BlueStarMVC.Pages.Server.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class CallBackController : ControllerBase
    {
        private readonly BluestarContext BlueContext;
        private readonly ZaloPayOptions zaloPayOptions;
        private readonly ILogger<CallBackController> logger;
        private readonly EmailOptions emailOptions;
        public CallBackController(BluestarContext blueContext,
            IOptions<ZaloPayOptions> options, ILogger<CallBackController> logger, IOptions<EmailOptions> emailOptions)
        {
            BlueContext = blueContext;
            zaloPayOptions = options.Value;
            this.logger = logger;
            this.emailOptions = emailOptions.Value;
        }
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] dynamic cbdata)
        {
            Console.WriteLine("cbdata: ", cbdata);
            var result = new Dictionary<string, object>();
            var dataProperty = cbdata.GetProperty("data");
            Console.WriteLine("dataProperty: ", dataProperty);
            try
            {
                if (dataProperty.ValueKind != JsonValueKind.Undefined)
                {
                    var dataStr = dataProperty.GetString();
                    var reqMac = Convert.ToString(cbdata.GetProperty("mac"));
                    var key2 = zaloPayOptions.Key2;
                    var mac = HmacHelper.Compute(ZaloPayHMAC.HMACSHA256, key2, dataStr);
                    if (!reqMac.Equals(mac))
                    {
                        // callback không hợp lệ
                        result["return_code"] = -1;
                        result["return_message"] = "mac not equal";
                    }
                    else
                    {
                        // thanh toán thành công
                        // merchant cập nhật trạng thái cho đơn hàng
                        var dataJson = JsonSerializer.Deserialize<Dictionary<string, object>>(dataStr);
                        var amount = dataJson["amount"];
                        long amount2 = amount.GetInt64();
                        var item = dataJson["embed_data"].GetString();
                        var itemJson = JsonSerializer.Deserialize<Dictionary<string, string>>(item);
                        var CustomerName = itemJson["CustomerName"];
                        var CustomerEmail = itemJson["CustomerEmail"];
                        var CustomerIdentify = itemJson["CustomerIdentify"];
                        var SeatID = itemJson["SeatID"];
                        var FlightID = itemJson["FlightID"];
                        var DepeartureDay = itemJson["DepeartureDay"];
                        var ArriveDay = itemJson["ArriveDay"];
                        var DepeartureTime = itemJson["DepeartureTime"];
                        var ArriveTime = itemJson["ArriveTime"];
                        var CustomerPhone = itemJson["CustomerPhone"];
                        var DurationTime = itemJson["DurationTime"];
                        var TripType = itemJson["TripType"];
                        var newTicket = new Ticket
                        {
                            TId = GenerateRandomString(4),
                            Cccd = CustomerIdentify,
                            Name = CustomerName,
                            FlyId = FlightID,
                            KgId = "K10",
                            SeatId = SeatID,
                            TicketPrice = amount2,
                            Mail = CustomerEmail
                        };

                        try
                        {
                            await BlueContext.Tickets.AddAsync(newTicket);
                            await BlueContext.SaveChangesAsync();


                            PdfDocument ticketPdf = UtilsEmail.CreatePdfDocument(amount2, CustomerName, CustomerIdentify, SeatID,
                                FlightID, DepeartureDay
                                , ArriveDay, DepeartureTime, ArriveTime, CustomerPhone, DurationTime, TripType);
                            MemoryStream ms = new MemoryStream();
                            ticketPdf.Save(ms);
                            ticketPdf.Close(true);
                            ms.Position = 0;
                            Attachment file = new Attachment(ms, $"{CustomerName}_Ticket.pdf", "application/pdf");
                            UtilsEmail.SendEMail(emailOptions.Username, CustomerEmail, "Your ticket for Blue Star",
                                "Please get your ticket here :", file,
                                emailOptions.Username, emailOptions.Password);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Error: {ex.Message}");
                            // Add more specific exception handling if needed
                        }
                        result["return_code"] = 1;
                        result["return_message"] = "success";
                    }
                }
            }
            catch (Exception ex)
            {
                result["return_code"] = 0; // ZaloPay server sẽ callback lại (tối đa 3 lần)
                result["return_message"] = ex.Message;
                logger.LogError(ex.Message);
            }

            // thông báo kết quả cho ZaloPay server
            return Ok(result);
        }
        private static string GenerateRandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var random = new Random();
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

       

    }
}