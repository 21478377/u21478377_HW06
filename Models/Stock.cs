using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace u21478377_HW06.Models
{
    [Table("stocks", Schema = "production")]
    public partial class Stock
    {
        [Key]
        [Column("store_id")]
        public int StoreId { get; set; }
        [Key]
        [Column("product_id")]
        public int ProductId { get; set; }
        [Column("quantity")]
        public int? Quantity { get; set; }

        [ForeignKey(nameof(ProductId))]
        [InverseProperty("Stocks")]
        public virtual Product Product { get; set; }
        [ForeignKey(nameof(StoreId))]
        [InverseProperty("Stocks")]
        public virtual Store Store { get; set; }
    }
}
