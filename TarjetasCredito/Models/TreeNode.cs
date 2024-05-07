namespace TarjetasCredito.Models
{
	public class TreeNode
	{
		public CreditCard CreditCard { get; set; }
		public TreeNode Izquierda { get; set; }
		public TreeNode Derecha { get; set; }

		public TreeNode(CreditCard card)
		{
			CreditCard = card;
			Izquierda = null;
			Derecha = null;
		}
	}
}
