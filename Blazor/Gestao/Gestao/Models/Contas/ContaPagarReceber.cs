using Gestao.Models.Enums;

namespace Gestao.Models.Contas
{
    public class ContaPagarReceber : Base
    {
        public TipoConta Tipo { get; set; }
        public string Descricao { get; set; } = null!;
        
        public int? CategoriaId { get; set; }
        public Categoria? Categoria { get; set; }

        public int? ContaId { get; set; }
        public Conta? Conta { get; set; }

        public DateTimeOffset DataCompetencia { get; set; }
        public DateTimeOffset DataVencimento { get; set; }
        public decimal Valor { get; set; }

        public decimal JurosMultas { get; set; }
        public decimal DescontosTaxas { get; set; }
        public DateTimeOffset DataPagamento { get; set; }
        public decimal ValorPago { get; set; }
        public string? Observacao { get; set; }

        public virtual ICollection<Arquivo>? Arquivos { get; set; }

        public int? EmpresaId { get; set; }
        public Empresa? Empresa { get; set; }
    }
}
