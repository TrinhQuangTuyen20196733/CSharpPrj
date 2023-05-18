using BHDStarBooking.DTO;
using BHDStarBooking.Entity;
using BHDStarBooking.Service.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace BHDStarBooking.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    [Authorize(Roles = "ADMIN")]
    public class ReceiptController : ControllerBase
    {
        private readonly IReceipt receiptService;

        public ReceiptController(IReceipt receiptService)
        {
            this.receiptService = receiptService;
        }

        [HttpPost]
        [AllowAnonymous]

        public async Task<ReceiptDTO> createReceipt([FromBody] Receipt receipt)
        {
            return await receiptService.insertOneAsync(receipt);
        }
        [HttpGet]
        [AllowAnonymous]

        public async Task<List<ReceiptDTO>> getAllReceiptAsync()
        {
            return await receiptService.getAllAsync();
        }
        [HttpGet("/api/users/{user_id:length(24)}/[controller]")]
        [AllowAnonymous]

        public async Task<List<ReceiptDTO>> getAllByUserIdAsync([FromQuery] string user_id)
        {
            return await receiptService.getAllByUserIdAsync(user_id);
        }

    }
}
