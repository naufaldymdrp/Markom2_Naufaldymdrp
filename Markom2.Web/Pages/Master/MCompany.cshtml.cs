using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Markom2.Repository.Business;
using Markom2.Repository.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;


namespace Markom2.Web.Pages.Master
{
    public enum MCompanyPartial
    {
        Add,
        Detail,
        Edit,
        Delete
    }

    [Authorize]
    public class MCompanyModel : PageModel
    {
        private readonly MCompanyService _mCompanyService;

        private readonly ILogger _logger;

        private readonly UserManager<IdentityUser> _userManager;

        public MCompanyModel(MCompanyService mCompanyService, ILogger<MCompanyModel> logger, UserManager<IdentityUser> userManager)
        {
            _mCompanyService = mCompanyService;
            _logger = logger;
            _userManager = userManager;
        }

        public IList<MCompany> Companies { get; set; }

        public MCompany Company { get; set; }

        public Exception Error { get; set; }

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

        public async Task<IActionResult> OnGetSearchAsync(MCompany company)
        {
            try
            {
                var result = await _mCompanyService.GetAsync(company);

                return Partial("MCompanyPartials/_MCompanyListPartial", result);
            }
            catch (Exception ex)
            {
                _logger.LogError("Terjadi error : {@ex}", ex);
                return BadRequest(ex);
            }
        }

        public async Task<IActionResult> OnGetAddMCompanyPartialAsync()
        {
            try
            {                
                var currentUser = await _userManager.GetUserAsync(User);

                var company = new MCompany { CreatedBy = currentUser.Id };
                var tuple = (company, MCompanyPartial.Add);
                return Partial("MCompanyPartials/_MCompanyFormPartial", tuple);
            }
            catch (Exception ex)
            {
                _logger.LogError("Terjadi error : {@ex}", ex);

                return BadRequest(ex);
            }
        }  
        
        public async Task<IActionResult> OnGetDetailMCompanyPartialAsync(int dataId)
        {
            try
            {
                var company = await _mCompanyService.GetAsync(dataId);

                var tuple = (company, MCompanyPartial.Detail);
                return Partial("MCompanyPartials/_MCompanyFormPartial", tuple);
            }
            catch (Exception ex)
            {
                _logger.LogError("Terjadi error : {@ex}", ex);

                return BadRequest(ex);
            }
        }

        public async Task<IActionResult> OnGetEditMCompanyPartialAsync(int dataId)
        {
            try
            {
                var company = await _mCompanyService.GetAsync(dataId);

                var tuple = (company, MCompanyPartial.Edit);
                return Partial("MCompanyPartials/_MCompanyFormPartial", tuple);
            }
            catch (Exception ex)
            {
                _logger.LogError("Terjadi error : {@ex}", ex);

                return BadRequest(ex);
            }
        }

        public async Task<IActionResult> OnPostAddMCompanyAsync(MCompany Item1)
        {            
            try
            {           
                if (!TryValidateModel(Item1))
                {
                    var result = BadRequest(ModelState);
                    return result;
                }

                await _mCompanyService.AddAsync(Item1);

                Companies = await _mCompanyService.GetAllAsync();

                return Partial("MCompanyPartials/_MCompanyListPartial", Companies);
            }
            catch (Exception ex)
            {
                _logger.LogInformation("Error : {@ex}", ex);
                return BadRequest(ex.Message);
            }            
        }

        public async Task<IActionResult> OnPostEditMCompanyAsync(MCompany item1)
        {
            try
            {
                if (!TryValidateModel(item1))
                {
                    return BadRequest(ModelState);
                }

                var user = await _userManager.GetUserAsync(User);
                item1.UpdatedBy = user.Id;
                item1.UpdatedDate = DateTime.Now;

                await _mCompanyService.EditAsync(item1);

                Companies = await _mCompanyService.GetAllAsync();

                return Partial("MCompanyPartials/_MCompanyListPartial", Companies);
            }
            catch (Exception ex)
            {
                _logger.LogError("Terjadi error : {@ex}", ex);

                return BadRequest(ex.Message);
            }
        }
    }
}
