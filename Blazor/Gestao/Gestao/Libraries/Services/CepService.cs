using System.Net;

namespace Gestao.Libraries.Services
{
    public class CepService : ICepService
    {
        public async Task<LocalAddress?> SearchByPostalCode(string postalCode)
        {
            var url = $"https://viacep.com.br/ws/{postalCode.Replace(".", string.Empty).Replace("-", string.Empty)}/json/";

            var http = new HttpClient();
            return await http.GetFromJsonAsync<LocalAddress>(url);
        }
    }

    public class LocalAddress
    {
        public string Cep { get; set; } = string.Empty;
        public string Logradouro { get; set; } = string.Empty;
        public string Complemento { get; set; } = string.Empty;
        public string Bairro { get; set; } = string.Empty;
        public string Localidade { get; set; } = string.Empty;
        public string UF { get; set; } = string.Empty;
        public string IBGE { get; set; } = string.Empty;
        public string GIA { get; set; } = string.Empty;
        public string DDD { get; set; } = string.Empty;
        public string SIAFI { get; set; } = string.Empty;
    }
}
