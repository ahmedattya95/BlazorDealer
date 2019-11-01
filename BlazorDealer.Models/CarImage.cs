using System;
using System.ComponentModel.DataAnnotations;

namespace BlazorDealer.Models
{
    public class CarImage
    {
        [Key]
        public string ID { get; set; }
        [Required]
        [StringLength(256)]
        public string ImagePath { get; set; }
        [Required]
        public DateTime UploadDate { get; set; }

        public Car Car { get; set; }
        public string CarId { get; set; }
    }
}
