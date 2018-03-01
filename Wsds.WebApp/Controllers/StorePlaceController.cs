using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Wsds.DAL.Repository;
using Wsds.WebApp.Attributes;

namespace Wsds.WebApp.Controllers
{
    [Produces("application/json")]
    [Route("api/StorePlace")]
    public class StorePlaceController : Controller
    {
        private IStorePlaceRepository _spRepo;
        public StorePlaceController(IStorePlaceRepository spRepo) => _spRepo = spRepo;

        [HttpGet("StorePlace/{id}")]
        public IActionResult GetStorePlaceById(long id) {
            return Ok(_spRepo.StorePlace(id));
        }

        [HttpGet("ProductStorePlaces")]
        [Link("idQuotationProduct")]
        public IActionResult GetProductStorePlacesByQuotId([FromQuery] long idQuotationProduct)
        {
            return Ok(_spRepo.GetProductSPByQuotId(idQuotationProduct));
        }

        [HttpGet("StorePlace")]
        public IActionResult GetStorePlaces()
        {
            return Ok(_spRepo.StorePlaces);
        }

    }
}