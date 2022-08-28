using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace AluraFlixAPI.Models {

    public class Video {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string Titulo { get; set; }
        [Required]
        [StringLength(200)]
        public string Descricao { get; set; }
        [Required]
        [Url]
        public string Url { get; set; }
        [Required]
        [DefaultValue(1)]
        public int CategoriaId { get; set; }
        public virtual Categoria Categoria { get; set; }

    }
}