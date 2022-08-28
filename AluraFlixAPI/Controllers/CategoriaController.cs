using AluraFlixAPI.Data.Dtos;
using AluraFlixAPI.Data.Dtos.Categoria;
using AluraFlixAPI.Services;
using FluentResults;
using Microsoft.AspNetCore.Mvc;

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
        public IActionResult CreateCategoria([FromBody] CreateCategoriaDto createCategoriaDto)
        {
            ReadCategoriaDto categoria = _categoriaService.CreateCategoria(createCategoriaDto);

            return CreatedAtAction(nameof(FindOneCategoria), new {Id = categoria.Id}, categoria);

        }

        [HttpGet("{id}")]
        public IActionResult FindOneCategoria(int id)
        {
            ReadCategoriaDto readCategoriaDto = _categoriaService.FindOneCategoria(id);

            if (readCategoriaDto == null) return NotFound("Categoria não encontrada");
            return Ok(readCategoriaDto);
        }

        [HttpGet]
        public IActionResult FindAllCategoria()
        {
            return Ok(_categoriaService.FindAllCategorias());
        }

        [HttpPut("{id}")]
        public IActionResult UpdateCategoria(int id, [FromBody] UpdateCategoriaDto updateCategoriaDto)
        {
            Result result = _categoriaService.UpdateCategoria(id, updateCategoriaDto);
            if (result.IsFailed) return NotFound(result.Errors[0].Message);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteCategoria(int id)
        {
            Result result = _categoriaService.RemoveCategoria(id);
            if (result.IsFailed) return NotFound(result.Errors[0].Message);
            return NoContent();
        }
    }
}
