
namespace Gestao.Libraries.Services
{
    public interface ICepService
    {
        Task<LocalAddress?> SearchByPostalCode(string postalCode);
    }
}