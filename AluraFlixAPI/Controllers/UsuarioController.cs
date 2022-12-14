using AluraFlixAPI.Data.Dtos;
using AluraFlixAPI.Services;
using FluentResults;
using Microsoft.AspNetCore.Authorization;
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

        [Route("/login")]
        [HttpPost]
        public IActionResult LoginUsuario([FromBody] LoginRequest loginRequest)
        {
            Result result = _usuarioService.Login(loginRequest);
            if (result.IsFailed) return BadRequest(result.Errors[0]);
            return Ok(result.Successes[0]);
        }

        [Route("/logout")]
        [HttpPost]
        [Authorize(Roles = "admin, regular")]
        public IActionResult LogoutUsuario()
        {
            Result result = _usuarioService.Logout();
            if (result.IsFailed) return Unauthorized(result.Errors[0]);
            return Ok(result.Successes);
        }
    }
}
