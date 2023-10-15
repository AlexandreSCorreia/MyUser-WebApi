using System.ComponentModel.DataAnnotations;

namespace MyUserWebApi.Domain.Dtos.User
{
    public class UserDtoCreate
    {
        [Required(ErrorMessage = "Name é campo obrigatório")]
        [StringLength(60, ErrorMessage = "Name deve ter no máximo {1} caracteres.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Email é campo obrigatório")]
        [EmailAddress(ErrorMessage = "E-mail em formato inválido.")]
        [StringLength(100, ErrorMessage = "Email deve ter no máximo {1} caracteres.")]
        public string Email { get; set; }
    }
}