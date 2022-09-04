using System;
using System.Collections.Generic;
using System.Linq;
using AluraFlixAPI.Data;
using AluraFlixAPI.Data.Dtos;
using AluraFlixAPI.Helpers;
using AluraFlixAPI.Models;
using AutoMapper;
using FluentResults;
using Microsoft.AspNetCore.Mvc.RazorPages;

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

        public PagedList<ReadVideoDto> FindAllVideos(string search = null, int page = 1)
        {
            List<Video> videos = new List<Video>();
            if (search != null)
            {
                videos = _context.Videos.Where(x => x.Titulo.ToUpper().Contains(search.ToUpper())).ToList();
                return PagedList<ReadVideoDto>.ToPagedList(_mapper.Map<List<ReadVideoDto>>(videos), page, 5);
            }

            videos = _context.Videos.ToList();
            return PagedList<ReadVideoDto>.ToPagedList(_mapper.Map<List<ReadVideoDto>>(videos), page, 5);
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

        public List<ReadVideoDto> FindAllVideosFree()
        {
            List<Video> videos = _context.Videos.Where(x => x.CategoriaId == 1).ToList();
            return _mapper.Map<List<ReadVideoDto>>(videos);
        }
    }
}