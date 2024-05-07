namespace TarjetasCredito.Models
{
    public class CreditLimit
    {
        public decimal limit { get; set; }
        public DateTimeOffset date { get; set; }

        public CreditLimit(decimal limit)
        {
            this.limit = limit;
            date = DateTimeOffset.Now;
        }
    }
}
