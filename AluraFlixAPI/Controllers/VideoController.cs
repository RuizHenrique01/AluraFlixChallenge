using System.Collections.Generic;
using AluraFlixAPI.Data.Dtos;
using AluraFlixAPI.Services;
using FluentResults;
using Microsoft.AspNetCore.Mvc;

namespace AluraFlixAPI.Controllers{
    [ApiController]
    [Route("[controller]")]
    public class VideoController : ControllerBase{
        private VideoService _videoService;

        public VideoController(VideoService videoService){
            _videoService = videoService;
        }

        [HttpPost]
        public IActionResult CreateVideo([FromBody] CreateVideoDto createVideoDto){
            ReadVideoDto readVideoDto = _videoService.CreateVideo(createVideoDto);
            return CreatedAtAction(nameof(FindOneVideo), new {Id = readVideoDto.Id}, readVideoDto);
        }

        [HttpGet("{id}")]
        public IActionResult FindOneVideo(int id){
            ReadVideoDto readVideoDto = _videoService.FindOneVideo(id);

            if(readVideoDto == null) return NotFound("Video não encontrado");
            return Ok(readVideoDto);
        }

        [HttpGet]
        public IActionResult FindAllVideos(){
            List<ReadVideoDto> readVideoDto = _videoService.FindAllVideos();
            return Ok(readVideoDto);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateVideo(int id, [FromBody] UpdateVideoDto updateVideoDto){
            Result result = _videoService.UpdateVideo(id, updateVideoDto);
            if(result.IsFailed) return NotFound(result.Errors[0].Message);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteVideo(int id){
            Result result = _videoService.DeleteVideo(id);
            if(result.IsFailed) return NotFound(result.Errors[0].Message);
            return NoContent();
        }
    }
}