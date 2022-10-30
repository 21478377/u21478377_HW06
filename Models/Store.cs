using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace u21478377_HW06.Models
{
    [Table("stores", Schema = "sales")]
    public partial class Store
    {
        public Store()
        {
            Orders = new HashSet<Order>();
            Stocks = new HashSet<Stock>();
            staff = new HashSet<Staff>();
        }

        [Key]
        [Column("store_id")]
        public int StoreId { get; set; }
        [Required]
        [Column("store_name")]
        [StringLength(255)]
        public string StoreName { get; set; }
        [Column("phone")]
        [StringLength(25)]
        public string Phone { get; set; }
        [Column("email")]
        [StringLength(255)]
        public string Email { get; set; }
        [Column("street")]
        [StringLength(255)]
        public string Street { get; set; }
        [Column("city")]
        [StringLength(255)]
        public string City { get; set; }
        [Column("state")]
        [StringLength(10)]
        public string State { get; set; }
        [Column("zip_code")]
        [StringLength(5)]
        public string ZipCode { get; set; }

        [InverseProperty(nameof(Order.Store))]
        public virtual ICollection<Order> Orders { get; set; }
        [InverseProperty(nameof(Stock.Store))]
        public virtual ICollection<Stock> Stocks { get; set; }
        [InverseProperty(nameof(Staff.Store))]
        public virtual ICollection<Staff> staff { get; set; }
    }
}
