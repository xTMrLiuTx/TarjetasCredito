namespace TarjetasCredito.Models
{
	public class Node
	{
		public decimal data { get; set; }
		public Node next { get; set; }

		public Node(decimal value) {
			data = value;
			next = null;
		}
	}
}
