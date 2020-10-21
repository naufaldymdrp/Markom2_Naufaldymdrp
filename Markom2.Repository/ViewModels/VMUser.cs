using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Markom2.Repository.ViewModels
{
    public class VMUser
    {
        public int No { get; set; }

        [MaxLength(101)]
        [Display(Prompt = "Employee")]
        public string Employee { get; set; }

        [MaxLength(50)]
        [Display(Prompt = "Role")]
        public string Role { get; set; }

        [MaxLength(50)]
        [Display(Prompt = "Company")]
        public string Company { get; set; }

        [MaxLength(50)]
        [Display(Prompt = "Username")]
        public string Username { get; set; }
        
        public string Password { get; set; }

        [Display(Name = "Created Date", Prompt = "Created Date")]
        public string CreatedDate { get; set; }

        [MaxLength(50)]
        [Display(Name = "Created By", Prompt = "Created By")]
        public string CreatedBy { get; set; }
    }
}
