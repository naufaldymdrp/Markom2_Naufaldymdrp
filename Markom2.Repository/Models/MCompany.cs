using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        [DisplayName("No")]
        public int Id { get; set; }
        
        [MaxLength(50)]
        [Column(TypeName = "varchar(50)")]
        [DisplayName("Company Code")]
        public string Code { get; set; }

        [Required]
        [MaxLength(50)]
        [Column(TypeName = "varchar(50)")]
        [DisplayName("Company Name")]
        public string Name { get; set; }

        [MaxLength(255)]
        [Column(TypeName = "varchar(255)")]
        public string Address { get; set; }

        [MaxLength(50)]
        [Column(TypeName = "varchar(50)")]
        public string Phone { get; set; }

        [MaxLength(50)]
        [Column(TypeName = "varchar(50)")]
        public string Email { get; set; }

        public bool IsDelete { get; set; }
                       
        [Required]                
        [Column("Created_By")]
        [DisplayName("Created By")]
        public string CreatedBy { get; set; }

        [ForeignKey("CreatedBy")]
        public IdentityUser CreatedBy_Navigation { get; set; }
              
        [Column("Created_Date", TypeName = "datetime")]
        [DisplayName("Created Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime CreatedDate { get; set; }

        //[MaxLength(50)]
        [Column("Updated_By")]        
        public string UpdatedBy { get; set; }

        [ForeignKey("UpdatedBy")]
        public IdentityUser UpdatedBy_Navigation { get; set; }

        [Column("Updated_Date", TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
    }    
}
