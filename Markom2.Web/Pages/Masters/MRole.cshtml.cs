using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Markom2.Repository.Business.Masters;
using Markom2.Repository.Models;
using Markom2.Repository.ViewModels;
using Markom2.Web.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace Markom2.Web.Pages.Masters
{
    [Authorize]
    public class MRoleModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;        
        private readonly MRoleService _mRoleService;
        private readonly ILogger _logger;

        public MRoleModel(
            UserManager<IdentityUser> userManager,            
            MRoleService mRoleService,
            ILogger<MRoleModel> logger)
        {
            _userManager = userManager;
            _mRoleService = mRoleService;
            _logger = logger;
        }

        public MRole Role { get; set; }

        public VMRole RoleView { get; set; }

        public IList<VMRole> RoleViewList { get; set; }

        public async Task OnGetAsync()
        {
            try
            {
                RoleViewList = await _mRoleService.GetAllAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError("Error occured, {@ex}", ex);
            }
        }

        public async Task<IActionResult> OnGetAddItemPartialAsync()
        {
            try
            {
                var user = await _userManager.GetUserAsync(User);

                var role = new MRole { CreatedBy = user.Id };

                return Partial("MRolePartials/_Form", (role, FormPartialMode.Add));
            }
            catch (Exception ex)
            {
                _logger.LogError("Error occured, {@ex}", ex);

                return BadRequest(ex.Message);
            }
        }

        public async Task<IActionResult> OnPostAddAsync(MRole item1)
        {
            try
            {
                if (!TryValidateModel(item1))
                {
                    return BadRequest(ModelState);
                }

                await _mRoleService.AddAsync(item1);

                var roles = await _mRoleService.GetAllAsync();

                return Partial("MRolePartials/_ViewList", roles);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error occured, {@ex}", ex);

                return BadRequest(ex.Message);
            }
        }
    }
}
