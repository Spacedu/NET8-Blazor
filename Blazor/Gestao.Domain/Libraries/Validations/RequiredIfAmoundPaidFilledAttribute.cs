using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gestao.Domain.Libraries.Validations
{
    public class RequiredIfAmoundPaidFilledAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            FinancialTransaction transaction = (FinancialTransaction)validationContext.ObjectInstance;

            if (transaction.AmoundPaid.HasValue)
            {
                if (value is null)
                    return new ValidationResult("O campo é obrigatório!", new[] { validationContext.MemberName! });
            }

            return ValidationResult.Success;
        }
    }
}
