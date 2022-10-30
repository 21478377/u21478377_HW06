using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace u21478377_HW06.Models
{
    [Table("customers", Schema = "sales")]
    public partial class Customer
    {
        public Customer()
        {
            Orders = new HashSet<Order>();
        }

        [Key]
        [Column("customer_id")]
        public int CustomerId { get; set; }
        [Required]
        [Column("first_name")]
        [StringLength(255)]
        public string FirstName { get; set; }
        [Required]
        [Column("last_name")]
        [StringLength(255)]
        public string LastName { get; set; }
        [Column("phone")]
        [StringLength(25)]
        public string Phone { get; set; }
        [Required]
        [Column("email")]
        [StringLength(255)]
        public string Email { get; set; }
        [Column("street")]
        [StringLength(255)]
        public string Street { get; set; }
        [Column("city")]
        [StringLength(50)]
        public string City { get; set; }
        [Column("state")]
        [StringLength(25)]
        public string State { get; set; }
        [Column("zip_code")]
        [StringLength(5)]
        public string ZipCode { get; set; }

        [InverseProperty(nameof(Order.Customer))]
        public virtual ICollection<Order> Orders { get; set; }
    }
}
