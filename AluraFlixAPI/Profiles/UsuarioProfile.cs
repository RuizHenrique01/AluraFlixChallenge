using AluraFlixAPI.Data.Dtos;
using AluraFlixAPI.Models;
using AutoMapper;

namespace AluraFlixAPI.Profiles
{
    public class UsuarioProfile : Profile
    {
        public UsuarioProfile()
        {
            CreateMap<CreateUsuarioDto, Usuario>();
        }
    }
}
