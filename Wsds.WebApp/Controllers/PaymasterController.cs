using Microsoft.AspNetCore.Mvc;
using Wsds.WebApp.Models;

namespace Wsds.WebApp.Controllers
{
    public class PaymasterController : Controller
    {
        
        [HttpGet]
        public IActionResult Payment(PaymentModel payment)
        {
            if (payment == null || !TryValidateModel(payment))
                return BadRequest("������ ��������� ������ � ������� � ��������� �������");


            //return Content("degufgrrufg");
            return View("Payment", payment);
        }
    }
}