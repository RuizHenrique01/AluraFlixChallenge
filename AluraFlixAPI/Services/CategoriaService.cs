using AluraFlixAPI.Data;
using AluraFlixAPI.Data.Dtos;
using AluraFlixAPI.Helpers;
using AluraFlixAPI.Models;
using AutoMapper;
using FluentResults;
using System;
using System.Collections.Generic;
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

        public PagedList<ReadCategoriaDto> FindAllCategorias(int page = 1)
        {
            List<Categoria> categorias = _context.Categorias.ToList();
            return PagedList<ReadCategoriaDto>.ToPagedList(_mapper.Map<List<ReadCategoriaDto>>(categorias), page, 5);
        }

        public Result UpdateCategoria(int id, UpdateCategoriaDto updateCategoriaDto)
        {
            Categoria categoria = _context.Categorias.FirstOrDefault(c => c.Id == id);

            if (categoria == null)
                return Result.Fail("Categoria não encontrada");

            _mapper.Map(updateCategoriaDto, categoria);
            _context.SaveChanges();

            return Result.Ok();
        }

        public Result RemoveCategoria(int id)
        {
            Categoria categoria = _context.Categorias.FirstOrDefault(c => c.Id == id);

            if (categoria == null)
                return Result.Fail("Categoria não encontrada");

            _context.Categorias.Remove(categoria);
            _context.SaveChanges();

            return Result.Ok();
        }

        public ReadCategoriaVideosDto FindOneCategoriaVideos(int id)
        {
            Categoria categoria = _context.Categorias.FirstOrDefault(c => c.Id == id);
            return _mapper.Map<ReadCategoriaVideosDto>(categoria);
        }
    }
}
