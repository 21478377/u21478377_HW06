using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace u21478377_HW06.Models
{
    [Table("brands", Schema = "production")]
    public partial class Brand
    {
        public Brand()
        {
            Products = new HashSet<Product>();
        }

        [Key]
        [Column("brand_id")]
        public int BrandId { get; set; }
        [Required]
        [Column("brand_name")]
        [StringLength(255)]
        public string BrandName { get; set; }

        [InverseProperty(nameof(Product.Brand))]
        public virtual ICollection<Product> Products { get; set; }
    }
}
