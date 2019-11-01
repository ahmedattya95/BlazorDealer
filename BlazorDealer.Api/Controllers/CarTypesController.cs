using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlazorDealer.Models;
using BlazorDealer.Models.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlazorDealer.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarTypesController : ControllerBase
    {
        private readonly BlazorDealerDbContext _db;
        public CarTypesController(BlazorDealerDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var carTypes = _db.CarTypes.Select(b => new CarType
            {
                ID = b.ID,
                Name = b.Name,
            });

            return Ok(new HttpCollectionResponse<CarType>
            {
                IsSuccess = true,
                Message = "Car Type returned successfully!",
                Values = carTypes
            });
        }
    }
}