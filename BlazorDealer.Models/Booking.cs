using System;
using System.ComponentModel.DataAnnotations;

namespace BlazorDealer.Models
{
    public class Booking
    {
        [Key]
        public string ID { get; set; }
        public Car Car { get; set; }
        public string CarId { get; set; } 
        public Customer Customer { get; set; }
        public string CustomerId { get; set; }

        [StringLength(500)]
        public string Description { get; set; }
        public DateTime Time { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
