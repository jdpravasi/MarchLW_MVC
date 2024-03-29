using System.ComponentModel.DataAnnotations;

namespace MarchLW_MVC.Models
{
    public class Rides
    {
        [Key]
        public int ID { get; set; }
        [Required]
        public string RideName { get; set; }
        [Required]
        public int RidePriceAdult { get; set; }
        [Required]
        public int RidePriceChild { get; set; }
    }
}
