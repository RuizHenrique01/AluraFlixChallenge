using AluraFlixAPI.Data.Dtos;
using AluraFlixAPI.Models;
using AutoMapper;
using System.Linq;

namespace AluraFlixAPI.Profiles
{
    public class CategoriaProfile : Profile
    {
        public CategoriaProfile()
        {
            CreateMap<CreateCategoriaDto, Categoria>();
            CreateMap<Categoria, ReadCategoriaDto>();
            CreateMap<Categoria, ReadCategoriaVideosDto>()
                .ForMember(categoria => categoria.Videos, opts => 
                opts.MapFrom(categoria => categoria.Videos.Select(c => new {c.Id, c.Titulo, c.Descricao, c.Url})));
            CreateMap<UpdateCategoriaDto, Categoria>();
        }
    }
}
