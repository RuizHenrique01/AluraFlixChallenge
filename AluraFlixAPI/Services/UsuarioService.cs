using AluraFlixAPI.Data.Dtos;
using AluraFlixAPI.Models;
using AutoMapper;
using FluentResults;
using Microsoft.AspNetCore.Identity;
using System;
using System.Threading.Tasks;

namespace AluraFlixAPI.Services
{
    public class UsuarioService
    {
        private UserManager<IdentityUser<int>> _userManager;
        private IMapper _mapper;

        public UsuarioService(UserManager<IdentityUser<int>> userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }

        public Result CreateUsuario(CreateUsuarioDto createUsuarioDto)
        {
            Usuario usuario = _mapper.Map<Usuario>(createUsuarioDto);
            IdentityUser<int> identityUser = _mapper.Map<IdentityUser<int>>(usuario);
            Task<IdentityResult> identityResult = _userManager.CreateAsync(identityUser, createUsuarioDto.Password);
            if (identityResult.Result.Succeeded) return Result.Ok();
            return Result.Fail("Erro ao criar usuário!");
        } 
    }
}
