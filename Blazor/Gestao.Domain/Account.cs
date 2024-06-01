using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gestao.Domain
{
    public class Account
    {
        public int Id { get; set; }
        public string Description { get; set; } = string.Empty;
        public decimal Balance { get; set; }
        public DateTimeOffset BalanceDate { get; set; }
        public int? CompanyId { get; set; }
        public Company? Company { get; set; }
    }
}
