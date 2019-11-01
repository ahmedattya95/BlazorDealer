using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BlazorDealer.Models.Shared
{
    public class CarViewModel
    {

        [Required]
        [StringLength(50)]
        public string CarModel { get; set; }

        [Required]
        public int Year { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        [Required]
        public string TypeId { get; set; }

        [Required]
        public string BrandId { get; set; }

        [Required]
        public decimal Price { get; set; }

    }
}
