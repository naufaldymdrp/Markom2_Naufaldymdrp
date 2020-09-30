using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Markom2.Repository.ViewModels
{
    public class MEmployeeViewModel
    {
        [MaxLength(50)]
        public string Code { get; set; }

        [MaxLength(100)]
        public string Name { get; set; }

        public string CompanyName { get; set; }

        [Display(Name = "Created By", Prompt = "Created By")]
        public string CreatedBy { get; set; }

        [Display(Name = "Created Date", Prompt = "Created Date")]
        public DateTime CreatedDate { get; set; }
    }
}
