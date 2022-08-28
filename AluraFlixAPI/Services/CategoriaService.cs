using AluraFlixAPI.Data;
using AluraFlixAPI.Data.Dtos.Categoria;
using AluraFlixAPI.Models;
using AutoMapper;
using System.Linq;

namespace AluraFlixAPI.Services
{
    public class CategoriaService
    {
        private AppDbContext _context;
        private IMapper _mapper;

        public CategoriaService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public ReadCategoriaDto CreateCategoria(CreateCategoriaDto createCategoriaDto)
        {
            Categoria categoria = _mapper.Map<Categoria>(createCategoriaDto);

            _context.Categorias.Add(categoria);
            _context.SaveChanges();

            return _mapper.Map<ReadCategoriaDto>(categoria);
        }

        public ReadCategoriaDto FindOneCategoria(int id)
        {
            Categoria categoria = _context.Categorias.FirstOrDefault(c => c.Id == id);
            return _mapper.Map<ReadCategoriaDto>(categoria);
        }
    }
}
