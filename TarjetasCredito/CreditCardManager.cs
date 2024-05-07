using System.Text.Json;

namespace TarjetasCredito
{
	public class CreditCardManager
	{
		public List<CreditCard>? creditCards = new List<CreditCard>();

		public CreditCardManager() { 
			LoadData();
		}
		public void LoadData()
		{
			try
			{
				// Lee el contenido del archivo JSON como texto
				string content = File.ReadAllText("Models/dataCreditCards.json");

				// Deserializa el contenido JSON en una lista de personas
				creditCards = JsonSerializer.Deserialize<List<CreditCard>>(content);
			}
			catch (Exception ex)
			{
				Console.WriteLine($"JSON error: {ex.Message}");
			}
		}

		public CreditCard GetCreditCard(int id)
		{
			//Get Credit card logic
			return creditCards.FirstOrDefault(i => i.Id == id);
		}

		public void AddCreditCard(CreditCard creditCard)
		{
			//Add credit card logic
			creditCards.Add(creditCard);
		}

		public CreditCard RemoveCreditCard(int id)
		{
			//Remove credit card logic
			var creditCard = creditCards.FirstOrDefault(c => c.Id == id);
			if (creditCard != null)
			{
				creditCards.Remove(creditCard);
			}
			return creditCard;
		}

		public CreditCard GetCreditCardByBinarySearch(string creditCardId)
		{
			var creditCardTree = CreateBinaryTree();

			CreditCard creditCard = creditCardTree.Buscar(creditCardId);

			return creditCard;
		}

		public SearchTree CreateBinaryTree()
		{
			SearchTree cardsTree = new SearchTree();

            foreach (var card in creditCards)
            {
				cardsTree.Insertar(card);
            }

			return cardsTree;
        }
	}
}
