using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlazorDealer.Models;
using BlazorDealer.Models.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace BlazorDealer.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {

        private readonly BlazorDealerDbContext _db;
        private readonly IHubContext<BookingHub> _hub;

        public BookingController(BlazorDealerDbContext db, IHubContext<BookingHub> hub)
        {
            _db = db;
            _hub = hub;
        }

        [HttpPost]
        public async Task<IActionResult> Post(BookingViewModel model)
        {
            if(ModelState.IsValid)
            {
                var car = await _db.Cars.FindAsync(model.CarId);
                if (car == null)
                    return NotFound(); 

                var customer = new Customer();
                customer.FirstName = model.FirstName;
                customer.LastName = model.LastName;
                customer.Address = model.Address;
                customer.CreatedDate = DateTime.UtcNow;
                customer.ID = Guid.NewGuid().ToString();
                customer.Email = model.Email;
                customer.Phone = model.Phone;
                var booking = new Booking
                {
                    ID = Guid.NewGuid().ToString(),
                    CarId = model.CarId,
                    CreatedDate = DateTime.UtcNow,
                    Time = model.Time,
                    Description = model.Description,
                };
                customer.Bookings = new List<Booking>(); 
                customer.Bookings.Add(booking); 
                await _db.Customers.AddAsync(customer);
                await _db.SaveChangesAsync();

                string message = $"{car.Model} has been booked for test driver by {model.FirstName} {model.LastName}";
                await _hub.Clients.All.SendAsync("booking", message); 

                return Ok(new HttpSingleResponse<string>
                {
                    Message = "Booking has been received successfully!",
                    IsSuccess = true, 
                    ResponseDate = DateTime.UtcNow, 
                    Value = message
                });
            }

            return BadRequest("Some properties are not valid"); 
        }
    }
}