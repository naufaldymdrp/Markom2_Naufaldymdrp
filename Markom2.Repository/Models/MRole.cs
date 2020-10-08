using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Markom2.Repository.Models
{
    [Table("M_Role")]
    public class MRole
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        [Column(TypeName = "varchar(50)")]
        public string Code { get; set; }

        [Required]
        [MaxLength(50)]
        [Column(TypeName = "varchar(50)")]
        public string Name { get; set; }

        [Required]
        [MaxLength(255)]
        [Column(TypeName = "varchar(255)")]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [Column("Is_Delete")]
        public bool IsDelete { get; set; }

        [Required]
        [Column("Created_By")]
        public string CreatedBy { get; set; }

        [ForeignKey("CreatedBy")]
        public IdentityUser CreatedBy_Navigation { get; set; }

        [Column("Created_Date")]
        public DateTime CreatedDate { get; set; }

        [Column("Updated_By")]
        public string UpdatedBy { get; set; }

        [ForeignKey("UpdatedBy")]
        public IdentityUser UpdatedBy_Navigation { get; set; }

        [Column("Updated_Date")]
        public DateTime? UpdatedDate { get; set; }
    }
}
