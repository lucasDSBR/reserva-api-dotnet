using System.ComponentModel.DataAnnotations;

namespace Api.Domain.Dtos
{
    public class LoginDto
    {
        [Required(ErrorMessage="Email é um campo obrigatório para o login")]
        [EmailAddress(ErrorMessage="Email em formato inválido")]
        public string Email { get; set; }
    }
}