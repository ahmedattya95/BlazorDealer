using System;
using System.ComponentModel.DataAnnotations;

namespace BlazorDealer.Models
{
    public class Notification
    {
        [Key]
        public string ID { get; set; }

        [Required]
        [StringLength(256)]
        public string Title { get; set; }
        public DateTime NotificationDate { get; set; }
        public bool IsSeen { get; set; }
        public bool IsSent { get; set; }
    }

}
