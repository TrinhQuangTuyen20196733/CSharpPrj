using BHDStarBooking.DTO;
using BHDStarBooking.DTO.Request;
using BHDStarBooking.DTO.Response;
using BHDStarBooking.DTO.Response.Page;
using BHDStarBooking.Entity;
using BHDStarBooking.Service.IService;
using BHDStarBooking.Service.ServiceImpl;
using BHDStarBooking.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson.IO;
using Newtonsoft.Json;
using System.Data;
using System.Net;
using JsonConvert = Newtonsoft.Json.JsonConvert;

namespace BHDStarBooking.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

   /* [Authorize(Roles = "ADMIN")]*/
    [EnableCors]
    public class MovieController : ControllerBase
    {
        private readonly IMovieService movieService;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public MovieController(IMovieService movieService, IWebHostEnvironment webHostEnvironment)
        {
            this.movieService = movieService;
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpPost]
        public async Task<IActionResult> CreateMovie([FromForm] IFormFile image, [FromForm] string movieInfo)
        {
            try
            {
                // Đọc dữ liệu phim từ JSON string

                MovieEntity movie = JsonConvert.DeserializeObject<MovieEntity>(movieInfo);

                // Lưu file ảnh vào đĩa
                string imagePath = ImageUtil.getInstance().SaveImageToDisk(image);
                movie.thumbnail = imagePath;
                MovieDTO movieDTO = await movieService.insertOneAsync(movie);
                movieService.insertListItem(movie);
                return Ok(movieDTO);

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return BadRequest("Thêm phim không thành công! Bạn vui lòng thử lại!");
            }
        }
        [HttpGet]
        [AllowAnonymous]
        public async Task<List<MovieDTO>> GetAllAsync()
        {
            return await movieService.getAllAsync();
        }
        [HttpGet("pages/{pageNumber}")]
        public async Task<MoviePage> getMovieByPageAsync(int pageNumber)
        {
            List<MovieDTO> movieEntities = await movieService.getByPageAsync(pageNumber);
            long totalElements = await movieService.getTotalElementAsync();
            var moviePage = new MoviePage
            {
                totalItemPage = totalElements,
                totalPage = totalElements / 8 + 1,
                movieDTOList = movieEntities

            };
            return moviePage;
        }
        [HttpDelete("{id:length(24)}")]
        public async Task<MessageResponse> DeleteMovie(string id)
        {
            try
            {
                await movieService.deleteByIdAsync(id);
                movieService.deleteListItem(id);    
                return new MessageResponse()
                {
                    status = (int)HttpStatusCode.OK,
                    description = "Bạn đã xóa phim thành công"
                };
            }
            catch (Exception e)
            {
                return new MessageResponse()
                {
                    status = (int)HttpStatusCode.BadRequest,
                    description = "Xóa phim thất bại"
                };
            }
        }
        [HttpGet("{id:length(24)}")]
        [AllowAnonymous]

        public async Task<MovieDTO> GetMovieByIdAsync(string id)
        {
            return await movieService.getByIdAsync(id);
        }
        [HttpPut]
        public async Task<IActionResult> UpdateMovieAsync([FromForm] IFormFile image, [FromForm] string movieInfo)
        {
            // Đọc dữ liệu phim từ JSON string

            MovieEntity movie = JsonConvert.DeserializeObject<MovieEntity>(movieInfo);

            // Lưu file ảnh vào đĩa
            string imagePath = ImageUtil.getInstance().SaveImageToDisk(image);
            movie.thumbnail = imagePath;
            MovieDTO movieDTO = await movieService.updateMovieAsync(movie);
            return Ok(movieDTO);
        }
        [HttpPut("/api/noImageUpdate/[controller]")]
        public async Task<IActionResult> updateMovieNoImagedUpdate([FromBody] MovieEntity movie)
        {
            try
            {
                MovieDTO movieDTO = await movieService.updateMovieNoImage(movie);
                movieService.updateListItem(movie);
                return Ok(movieDTO);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpGet("search")]
        public async Task<List<MovieDTO>> searchByTitle([FromQuery] string title)
        {

            return await movieService.searchByTitle(title);
        }
    }
}
