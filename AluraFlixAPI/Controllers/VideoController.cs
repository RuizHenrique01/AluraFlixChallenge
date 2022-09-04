using System.Collections.Generic;
using System.Data;
using AluraFlixAPI.Data.Dtos;
using AluraFlixAPI.Services;
using FluentResults;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AluraFlixAPI.Controllers{
    [ApiController]
    [Route("[controller]")]
    public class VideoController : ControllerBase{
        private VideoService _videoService;
        private CategoriaService _categoriaService;

        public VideoController(VideoService videoService, CategoriaService categoriaService){
            _videoService = videoService;
            _categoriaService = categoriaService;
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public IActionResult CreateVideo([FromBody] CreateVideoDto createVideoDto){
            if(createVideoDto.CategoriaId != 0)
            {
                ReadCategoriaDto readCategoriaDto =_categoriaService.FindOneCategoria(createVideoDto.CategoriaId);

                if (readCategoriaDto == null)
                {
                    return NotFound("Categoria não encontrada");
                }
            }

            ReadVideoDto readVideoDto = _videoService.CreateVideo(createVideoDto);
            return CreatedAtAction(nameof(FindOneVideo), new {Id = readVideoDto.Id}, readVideoDto);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "admin, regular")]
        public IActionResult FindOneVideo(int id){
            ReadVideoDto readVideoDto = _videoService.FindOneVideo(id);

            if(readVideoDto == null) return NotFound("Video não encontrado");
            return Ok(readVideoDto);
        }

        [HttpGet]
        [Authorize(Roles = "admin, regular")]
        public IActionResult FindAllVideos([FromQuery] string search){
            List<ReadVideoDto> readVideoDto = _videoService.FindAllVideos(search);
            return Ok(readVideoDto);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "admin")]
        public IActionResult UpdateVideo(int id, [FromBody] UpdateVideoDto updateVideoDto){
            if (updateVideoDto.CategoriaId != 0)
            {
                ReadCategoriaDto readCategoriaDto = _categoriaService.FindOneCategoria(updateVideoDto.CategoriaId);

                if (readCategoriaDto == null)
                {
                    return NotFound("Categoria não encontrada");
                }
            }

            Result result = _videoService.UpdateVideo(id, updateVideoDto);
            if(result.IsFailed) return NotFound(result.Errors[0].Message);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "admin")]
        public IActionResult DeleteVideo(int id){
            Result result = _videoService.DeleteVideo(id);
            if(result.IsFailed) return NotFound(result.Errors[0].Message);
            return NoContent();
        }
    }
}