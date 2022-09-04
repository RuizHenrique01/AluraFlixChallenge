using System.ComponentModel.DataAnnotations;
using System;

namespace AluraFlixAPI.Data.Dtos
{
    public class CreateUsuarioDto
    {
        [Required]
        [StringLength(50)]
        public string Username { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        [Compare("Password")]
        public string RePassword { get; set; }
        [Required]
        public DateTime DataNascimento { get; set; }
    }
}
