using Gestao.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gestao.Domain
{
    public class Document : ISoftDelete
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Path { get; set; } = null!;// /wwwroot/files/transactions/1/comprovante.pdf
        public int? FinancialTransactionId { get; set; }
        public FinancialTransaction? FinancialTransaction { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset? DeletedAt { get; set; }
    }
}
