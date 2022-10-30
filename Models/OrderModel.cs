using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace u21478377_HW06.Models
{
    [Index(nameof(OrderDetailsOrderId), nameof(OrderDetailsItemId), Name = "IX_OrderModels_OrderDetailsOrderId_OrderDetailsItemId")]
    [Index(nameof(ProductId), Name = "IX_OrderModels_ProductId")]
    public partial class OrderModel
    {
        [Key]
        [StringLength(6)]
        public string Code { get; set; }
        [Required]
        [StringLength(255)]
        public string Name { get; set; }
        public int ProductId { get; set; }
        public int OrderId { get; set; }
        public int? OrderDetailsOrderId { get; set; }
        public int? OrderDetailsItemId { get; set; }

        [ForeignKey("OrderDetailsOrderId,OrderDetailsItemId")]
        [InverseProperty(nameof(OrderItem.OrderModels))]
        public virtual OrderItem OrderDetails { get; set; }
        [ForeignKey(nameof(ProductId))]
        [InverseProperty("OrderModels")]
        public virtual Product Product { get; set; }
    }
}
