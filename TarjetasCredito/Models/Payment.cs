namespace TarjetasCredito.Models
{
	public class Payment : IComparable<Payment>
	{
		public DateTimeOffset Timestamp { get; set; }
		public decimal Amount { get; set; }

		public int CompareTo(Payment other)
		{
			return Timestamp.CompareTo(other.Timestamp);
		}
	}
}
