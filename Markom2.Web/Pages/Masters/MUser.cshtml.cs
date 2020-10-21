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
    public class MUserModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ILogger _logger;
        private readonly MUserService _mUserService;

        public MUserModel(
            UserManager<IdentityUser> userManager,
            ILogger<MUserModel> logger,
            MUserService mUserSservice

            )
        {
            _userManager = userManager;
            _logger = logger;
            _mUserService = mUserSservice;
        }

        public IList<VMUser> UserViewList { get; set; }

        public async Task OnGetAsync()
        {
            try
            {
                UserViewList = await _mUserService.GetAllAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError("Error occured, {@ex}", ex);

                throw new Exception(ex.Message);
            }
        }
    }
}
