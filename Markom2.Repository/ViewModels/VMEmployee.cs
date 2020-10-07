using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Markom2.Repository.ViewModels
{
    public class VMEmployee
    {
        public int Id { get; set; }

        [MaxLength(50)]
        [Display(Name = "Code", Prompt = "Code")]
        public string Code { get; set; }

        [MaxLength(101)]
        [Display(Name = "Name", Prompt = "Name")]
        public string Name { get; set; }

        [Display(Name = "Company Name", Prompt = "Company Name")]
        public string CompanyName { get; set; }

        [Display(Name = "Created By", Prompt = "Created By")]
        public string CreatedBy { get; set; }

        [Display(Name = "Created Date", Prompt = "Created Date")]
        [DataType(DataType.Text)]
        public string CreatedDate { get; set; }

        public void DefaultIfNullProperties()
        {
            if (Code == null)
                Code = string.Empty;
            if (Name == null)
                Name = string.Empty;
            if (CompanyName == null)
                CompanyName = string.Empty;
            if (CreatedBy == null)
                CreatedBy = string.Empty;
            if (CreatedDate == null)
                CreatedDate = string.Empty;
        }
    }
}
