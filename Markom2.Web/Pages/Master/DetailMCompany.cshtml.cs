using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Markom2.Repository.Models;

namespace Markom2.Web.Pages.Master
{
    public class DetailMCompanyModel : PageModel
    {
        private readonly Markom2.Repository.Models.ApplicationDbContext _context;

        public DetailMCompanyModel(Markom2.Repository.Models.ApplicationDbContext context)
        {
            _context = context;
        }

        public MCompany MCompany { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            MCompany = await _context.M_Company
                .Include(m => m.CreatedBy_Navigation)
                .Include(m => m.UpdatedBy_Navigation).FirstOrDefaultAsync(m => m.Id == id);

            if (MCompany == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
