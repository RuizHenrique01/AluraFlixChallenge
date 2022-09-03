using AluraFlixAPI.Data.Dtos;
using AluraFlixAPI.Models;
using AutoMapper;
using Microsoft.AspNetCore.Identity;

namespace AluraFlixAPI.Profiles
{
    public class UsuarioProfile : Profile
    {
        public UsuarioProfile()
        {
            CreateMap<CreateUsuarioDto, Usuario>();
            CreateMap<Usuario, IdentityUser<int>>();
        }
    }
}
