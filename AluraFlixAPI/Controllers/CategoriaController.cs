using AluraFlixAPI.Data.Dtos;
using AluraFlixAPI.Helpers;
using AluraFlixAPI.Services;
using FluentResults;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace AluraFlixAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CategoriaController : ControllerBase
    {
        private CategoriaService _categoriaService;

        public CategoriaController(CategoriaService categoriaService)
        {
            _categoriaService = categoriaService;
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public IActionResult CreateCategoria([FromBody] CreateCategoriaDto createCategoriaDto)
        {
            ReadCategoriaDto categoria = _categoriaService.CreateCategoria(createCategoriaDto);

            return CreatedAtAction(nameof(FindOneCategoria), new {Id = categoria.Id}, categoria);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "admin, regular")]
        public IActionResult FindOneCategoria(int id)
        {
            ReadCategoriaDto readCategoriaDto = _categoriaService.FindOneCategoria(id);

            if (readCategoriaDto == null) return NotFound("Categoria não encontrada");
            return Ok(readCategoriaDto);
        }

        [HttpGet("{id}/videos")]
        [Authorize(Roles = "admin, regular")]
        public IActionResult FindOneCategoriaAndVideos(int id)
        {
            ReadCategoriaVideosDto readCategoriaVideosDto = _categoriaService.FindOneCategoriaVideos(id);

            if (readCategoriaVideosDto == null) return NotFound("Categoria não encontrada");
            return Ok(readCategoriaVideosDto);
        }

        [HttpGet]
        [Authorize(Roles = "admin, regular")]
        public IActionResult FindAllCategoria([FromQuery] int page = 1)
        {
            PagedList<ReadCategoriaDto> categorias = _categoriaService.FindAllCategorias(page);

            var metaData = new
            {
                categorias.CurrentPage,
                categorias.PageSize,
                categorias.TotalCount,
                categorias.TotalPages
            };

            var data = new
            {
                pagination = metaData,
                categorias
            };

            Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(metaData));

            return Ok(data);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "admin")]
        public IActionResult UpdateCategoria(int id, [FromBody] UpdateCategoriaDto updateCategoriaDto)
        {
            Result result = _categoriaService.UpdateCategoria(id, updateCategoriaDto);
            if (result.IsFailed) return NotFound(result.Errors[0].Message);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "admin")]
        public IActionResult DeleteCategoria(int id)
        {
            Result result = _categoriaService.RemoveCategoria(id);
            if (result.IsFailed) return NotFound(result.Errors[0].Message);
            return NoContent();
        }
    }
}
