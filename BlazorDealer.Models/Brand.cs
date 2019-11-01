using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BlazorDealer.Models
{
    public class Brand
    {
        [Key]
        public string ID { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        [StringLength(50)]
        public string Country { get; set; }

        [Required]
        [StringLength(256)]
        public  string IconPath { get; set; }
        public DateTime CreatedDate { get; set; }
        public ICollection<Car> Cars { get; set; }
        

    }

}
