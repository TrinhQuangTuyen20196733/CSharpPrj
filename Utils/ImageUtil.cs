using Microsoft.AspNetCore.Hosting;

namespace BHDStarBooking.Utils
{
    public class ImageUtil
    {
        private static BHDStarBooking.Utils.ImageUtil INSTANCE;

        public static BHDStarBooking.Utils.ImageUtil getInstance()
        {
            if (INSTANCE == null)
            {
                INSTANCE = new BHDStarBooking.Utils.ImageUtil();
            }
            return INSTANCE;
        }




        public  string SaveImageToDisk(IFormFile image)
        {
            try
            {
                //Lấy tên gốc của file
                string fileName = Path.GetFileName(image.FileName);
                //Tạo đường dẫn tuyệt đối cho thư mục lưu trữ
                string uploadPath = Path.Combine("./uploads", fileName);
                // Kiểm tra và tạo thư mục lưu trữ nếu nó chưa tồn tại
                string directory = Path.GetDirectoryName(uploadPath);
                if (!Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                }
                //Mở luồng Stream để ghi dữ liệu từ file vào đĩa
                using (var stream = new FileStream(uploadPath, FileMode.Create))
                {
                    image.CopyTo(stream); //Sao chép dữ liệu từ file vào luồng Stream
                }
                return uploadPath;
            }
            catch (IOException ex)
            {
                Console.WriteLine(ex.Message);
                throw new Exception("Thêm phim không thành công! Bạn vui lòng tạo lại!");
            }
        }
        public byte[] GetThumbnailToBase64(string pathThumbnail)
        {
            try
            {
                byte[] bytes = File.ReadAllBytes(pathThumbnail);
                return bytes;
            }
            catch (Exception e)
            {
                throw new Exception("Không thể tải được ảnh của phim!", e);
            }
        }
    }
}
