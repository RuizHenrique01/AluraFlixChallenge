using System.ComponentModel.DataAnnotations;

namespace AluraFlixAPI.Data.Dtos
{
    public class CreateCategoriaDto
    {
        [Required]
        [StringLength(50, ErrorMessage = "O título não pode passar de 50 caracteres.")]
        public string Titulo { get; set; }
        [Required]
        [RegularExpression("^#([A-Fa-f0-9]{6}|[A-Fa-f0-9]{3})$", ErrorMessage = "A cor deve ser um número na base hexadecimal")]
        public string Cor { get; set; }
    }
}
