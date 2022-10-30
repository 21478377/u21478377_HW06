using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using u21478377_HW06.Models;
using PagedList.Mvc;

namespace u21478377_HW06.ViewModels
{
    public class ProductVM
    {
        [Key]
        public int keyId { get; set; }
        public int Total { get; set; }
        public int GrandTotal { get; set; }

        public Product ProductDetails { get; set; }
        public Brand BrandDetails { get; set; }
        public Category CategoryDetails { get; set; }
        public Order OrderDetails { get; set; }
        public OrderItem ItemDetails { get; set; }
        public Store StoreDetail { get; set; }
    }
}
