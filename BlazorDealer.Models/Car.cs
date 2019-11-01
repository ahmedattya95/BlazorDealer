using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlazorDealer.Models
{
    public class Car
    {
        [Key]
        public string ID { get; set; }
        [Required]
        [StringLength(50)]
        public string Model { get; set; }
        public int Year { get; set; }
        [StringLength(500)]
        public string Description { get; set; }
        public CarType CarType { get; set; }
        public string CarTypeId { get; set; }
        public Brand Brand { get; set; }
        public string BrandId { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        [Required]
        public decimal Price { get; set; }
        [Required]
        [StringLength(256)]
        public string PhotoPath { get; set; }
        public DateTime CreatedDate { get; set; }
        public ICollection<Booking> Bookings { get; set; }
        public ICollection<CarImage> CarImages { get; set; }
    }
}
