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
    }
}
