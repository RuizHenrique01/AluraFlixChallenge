using System.ComponentModel.DataAnnotations;

namespace AluraFlixAPI.Data.Dtos
{
    public class ReadCategoriaDto
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Cor { get; set; }
    }
}
