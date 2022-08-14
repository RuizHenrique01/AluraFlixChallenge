using AluraFlixAPI.Data.Dtos;
using AluraFlixAPI.Services;
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

    }
}