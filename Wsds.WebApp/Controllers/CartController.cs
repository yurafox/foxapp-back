using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Wsds.DAL.Entities;
using Wsds.DAL.Entities.Communication;
using Wsds.DAL.Repository.Abstract;

namespace Wsds.WebApp.Controllers
{
    [Produces("application/json")]
    [Route("api/Cart")]
    public class CartController : Controller
    {
        private ICartRepository _cartRepo;

        public CartController(ICartRepository cartRepo) => _cartRepo = cartRepo;

        [HttpGet("CartProducts")]
        public IActionResult CartProducts() {
            var userID = 3; //TODO
            return Ok(_cartRepo.GetClientOrderProductsByUserId(userID));
        }

        [HttpPut("CartProducts")]
        public IActionResult UpdateCartProduct([FromBody] ClientOrderProduct_DTO item)
        {
            return Ok(_cartRepo.UpdateCartProduct(item));
        }

        [HttpPost("CartProducts")]
        public IActionResult CreateCartProduct([FromBody] ClientOrderProduct_DTO item)
        {
            ClientOrderProduct_DTO result = _cartRepo.InsertCartProduct(item);
            return CreatedAtRoute("", new { id = result.id }, result);
        }

        [HttpDelete("CartProducts/{id}")]
        public IActionResult DeleteCartProduct(long id)
        {
            _cartRepo.DeleteCartProduct(id);
            return NoContent();
        }

        [HttpGet("ClientDraftOrder")]
        public IActionResult getClientDraftOrder()
        {
            return Ok(_cartRepo.GetOrCreateClientDraftOrder());
        }

        [HttpGet("GetCartProductsByOrderId")]
        public IActionResult GetClientOrderProductsByOrderId([FromQuery] long idOrder)
        {
            return Ok(_cartRepo.GetClientOrderProductsByOrderId(idOrder));
        }

        [HttpGet("ClientOrder")]
        public IActionResult GetClientOrders()
        {
            return Ok(_cartRepo.GetClientOrders());
        }

        [HttpGet("ClientOrder/{id}")]
        public IActionResult GetClientOrders(long id)
        {
            return Ok(_cartRepo.GetClientOrder(id));
        }

        [HttpPut("ClientDraftOrder")]
        public IActionResult SaveClientOrder([FromBody] ClientOrder_DTO order)
        {
            return Ok(_cartRepo.SaveClientOrder(order));
        }

        [HttpPost("CalculateCart")]
        public IActionResult CalculateCart([FromBody] CalculateCartRequest cart)
        {
            return Ok(_cartRepo.CalculateCart(cart));
        }

        [HttpPut("PostOrder")]
        public IActionResult PostOrder([FromBody] ClientOrder_DTO order)
        {
            return Ok(_cartRepo.PostOrder(order));
        }

        [HttpGet("ClientOrderProductsByDate")]
        public IActionResult GetClientOrderProductsByDate([FromQuery] string datesRange)
        {
            return Ok(_cartRepo.GetOrderProductsByDate(datesRange));
        }
    }
}