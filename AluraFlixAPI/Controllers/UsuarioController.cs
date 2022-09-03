using AluraFlixAPI.Data.Dtos;
using AluraFlixAPI.Services;
using FluentResults;
using Microsoft.AspNetCore.Mvc;

namespace AluraFlixAPI.Controllers
{
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private UsuarioService _usuarioService;

        public UsuarioController(UsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [Route("/signup")]
        [HttpPost]
        public IActionResult CreateUsuario([FromBody] CreateUsuarioDto createUsuarioDto)
        {
            Result result = _usuarioService.CreateUsuario(createUsuarioDto);
            if(result.IsFailed) return BadRequest(result.Errors[0]);
            return Ok();

        }
    }
}
