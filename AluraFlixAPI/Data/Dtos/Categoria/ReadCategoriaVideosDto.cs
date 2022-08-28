using System.ComponentModel.DataAnnotations;

namespace AluraFlixAPI.Data.Dtos
{
    public class ReadCategoriaVideosDto
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Cor { get; set; }
        public object Videos { get; set; } 
    }
}
