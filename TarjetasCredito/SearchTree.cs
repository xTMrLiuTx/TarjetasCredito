using TarjetasCredito.Models;

namespace TarjetasCredito
{
	public class SearchTree
	{
		private TreeNode raiz;

		public SearchTree()
		{
			raiz = null;
		}

		public void Insertar(CreditCard creditCard)
		{
			raiz = InsertarRecursivo(raiz, creditCard);
		}

		private TreeNode InsertarRecursivo(TreeNode nodo, CreditCard creditCard)
		{
			if (nodo == null)
			{
				nodo = new TreeNode(creditCard);
				return nodo;
			}

			if (creditCard.Id.CompareTo(nodo.CreditCard.Id) < 0)
			{
				nodo.Izquierda = InsertarRecursivo(nodo.Izquierda, creditCard);
			}
			else if (creditCard.Id.CompareTo(nodo.CreditCard.Id) > 0)
			{
				nodo.Derecha = InsertarRecursivo(nodo.Derecha, creditCard);
			}
			else
			{
				return nodo;
			}

			return nodo;
		}

		public CreditCard Buscar(string cardId)
		{
			return BuscarRecursivo(raiz, cardId);
		}

		private CreditCard BuscarRecursivo(TreeNode nodo, string cardId)
		{
			if (nodo == null)
			{
				return null;
			}

			if (cardId.CompareTo(nodo.CreditCard.Id.ToString()) == 0)
			{
				return nodo.CreditCard;
			}
			else if (cardId.CompareTo(nodo.CreditCard.Id.ToString()) < 0)
			{
				return BuscarRecursivo(nodo.Izquierda, cardId);
			}
			else
			{
				return BuscarRecursivo(nodo.Derecha, cardId);
			}
		}
	}
}
