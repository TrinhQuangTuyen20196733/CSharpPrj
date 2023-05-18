using BHDStarBooking.DTO;
using BHDStarBooking.Entity;

namespace BHDStarBooking.Service.IService
{
    public interface IReceipt
    {
        Task<List<ReceiptDTO>> getAllAsync();
        Task<List<ReceiptDTO>> getAllByUserIdAsync(string user_id);
        Task<ReceiptDTO> insertOneAsync(Receipt receipt);
    }
}
