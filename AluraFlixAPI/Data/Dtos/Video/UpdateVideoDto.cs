using System.ComponentModel.DataAnnotations;

namespace AluraFlixAPI.Data.Dtos {

    public class UpdateVideoDto {
        [Required(ErrorMessage = "Título é um campo obrigatório.")]
        [StringLength(50, ErrorMessage = "O título não pode passar de 50 caracteres.")]
        public string Titulo { get; set; }
        [Required(ErrorMessage = "Descrição é um campo obrigatório")]
        [StringLength(200, ErrorMessage = "O título não pode passar de 200 caracteres.")]
        public string Descricao { get; set; }
        [Required(ErrorMessage = "Url é um campo obrigatório")]
        [Url]
        public string Url { get; set; }
        [Required(ErrorMessage = "Categoria é um campo obrigatório")]
        public int CategoriaId { get; set; }
    }
}