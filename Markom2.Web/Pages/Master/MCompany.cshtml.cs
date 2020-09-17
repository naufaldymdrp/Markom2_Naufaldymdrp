using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Markom2.Repository.Business;
using Markom2.Repository.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

#nullable enable
namespace Markom2.Web.Pages.Master
{
    [Authorize]
    public class MCompanyModel : PageModel
    {
        private readonly MCompanyService _mCompanyService;

        private readonly ILogger _logger;

        public MCompanyModel(MCompanyService mCompanyService, ILogger<MCompanyModel> logger)
        {
            _mCompanyService = mCompanyService;
            _logger = logger;
        }

        public IList<MCompany>? Companies { get; set; }

        public Exception? Error { get; set; }        

        public async Task OnGetAsync()
        {
            try
            {
                Companies = await _mCompanyService.GetAllAsync();
            }
            catch (Exception ex)
            {                
                _logger.LogError("Pengambilan data gagal, keterangan {@ex} : ", ex);
                Error = ex;
            }
        }
    }
}
