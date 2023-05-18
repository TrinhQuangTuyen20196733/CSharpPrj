using AutoMapper;
using BHDStarBooking.DTO;
using BHDStarBooking.Entity;
using BHDStarBooking.Utils;

namespace BHDStarBooking.Mapper
{
    public class MovieProfile : Profile
    {
        public MovieProfile()
        {
            CreateMap<MovieEntity, MovieDTO>()
                 .ForMember(dest => dest.thumbnail, opt =>
                 opt.MapFrom(src => ImageUtil.getInstance().GetThumbnailToBase64(src.thumbnail)));

        }
    }
}
