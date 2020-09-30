using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Markom2.Repository.Models
{
    [Table("M_Employee")]
    public class MEmployee
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(50)]
        [Column("Employee_Number", TypeName = "varchar(50)")]
        public string Code { get; set; }

        [Required]
        [MaxLength(50)]
        [Column("First_Name", TypeName = "varchar(50)")]
        public string FirstName { get; set; }

        [MaxLength(50)]
        [Column("Last_Name", TypeName = "varchar(50)")]
        public string LastName { get; set; }

        [Column("M_Company_Id")]
        public int? MCompanyId { get; set; }

        [ForeignKey("MCompanyId")]
        public MCompany MCompany_Navigation { get; set; }

        [Column(TypeName = "varchar(150)")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Column("Is_Delete")]
        public bool IsDelete { get; set; }

        [Required]
        [Column("Created_By")]
        public string CreatedBy { get; set; }

        [ForeignKey("CreatedBy")]
        public IdentityUser CreatedBy_Navigation { get; set; }

        [Column("Created_Date", TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }

        [Column("Updated_By")]
        public string UpdatedBy { get; set; }

        [ForeignKey("UpdatedBy")]
        public IdentityUser UpdatedBy_Navigation { get; set; }

        [Column("Updated_Date", TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
    }
}
