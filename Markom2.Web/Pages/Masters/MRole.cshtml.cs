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

        public async Task<IActionResult> GeneralGet(int dataId, FormPartialMode formMode)
        {
            try
            {
                if (dataId == 0 && formMode == FormPartialMode.Add)
                {
                    var userId = _userManager.GetUserId(User);

                    var role = new MRole { CreatedBy = userId };

                    return Partial("MRolePartials/_Form", (role, formMode));
                }
                else if (dataId != 0)
                {
                    var role = await _mRoleService.GetAsync(dataId);

                    return Partial("MRolePartials/_Form", (role, formMode));
                }
                else
                {
                    throw new ArgumentException("dataId ");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Error occured, {@ex}", ex);

                return BadRequest(ex.Message);
            }
        }

        #region Add

        public async Task<IActionResult> OnGetAddItemPartialAsync()
        {            
            return await GeneralGet(0, FormPartialMode.Add);
        }

        public async Task<IActionResult> OnPostAddAsync(MRole item1)
        {
            try
            {
                item1.CreatedDate = DateTime.Now;
                ModelState.Clear();
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

        #endregion

        #region Detail

        public async Task<IActionResult> OnGetDetailAsync(int dataId)
        {                        
            return await GeneralGet(dataId, FormPartialMode.Detail);
        }

        #endregion 

        #region Edit

        public async Task<IActionResult> OnGetEditAsync(int dataId)
        {
            return await GeneralGet(dataId, FormPartialMode.Edit);
        }

        public async Task<IActionResult> OnPostEditAsync(MRole item1)
        {
            try
            {
                if (!TryValidateModel(item1))
                    return BadRequest(item1);

                var userId = _userManager.GetUserId(User);
                item1.UpdatedBy = userId;
                item1.UpdatedDate = DateTime.Now;

                await _mRoleService.EditAsync(item1);

                var roles = await _mRoleService.GetAllAsync();

                return Partial("MRolePartials/_ViewList", roles);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error occured, {@ex}", ex);

                return BadRequest(ex.Message);
            }
        }

        #endregion

        #region Delete

        public async Task<IActionResult> OnGetDeleteAsync(int dataId)
        {
            return await GeneralGet(dataId, FormPartialMode.Delete);
        }

        public async Task<IActionResult> OnPostDeleteAsync([FromBody] JsonDataId data)
        {
            try
            {
                if (data == null)
                    throw new ArgumentException("Parameter should not be null !");

                var dataId = Convert.ToInt32(data.DataId);
                var updatedBy = _userManager.GetUserId(User);
                var updatedDate = DateTime.Now;

                await _mRoleService.DeleteAsync(dataId, updatedBy, updatedDate);

                var roles = await _mRoleService.GetAllAsync();

                return Partial("MRolePartials/_ViewList", roles);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error occured, {@ex}", ex);

                return BadRequest(ex.Message);
            }
        }

        #endregion

        #region Search

        public async Task<IActionResult> OnGetSearchAsync(VMRole roleView)
        {
            try
            {
                var roles = await _mRoleService.SearchAsync(roleView);

                return Partial("MRolePartials/_ViewList", roles);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error occured, {@ex}", ex);

                return BadRequest(ex.Message);
            }
        }

        #endregion
    }
}
