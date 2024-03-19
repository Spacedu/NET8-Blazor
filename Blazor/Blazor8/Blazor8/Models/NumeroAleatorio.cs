namespace Blazor8.Models
{
    public class NumeroAleatorio
    {
        public int NumeroIdentitificador { get; set; }
        public NumeroAleatorio()
        {
            Random rnd = new Random();
            NumeroIdentitificador = rnd.Next(0, 10000);
        }
    }
}
