using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Markom2.Repository.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Markom2.Web.Pages.Master
{
    [Authorize]
    public class MCompanyModel : PageModel
    {

        

        IList<MCompany> Companies { get; set; }

        public void OnGet()
        {
            
        }
    }
}
