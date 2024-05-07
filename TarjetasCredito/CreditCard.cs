using TarjetasCredito.Models;

namespace TarjetasCredito
{
    public class CreditCard
	{
		public int Id { get; set; }
		public string CardNumber { get; set; }
		public decimal Balance { get; set; }
		public DateTimeOffset ExpirationDate { get; set; }
		public bool IsBlocked { get; set; }
		public Stack<decimal> MovementHistory { get; set; }
		public Queue<Notification> Notifications { get; set; }
		public Queue<Payment> Payments { get; set; }
		//public BinarySearchTree<Payment> Payments { get; set; }
		public LinkedList PinList { get; set; }
		public Stack<CreditLimit> CreditLimits { get; set; }

		public CreditCard(int id, string cardNumber, decimal balance, DateTimeOffset expirationDate)
		{
			Id = id;
			CardNumber = cardNumber;
			Balance = balance;
			ExpirationDate = expirationDate;
			MovementHistory = new Stack<decimal>();
			Notifications = new Queue<Notification>();
			//Payments = new BinarySearchTree<Payment>();
			Payments = new Queue<Payment>();
			PinList = new LinkedList();
			CreditLimits = new Stack<CreditLimit>();
		}
	}
}
