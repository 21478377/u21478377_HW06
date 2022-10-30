using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace u21478377_HW06.Models
{
    public class ReportVM
    {
        [Key]
        public int reportId { get; set; }

        public Product ProductDetails { get; set; }
        public Category CategoryDetails { get; set; }
        public Order OrderDetails { get; set; }
        public OrderItem ItemDetails { get; set; }
    }
}
