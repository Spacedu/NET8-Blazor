using Gestao.Models.Enums;

namespace Gestao.Models.Contas
{
    public class GrupoContaRecorrente : Base
    {
        public Frequencia Repetir { get; set; }
        public ushort Vezes { get; set; }
    }
}
