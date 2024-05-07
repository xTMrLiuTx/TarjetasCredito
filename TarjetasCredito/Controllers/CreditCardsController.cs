using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using TarjetasCredito.Models;

namespace TarjetasCredito.Controllers
{
    [ApiController]
	[Route("api/creditcards")]
	public class CreditCardsController : ControllerBase
	{
		private CreditCardManager _creditCardManager;
		LinkedList list = new LinkedList();

		public CreditCardsController(CreditCardManager creditCardManager)
		{
			_creditCardManager = creditCardManager;
		}

		[HttpGet("{id}")]
		public ActionResult<CreditCard> GetCreditCard(int id)
		{
			var creditCard = _creditCardManager.GetCreditCard(id);
			if (creditCard == null)
			{
				return NotFound();
			}

			return Ok(creditCard);
		}

		[HttpPost]
		public ActionResult<CreditCard> AddCreditCard(CreditCard creditCard)
		{
			_creditCardManager.AddCreditCard(creditCard);
			return CreatedAtAction(nameof(GetCreditCard), new { id = creditCard.Id }, creditCard);
		}

		[HttpDelete("{id}")]
		public ActionResult<CreditCard> RemoveCreditCard(int id)
		{
			var creditCard = _creditCardManager.RemoveCreditCard(id);
			if (creditCard == null)
			{
				return NotFound();
			}

			return Ok(creditCard);
		}

		[HttpGet]
		[Route("linkedlist")]
		public ActionResult<List<decimal>> getBalanceLinkedList()
		{
			List<decimal> newList = new List<decimal>();

			foreach (var item in _creditCardManager.creditCards)
			{
				list.insert(item.Balance);
			}

			var currentNode = list.getHead();

			while (currentNode != null)
			{
				newList.Add(currentNode.data);
				currentNode = currentNode.next;
			}

			return Ok(newList);
		}
	}

	[ApiController]
	[Route("api/creditcards/{creditCardId}/balance")]
	public class CreditCardBalanceController : ControllerBase
	{
		private CreditCardManager _creditCardManager;

		public CreditCardBalanceController(CreditCardManager creditCardManager)
		{
			_creditCardManager = creditCardManager;
		}

		[HttpGet]
		public ActionResult<decimal> GetBalance(int creditCardId)
		{
			var creditCard = _creditCardManager.GetCreditCard(creditCardId);
			if (creditCard == null)
			{
				return NotFound();
			}

			return Ok(creditCard.Balance);
		}

		[HttpPost]
		public ActionResult<decimal> UpdateBalance(int creditCardId, decimal newBalance)
		{
			var creditCard = _creditCardManager.GetCreditCard(creditCardId);
			if (creditCard == null)
			{
				return NotFound();
			}

			creditCard.Balance = newBalance;
			return Ok(creditCard.Balance);
		}
	}

	[ApiController]
	[Route("api/creditcards/{creditCardId}/payments")]
	public class CreditCardPaymentsController : ControllerBase
	{
		private CreditCardManager _creditCardManager;

		public CreditCardPaymentsController(CreditCardManager creditCardManager)
		{
			_creditCardManager = creditCardManager;
		}

		[HttpPost]
		public ActionResult<Payment> MakePayment(int creditCardId, Payment payment)
		{
			var creditCard = _creditCardManager.GetCreditCard(creditCardId);
			if (creditCard == null)
			{
				return NotFound();
			}

			creditCard.Payments.Enqueue(payment);
			creditCard.Balance -= payment.Amount;
			return Ok(payment);
		}
	}

	[ApiController]
	[Route("api/creditcards/{creditCardId}/statements")]
	public class CreditCardStatementsController : ControllerBase
	{
		private CreditCardManager _creditCardManager;

		public CreditCardStatementsController(CreditCardManager creditCardManager)
		{
			_creditCardManager = creditCardManager;
		}

		[HttpGet]
		public ActionResult<List<Payment>> GetStatements(string creditCardId)
		{
			var creditCard = _creditCardManager.GetCreditCardByBinarySearch(creditCardId);
			if (creditCard == null)
			{
				return NotFound();
			}

			return Ok(creditCard.Payments.ToList());
		}
	}

	[ApiController]
	[Route("api/creditcards/{creditCardId}/movements")]
	public class CreditCardMovementsController : ControllerBase
	{
		private CreditCardManager _creditCardManager;

		public CreditCardMovementsController(CreditCardManager creditCardManager)
		{
			_creditCardManager = creditCardManager;
		}

		[HttpPost]
		public ActionResult<decimal> MakeMovement(int creditCardId, decimal amount)
		{
			var creditCard = _creditCardManager.GetCreditCard(creditCardId);
			if (creditCard == null)
			{
				return NotFound();
			}

			creditCard.MovementHistory.Push(amount);
			creditCard.Balance += amount;
			return Ok(amount);
		}

		[HttpGet]
		public ActionResult<List<decimal>> GetMovements(int creditCardId)
		{
			var creditCard = _creditCardManager.GetCreditCard(creditCardId);
			if (creditCard == null)
			{
				return NotFound();
			}

			return Ok(creditCard.MovementHistory.ToList());
		}
	}

	[ApiController]
	[Route("api/creditcards/{creditCardId}/notifications")]
	public class CreditCardNotificationsController : ControllerBase
	{
		private CreditCardManager _creditCardManager;

		public CreditCardNotificationsController(CreditCardManager creditCardManager)
		{
			_creditCardManager = creditCardManager;
		}

		[HttpPost]
		public ActionResult<Notification> AddNotification(int creditCardId, Notification notification)
		{
			var creditCard = _creditCardManager.GetCreditCard(creditCardId);
			if (creditCard == null)
			{
				return NotFound();
			}

			creditCard.Notifications.Enqueue(notification);
			return Ok(notification);
		}

		[HttpGet]
		public ActionResult<Queue<Notification>> GetNotifications(int creditCardId)
		{
			var creditCard = _creditCardManager.GetCreditCard(creditCardId);
			if (creditCard == null)
			{
				return NotFound();
			}

			return Ok(creditCard.Notifications);
		}
	}

	[ApiController]
	[Route("api/creditcards/{creditCardId}/pin")]
	public class CreditCardPinController : ControllerBase
	{
		private CreditCardManager _creditCardManager;

		public CreditCardPinController(CreditCardManager creditCardManager)
		{
			_creditCardManager = creditCardManager;
		}

		[HttpGet]
		public ActionResult<List<int>> GetPinHistory(int creditCardId)
		{
			var creditCard = _creditCardManager.GetCreditCard(creditCardId);
			var newList = new List<int>();

			if (creditCard == null)
			{
				return NotFound();
			}

			var currentNode = creditCard.PinList.getHead();
			while (currentNode != null)
			{
				newList.Add(Convert.ToInt32(currentNode.data));
				currentNode = currentNode.next;
			}

			return newList;
		}

		[HttpPost]
		public ActionResult<CreditCard> ChangePin(int creditCardId, string newPin)
		{
			var creditCard = _creditCardManager.GetCreditCard(creditCardId);

			if (creditCard == null)
			{
				return NotFound();
			}

			creditCard.PinList.insert(Convert.ToDecimal(newPin));
			return Ok(creditCard);
		}
	}

	[ApiController]
	[Route("api/creditcards/{creditCardId}/block")]
	public class CreditCardBlockController : ControllerBase
	{
		private CreditCardManager _creditCardManager;

		public CreditCardBlockController(CreditCardManager creditCardManager)
		{
			_creditCardManager = creditCardManager;
		}

		[HttpGet]
		public ActionResult<bool> GetCreditCardStatus(string creditCardId)
		{
			var creditCard = _creditCardManager.GetCreditCardByBinarySearch(creditCardId);
			if (creditCard == null)
			{
				return NotFound();
			}
			
			return Ok(creditCard.IsBlocked);
		}

		[HttpPost]
		public ActionResult<CreditCard> BlockCard(string creditCardId)
		{
			var creditCard = _creditCardManager.GetCreditCardByBinarySearch(creditCardId);
			if (creditCard == null)
			{
				return NotFound();
			}

			creditCard.IsBlocked = true;
			return Ok(creditCard);
		}

		[HttpDelete]
		public ActionResult<CreditCard> UnblockCard(string creditCardId)
		{
			var creditCard = _creditCardManager.GetCreditCardByBinarySearch(creditCardId);
			if (creditCard == null)
			{
				return NotFound();
			}

			creditCard.IsBlocked = false;
			return Ok(creditCard);
		}
	}

	[ApiController]
	[Route("api/creditcards/{creditCardId}/limit")]
	public class CreditCardLimitController : ControllerBase
	{
		private CreditCardManager _creditCardManager;

		public CreditCardLimitController(CreditCardManager creditCardManager)
		{
			_creditCardManager = creditCardManager;
		}

		[HttpGet]
		public ActionResult<List<decimal>> GetCreditLimits(int creditCardId)
		{
			var creditCard = _creditCardManager.GetCreditCard(creditCardId);
			if (creditCard == null)
			{
				return NotFound();
			}

			return Ok(creditCard.CreditLimits.ToList());
		}

		[HttpGet]
		[Route("last")]
		public ActionResult<decimal> GetLastCreditLimit(int creditCardId)
		{
			var creditCard = _creditCardManager.GetCreditCard(creditCardId);
			if (creditCard == null)
			{
				return NotFound();
			}

			return Ok(creditCard.CreditLimits.Peek().limit);
		}

		[HttpPost]
		public ActionResult<CreditCard> IncreaseLimit(int creditCardId, decimal newLimit)
		{
			var creditCard = _creditCardManager.GetCreditCard(creditCardId);
			if (creditCard == null)
			{
				return NotFound();
			}

			// Increase limit logic here
			var creditLimit = new CreditLimit(newLimit);
			creditCard.CreditLimits.Push(creditLimit);

			return Ok(creditCard);
		}
	}
}
