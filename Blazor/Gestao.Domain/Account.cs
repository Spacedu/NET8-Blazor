using Gestao.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gestao.Domain
{
    public class Account : ISoftDelete
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "O campo 'Descrição/Nome' é obrigatório!")]
        [MinLength(3, ErrorMessage = "O campo 'Descrição/Nome' deve ter pelo menos {1} caracteres!")]
        public string Description { get; set; } = string.Empty;
        [Required(ErrorMessage = "O campo 'Saldo inicial' é obrigatório!")]
        public decimal Balance { get; set; }
        [Required(ErrorMessage = "O campo 'Data de abertura da conta' é obrigatório!")]
        public DateTimeOffset BalanceDate { get; set; }
        public int? CompanyId { get; set; }
        public Company? Company { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset? DeletedAt { get; set; }
        public ICollection<FinancialTransaction>? FinancialTransactions { get; set; }
    }
}
