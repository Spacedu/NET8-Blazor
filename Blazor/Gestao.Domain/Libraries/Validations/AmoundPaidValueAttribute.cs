using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gestao.Domain.Libraries.Validations
{
    public class AmoundPaidValueAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value == null)
                return ValidationResult.Success;

            FinancialTransaction transaction = (FinancialTransaction)validationContext.ObjectInstance;

            decimal total = 0;
            if(transaction.Amount.HasValue)
            {
                total = transaction.Amount.Value;

                if (transaction.InterestPenalty.HasValue)
                {
                    total += transaction.InterestPenalty.Value;
                }

                if (transaction.Discount.HasValue)
                {
                    total -= transaction.Discount.Value;
                }

                if(total != transaction.AmoundPaid)
                {
                    return new ValidationResult($"Valor incorreto, deveria ser: {total.ToString("C")}, verifique os campos 'Valor', 'Juros/Multas' e 'Desconto'.", new[] { validationContext.MemberName! });
                }
            }

            return ValidationResult.Success;
        }
    }
}
