using System;
using System.Collections.Generic;
using System.Text;

namespace BlazorDealer.Models.Shared
{
    public class BookingViewModel
    {

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Description { get; set; }
        public string CarId { get; set; }
        public DateTime Time { get; set; }
        public string Address { get; set; }

    }
}
