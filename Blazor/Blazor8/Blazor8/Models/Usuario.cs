using System.ComponentModel.DataAnnotations;

namespace Blazor8.Models
{
    public class Usuario
    {
        public int Id { get; set; }
        public string? Nome { get; set; }
        [Required(ErrorMessage = "O campo 'E-mail' é obrigatório!")]
        [EmailAddress(ErrorMessage = "O campo 'E-mail' é inválido!")]
        public string? Email { get; set; }
        [Required(ErrorMessage = "O campo 'Senha' é obrigatório!")]
        [MinLength(8, ErrorMessage = "O campo 'Senha' deve ter pelo menos 8 caracteres!")]
        public string? Senha { get; set; }
    }
}
