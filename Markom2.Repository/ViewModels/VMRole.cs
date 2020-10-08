using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Markom2.Repository.ViewModels
{
    public class VMRole
    {
        [DisplayName("No")]
        public int Id { get; set; }

        [MaxLength(50)]
        [DisplayName("Role Code")]
        public string Code { get; set; }

        [MaxLength(50)]
        [DisplayName("Role Name")]
        public string Name { get; set; }
        
        [MaxLength(255)]        
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }
        
        [DisplayName("Created By")]
        public string CreatedBy { get; set; }        

        [DisplayName("Created Date")]
        public DateTime CreatedDate { get; set; }
    }
}
