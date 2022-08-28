using AluraFlixAPI.Data.Dtos.Categoria;
using AluraFlixAPI.Models;
using AutoMapper;

namespace AluraFlixAPI.Profiles
{
    public class CategoriaProfile : Profile
    {
        public CategoriaProfile()
        {
            CreateMap<CreateCategoriaDto, Categoria>();
            CreateMap<Categoria, ReadCategoriaDto>();
            CreateMap<UpdateCategoriaDto, Categoria>();
        }
    }
}
