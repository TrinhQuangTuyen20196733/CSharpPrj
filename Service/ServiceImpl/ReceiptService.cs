using BHDStarBooking.Config.MongoConfig;
using BHDStarBooking.DTO;
using BHDStarBooking.Entity;
using BHDStarBooking.Repository;
using BHDStarBooking.Service.IService;

namespace BHDStarBooking.Service.ServiceImpl
{
    public class ReceiptService : IReceipt
    {
        private readonly IMongoRepository<Receipt> receiptRepository;
        private readonly IUserService userService;

        public ReceiptService(IMongoRepository<Receipt> receiptRepository, IUserService userService)
        {
            this.receiptRepository = receiptRepository;
            this.userService = userService;
        }

        public async Task<List<ReceiptDTO>> getAllAsync()
        {
            List<Receipt> receipts = await receiptRepository.GetAllAsync();
            var receiptDTOs = new List<ReceiptDTO>();
            foreach (var receipt in receipts)
            {
                UserEntity user = await userService.getUserByEmailAsync(receipt.email);
                var receiptDTO = new ReceiptDTO()
                {
                    user = user,
                    seats = receipt.seats,
                    services = receipt.services
                };
                receiptDTOs.Add(receiptDTO);
            }
            return receiptDTOs;
        }

        public Task<List<ReceiptDTO>> getAllByUserIdAsync(string user_id)
        {
            throw new NotImplementedException();
        }

        public async Task<ReceiptDTO> insertOneAsync(Receipt receipt)
        {
           Receipt receiptResult= await receiptRepository.InsertAsync(receipt);
            UserEntity user = await userService.getUserByEmailAsync(receipt.email);
            var receiptDTO = new ReceiptDTO()
            {
                user = user,
                seats= receiptResult.seats,
                services=receiptResult.services
            };
            return receiptDTO;
        }
    }
}
