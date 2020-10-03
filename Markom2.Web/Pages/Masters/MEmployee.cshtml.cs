using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Markom2.Repository.Business.Masters;
using Markom2.Repository.Models;
using Markom2.Repository.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;

namespace Markom2.Web.Pages.Masters
{
    public enum MEmployeePartial
    {
        Add,
        Detail,
        Edit,
        Delete
    }

    [Authorize]
    public class MEmployeeModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly MCompanyService _mCompanyService;
        private readonly MEmployeeService _mEmployeeService;
        private readonly ILogger _logger;

        public MEmployeeModel(
            UserManager<IdentityUser> userManager,
            MCompanyService mCompanyService,
            MEmployeeService mEmployeeService, 
            ILogger<MEmployeeModel> logger)
        {
            _userManager = userManager;
            _mCompanyService = mCompanyService;
            _mEmployeeService = mEmployeeService;
            _logger = logger;
        }

        public MEmployee Employee { get; set; }

        public VMEmployee EmployeeView { get; set; }

        public IList<VMEmployee> EmployeeViewList { get; set; }

        public async Task OnGetAsync()
        {
            try
            {
                EmployeeViewList = await _mEmployeeService.GetAllAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError("Terjadi error ketika OnGetAsync, keterangan : {@ex}", ex);
            }
        }

        public async Task<IActionResult> OnGetAddMEmployeePartialAsync()
        {
            try
            {
                var user = await _userManager.GetUserAsync(User);

                var companies = await _mCompanyService.GetAllAsync();

                var selectList = new SelectList(companies, nameof(MCompany.Id), nameof(MCompany.Name));

                var employee = new MEmployee
                {
                    CreatedBy = user.Id
                };

                return Partial("MEmployeePartials/_MEmployeeFormPartial", (employee, MEmployeePartial.Add, selectList));
            }
            catch (Exception ex)
            {
                _logger.LogError("Terjadi error, keterangan : {@ex}", ex);

                return BadRequest(ex.Message);
            }
        }

        public async Task<IActionResult> OnPostAddMEmployeeAsync(MEmployee item1)
        {
            try
            {
                if (!TryValidateModel(item1))
                {
                    return BadRequest(ModelState);
                }

                await _mEmployeeService.AddAsync(item1);

                var employees = await _mEmployeeService.GetAllAsync();

                return Partial("MEmployeePartials/_MEmployeeViewListPartial", employees);
            }
            catch (Exception ex)
            {
                _logger.LogError("Terjadi error, karena {@ex}", ex);

                return BadRequest(ex.Message);
            }
        }
    }
}
