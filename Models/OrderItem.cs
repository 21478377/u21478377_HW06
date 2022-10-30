using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace u21478377_HW06.Models
{
    [Table("order_items", Schema = "sales")]
    public partial class OrderItem
    {
        public OrderItem()
        {
            OrderModels = new HashSet<OrderModel>();
        }

        [Key]
        [Column("order_id")]
        public int OrderId { get; set; }
        [Key]
        [Column("item_id")]
        public int ItemId { get; set; }
        [Column("product_id")]
        public int ProductId { get; set; }
        [Column("quantity")]
        public int Quantity { get; set; }
        [Column("list_price", TypeName = "decimal(10, 2)")]
        public decimal ListPrice { get; set; }
        [Column("discount", TypeName = "decimal(4, 2)")]
        public decimal Discount { get; set; }

        [ForeignKey(nameof(OrderId))]
        [InverseProperty("OrderItems")]
        public virtual Order Order { get; set; }
        [ForeignKey(nameof(ProductId))]
        [InverseProperty("OrderItems")]
        public virtual Product Product { get; set; }
        [InverseProperty(nameof(OrderModel.OrderDetails))]
        public virtual ICollection<OrderModel> OrderModels { get; set; }
    }
}
