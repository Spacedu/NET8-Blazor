using Gestao.Domain;

namespace Gestao.Data.Repositories
{
    public class CompanyRepository
    {
        private readonly ApplicationDbContext _db;

        public CompanyRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        //CRUD
        //TODO - Fazer Paginação
        public List<Company> GetAll()
        {
            throw new NotImplementedException();
        }
        public Company Get(int id)
        {
            throw new NotImplementedException();
        }
        public void Add(Company company)
        {

        }
        public void Update(Company company)
        {

        }
        public void Delete(int id)
        {

        }
    }
}
