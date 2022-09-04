using AluraFlixAPI.Data.Dtos;
using AluraFlixAPI.Models;
using AutoMapper;
using FluentResults;
using Microsoft.AspNetCore.Identity;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace AluraFlixAPI.Services
{
    public class UsuarioService
    {
        private UserManager<IdentityUser<int>> _userManager;
        private SignInManager<IdentityUser<int>> _signInManager;
        private IMapper _mapper;
        private TokenService _tokenService;

        public UsuarioService(UserManager<IdentityUser<int>> userManager, SignInManager<IdentityUser<int>> signInManager, IMapper mapper, TokenService tokenService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _mapper = mapper;
            _tokenService = tokenService;
        }

        public Result CreateUsuario(CreateUsuarioDto createUsuarioDto)
        {
            Usuario usuario = _mapper.Map<Usuario>(createUsuarioDto);
            IdentityUser<int> identityUser = _mapper.Map<IdentityUser<int>>(usuario);
            var identityResult = _userManager.CreateAsync(identityUser, createUsuarioDto.Password).Result;
            var userRole = _userManager.AddToRoleAsync(identityUser, "regular").Result;
            if (identityResult.Succeeded) return Result.Ok();
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

                var roleIdentity = _signInManager.UserManager.GetRolesAsync(user.Result).Result.FirstOrDefault();

                Token token = _tokenService.CreateToken(user.Result, roleIdentity);

                return Result.Ok().WithSuccess(token.Value);

            }

            return Result.Fail("Email/senha incorreto!");
        }

        public Result Logout()
        {
            var resultado = _signInManager.SignOutAsync();
            if (resultado.IsCompletedSuccessfully) return Result.Ok();
            return Result.Fail("Falha no Logout!");
        }
    }
}
