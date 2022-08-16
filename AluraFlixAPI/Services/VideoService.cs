using System;
using System.Collections.Generic;
using System.Linq;
using AluraFlixAPI.Data;
using AluraFlixAPI.Data.Dtos;
using AluraFlixAPI.Models;
using AutoMapper;
using FluentResults;

namespace AluraFlixAPI.Services{
    public class VideoService{
        private AppDbContext _context;
        private IMapper _mapper;

        public VideoService(AppDbContext context, IMapper mapper){
            _context = context;
            _mapper = mapper;
        }
        
        public ReadVideoDto CreateVideo(CreateVideoDto createVideoDto){
            Video video = _mapper.Map<Video>(createVideoDto);

            _context.Videos.Add(video);
            _context.SaveChanges();

            return _mapper.Map<ReadVideoDto>(video);
        }

        public ReadVideoDto FindOneVideo(int id){
            Video video = _context.Videos.Find(id);

            if(video == null) return null;
           
           return _mapper.Map<ReadVideoDto>(video);
        }

        public List<ReadVideoDto> FindAllVideos()
        {
            List<Video> videos = _context.Videos.ToList();
            return _mapper.Map<List<ReadVideoDto>>(videos);
        }

        public Result UpdateVideo(int id, UpdateVideoDto updateVideoDto)
        {
            Video video = _context.Videos.Find(id);

            if(video == null) 
                return Result.Fail("Video não encontrado.");

            _mapper.Map(updateVideoDto, video);
            _context.SaveChanges();

            return Result.Ok();
        }

        public Result DeleteVideo(int id)
        {
            Video video = _context.Videos.Find(id);

            if(video == null) 
                return Result.Fail("Video não encontrado.");

            _context.Videos.Remove(video);
            _context.SaveChanges();

            return Result.Ok();
        }
    }
}