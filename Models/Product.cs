using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace u21478377_HW06.Models
{
    [Table("products", Schema = "production")]
    public partial class Product
    {
        public Product()
        {
            OrderItems = new HashSet<OrderItem>();
            OrderModels = new HashSet<OrderModel>();
            Stocks = new HashSet<Stock>();
        }

        [Key]
        [Column("product_id")]
        public int ProductId { get; set; }
        [Required]
        [Column("product_name")]
        [StringLength(255)]
        public string ProductName { get; set; }
        [Column("brand_id")]
        public int BrandId { get; set; }
        [Column("category_id")]
        public int CategoryId { get; set; }
        [Column("model_year")]
        public short ModelYear { get; set; }
        [Column("list_price", TypeName = "decimal(10, 2)")]
        public decimal ListPrice { get; set; }

        [ForeignKey(nameof(BrandId))]
        [InverseProperty("Products")]
        public virtual Brand Brand { get; set; }
        [ForeignKey(nameof(CategoryId))]
        [InverseProperty("Products")]
        public virtual Category Category { get; set; }
        [InverseProperty(nameof(OrderItem.Product))]
        public virtual ICollection<OrderItem> OrderItems { get; set; }
        [InverseProperty(nameof(OrderModel.Product))]
        public virtual ICollection<OrderModel> OrderModels { get; set; }
        [InverseProperty(nameof(Stock.Product))]
        public virtual ICollection<Stock> Stocks { get; set; }
    }
}
