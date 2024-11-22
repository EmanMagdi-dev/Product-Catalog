using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static Product_Catalog.BLL.Helper.ProductHelper;

namespace Product_Catalog.Models
{
    [Table("AuditLog")]
    public class AuditLog
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [ForeignKey(nameof(Product))]
        public int ProductId { get; set; }

        [Required]
        public ActionType Action { get; set; }

        [Required]
        [ForeignKey(nameof(User))]
        public string UserId { get; set; }

        public DateTime AtTime { get; set; } = DateTime.UtcNow;

        public virtual Product Product { get; set; }
        public virtual IdentityUser User { get; set; }
    }
}
