﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MarchLW_MVC.Models
{
    public class Tickets
    {
        [Key]
        public int ID { get; set; }
        [ForeignKey(nameof(CustomerID))]
        public int CustomerID { get; set; }
    }
}
