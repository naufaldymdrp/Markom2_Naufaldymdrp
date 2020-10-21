using Markom2.Repository.Models;
using Markom2.Repository.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Markom2.Repository.Business.Masters
{
    public class MUserService
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ApplicationDbContext _dbContext;
        private readonly ILogger _logger;

        public MUserService(
            UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager,
            ApplicationDbContext dbContext,
            ILogger<MUserService> logger
            )
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _dbContext = dbContext;
            _logger = logger;
        }

        public async Task<IList<VMUser>> GetAllAsync()
        {
            _logger.LogInformation("Getting all User data ...");

            var result = await _dbContext.MUsers
                .Include(item => item.MRole_Navigation)
                .Include(item => item.MEmployee_Navigation)
                    .ThenInclude(item => item.MCompany_Navigation)
                .Include(item => item.CreatedBy_Navigation)
                .Where(item => item.IsDelete == false)
                .Select(item => new VMUser
                {
                    No = item.Id,
                    Employee = $"{item.MEmployee_Navigation.FirstName} {item.MEmployee_Navigation.LastName}",
                    Role = item.MRole_Navigation.Name,
                    Company = item.MEmployee_Navigation.MCompany_Navigation.Name,
                    Username = item.Username,
                    Password = item.Password,
                    CreatedDate = item.CreatedDate.ToString("dd/MM/yyyy"),
                    CreatedBy = item.CreatedBy_Navigation.UserName         
                })
                .ToListAsync();

            return result;
        }
    }
}
