using AluraFlixAPI.Data.Dtos;
using AluraFlixAPI.Models;
using AutoMapper;

namespace AluraFlixAPI.Profiles{

    public class VideoProfile : Profile{

        public VideoProfile(){
            CreateMap<CreateVideoDto, Video>();
            CreateMap<Video, ReadVideoDto>();
            CreateMap<UpdateVideoDto, Video>();
        }
    }
}