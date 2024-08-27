using Coravel.Invocable;
using Gestao.Domain;

namespace Gestao.Libraries.Queues
{
    public class FinancialTransactionRepeatInvocable : IInvocable, IInvocableWithPayload<FinancialTransaction>
    {
        public FinancialTransaction Payload { get; set; }

        public Task Invoke()
        {
            //Payload.RepeatTimes - 1;
            throw new NotImplementedException();
            //TODO - Queue -> Criar as Transação(parcelas,contas recorrentes).

            //TODO - Queue -> Criar Grupo.
        }
    }
}
