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
        [Display(Name = "Role Code", Prompt = "Role Code")]
        public string Code { get; set; }

        [MaxLength(50)]
        [Display(Name = "Role Name", Prompt = "Role Name")]
        public string Name { get; set; }
        
        [MaxLength(255)]        
        [DataType(DataType.MultilineText)]
        [Display(Prompt = "Description")]
        public string Description { get; set; }
        
        [Display(Name = "Created By", Prompt = "Created By")]
        public string CreatedBy { get; set; }        

        [Display(Name = "Created Date", Prompt = "Created Date")]
        public string CreatedDate { get; set; }
    }
}
