using AutoMapper;
using Zy.Video.App.Models;
using Zy.Video.Bll.Models;

namespace Zy.Video.App.Profiles
{
    public class VideoFileProfileDto : Profile
    {
        public VideoFileProfileDto()
        {
            this.CreateMap<VideoFileDto, VideoFileBo>().ReverseMap();
            this.CreateMap<VideoFileInfoDto, VideoFileInfoBo>().ReverseMap();
        }
    }
}
