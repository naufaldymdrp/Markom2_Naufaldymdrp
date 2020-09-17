using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using System.Text;

namespace Markom2.Repository.Models
{
    [Table("M_Company")]
    public class MCompany
    {
        [Key]
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
        public string Address { get; set; }

        [Required]
        [MaxLength(50)]
        [Column(TypeName = "varchar(50)")]
        public string Phone { get; set; }

        [Required]
        [MaxLength(50)]
        [Column(TypeName = "varchar(50)")]
        public string Email { get; set; }

        public bool IsDelete { get; set; }
                       
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
        public DateTime UpdatedDate { get; set; }
    }
}
