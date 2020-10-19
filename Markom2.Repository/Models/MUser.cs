using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Markom2.Repository.Models
{
    [Table("M_User")]
    public class MUser
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        [Column(TypeName = "varchar(50)")]        
        public string Username { get; set; }

        [Required]
        [Column(TypeName = "varchar(50)")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Column("M_Role_Id")]
        public int MRoleId { get; set; }

        [ForeignKey("MRoleId")]
        public MRole MRole_Navigation { get; set; }

        [Column("M_Employee_Id")]
        public int MEmployeeId { get; set; }

        [ForeignKey("MEmployeeId")]
        public MEmployee MEmployee_Navigation { get; set; }

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
