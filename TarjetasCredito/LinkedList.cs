using TarjetasCredito.Models;

namespace TarjetasCredito
{
	public class LinkedList
	{
		private Node head;

		public LinkedList() {
			head = null;
		}

		public Node getLastNode(Node node)
		{
			if (node.next == null)
			{
				return node;
			}
			else
			{
				return getLastNode(node.next);
			}
		}

		public Node getHead()
		{
			return head;
		}
		public void insert(decimal value)
		{
			Node newNode = new Node(value);
			
			if(head == null)
			{
				head = newNode;
			}
			else
			{
				Node aux = getLastNode(head);
				aux.next = newNode;
			}
		}
	}
}
