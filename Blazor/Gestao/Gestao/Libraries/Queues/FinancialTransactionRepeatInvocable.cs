using Coravel.Invocable;
using Gestao.Domain;

namespace Gestao.Libraries.Queues
{
    public class FinancialTransactionRepeatInvocable : IInvocable, IInvocableWithPayload<FinancialTransaction>
    {
        public FinancialTransaction Payload { get; set; }

        public Task Invoke()
        {
            //TODO - Queue -> Criar Grupo (Id = Id da primeira parcela).

            //TODO - Cadastrar -> Novas transações
            //TODO - Editando -> 0parc -> 10parc -> Novas transações
            //TODO - Editando -> 5parc -> 10parc -> Novas transações (6-10).
            //TODO - Editando -> 10parc -> 7parc -> Excluir (10-8).
            //TODO - Editando -> 10parc -> 0parc -> Excluir (2-10) -> Repeat = None.
        }
    }
}
