using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BlazorDealer.Models
{
    public class CarType
    {
        [Key]
        public string ID { get; set; }
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        public ICollection<Car> Cars { get; set; }
    }
}
