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
        private SignInManager<IdentityUser<int>> _signInManager;
        private IMapper _mapper;

        public UsuarioService(UserManager<IdentityUser<int>> userManager, SignInManager<IdentityUser<int>> signInManager, IMapper mapper)
        {
            _userManager = userManager;
            _signInManager = signInManager;
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

        public Result Login(LoginRequest loginRequest)
        {
            var user = _userManager.FindByEmailAsync(loginRequest.Email);

            if(user == null)
            {
                return Result.Fail("Email/senha incorreto!");
            }

            var result = _signInManager.PasswordSignInAsync(user.Result.UserName, loginRequest.Password, false, false);

            if (result.Result.Succeeded)
            {

                return Result.Ok();

            }

            return Result.Fail("Email/senha incorreto!");
        }
    }
}
