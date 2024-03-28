namespace Gestao.Models.Contas
{
    public class Conta : Base
    {
        public string Descricao { get; set; } = null!;
        public decimal Saldo { get; set; }
        public DateTimeOffset DataSaldo { get; set; }

        public int? EmpresaId { get; set; }
        public Empresa? Empresa { get; set; }
    }
}
