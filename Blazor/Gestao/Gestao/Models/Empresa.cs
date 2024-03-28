namespace Gestao.Models
{
    public class Empresa : Base
    {
        public string RazaoSocial { get; set; } = null!;
        public string NomeFantasia { get; set; } = null!;
        public string CNPJ { get; set; } = null!;
        public string CEP { get; set; } = null!;
        public string Estado { get; set; } = null!;
        public string Cidade { get; set; } = null!;
        public string Bairro { get; set; } = null!;
        public string Endereco { get; set; } = null!;
        public string Complemento { get; set; } = null!;

        public string? UsuarioId { get; set; }
        public Usuario? Usuario { get; set; }
    }
}
