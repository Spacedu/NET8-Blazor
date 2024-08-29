using Coravel.Invocable;
using Gestao.Domain;
using Gestao.Domain.Repositories;

namespace Gestao.Libraries.Queues
{
    public class FinancialTransactionRepeatInvocable : IInvocable, IInvocableWithPayload<FinancialTransaction>
    {
        private IFinancialTransactionRepository _repository;

        public FinancialTransactionRepeatInvocable(IFinancialTransactionRepository repository)
        {
            _repository = repository;
        }

        public FinancialTransaction Payload { get; set; }

        public async Task Invoke()
        {
            int countTransactionsSameGroup = await _repository.GetCountTransactionsSameGroup(Payload.Id);

            //TODO - Queue -> Criar Grupo (Id = Id da primeira parcela).

            //TODO - Cadastrar -> Novas transações
            //TODO - Editando -> None > 0parc -> 10parc -> Novas transações
            var startPoint = 1;
            RegisterNewTransactions(startPoint);

            //TODO - Editando -> 5parc -> 10parc -> Novas transações (6-10).
            RegisterNewTransactions(countTransactionsSameGroup);

            //TODO - Editando -> 10parc -> 7parc -> Excluir (10-8).
            if(Payload.Repeat != Domain.Enums.Recurrence.None && countTransactionsSameGroup > Payload.RepeatTimes)
            {
                var transactions = await _repository.GetTransactionsSameGroup(Payload.Id);
                for (int i = countTransactionsSameGroup; i > Payload.RepeatTimes; i--)
                {
                    //TODO - Remover Parcelas
                    await _repository.Delete(transactions.ElementAt(i));
                }
            }

            //TODO - Editando -> 10parc -> 0parc -> Excluir (2-10) -> Repeat = None.
            if(Payload.Repeat == Domain.Enums.Recurrence.None && countTransactionsSameGroup > 1)
            {
                var transactions = await _repository.GetTransactionsSameGroup(Payload.Id);
                for (int i = 2; i <= countTransactionsSameGroup; i++)
                {
                    await _repository.Delete(transactions.ElementAt(i));
                }
            }
        }

        private void RegisterNewTransactions(int startPoint)
        {
            if (Payload.Repeat != Domain.Enums.Recurrence.None)
            {
                var repeatTimes = Payload.RepeatTimes - 1;


                for (int i = startPoint; i <= repeatTimes; i++)
                {
                    var financial = new FinancialTransaction();
                    financial.TypeFinancialTransaction = Payload.TypeFinancialTransaction;
                    financial.Description = Payload.Description;
                    financial.ReferenceDate = Payload.ReferenceDate; //TODO - Recalcular a data
                    financial.DueDate = Payload.DueDate; //TODO - Recalcular a data
                    financial.Amount = Payload.Amount;
                    financial.RepeatGroup = Payload.Id;
                    financial.Repeat = Domain.Enums.Recurrence.None;
                    financial.RepeatTimes = null;
                    financial.CreatedAt = DateTimeOffset.Now;

                    financial.CompanyId = Payload.CompanyId;
                    financial.AccountId = Payload.AccountId;
                    financial.CategoryId = Payload.CategoryId;

                    _repository.Add(financial);
                }
            }
        }
    }
}
