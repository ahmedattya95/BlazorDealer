using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BlazorDealer.Models
{
    public class Customer
    {
        [Key]
        public string ID { get; set; }

        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50)]
        public string LastName { get; set; }

        [Required]
        [StringLength(50)]
        public string Email { get; set; }

        [Required]
        [StringLength(50)]
        public string Phone { get; set; }

        [Required]
        [StringLength(256)]
        public string Address { get; set; }
        public DateTime CreatedDate { get; set; }
        public ICollection<Booking> Bookings { get; set; }
    }
}
