using Markom2.Repository.Models;
using Markom2.Repository.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Markom2.Repository.Business.Masters
{
    public class MRoleService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly ILogger _logger;

        public MRoleService(ApplicationDbContext dbContext, ILogger<MRoleService> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public async Task<IList<VMRole>> GetAllAsync()
        {
            _logger.LogInformation("Collecting Role data");

            var result = await _dbContext.MRoles
                .Include(item => item.CreatedBy_Navigation)
                .Select(item => new VMRole
                {
                    Id = item.Id,
                    Code = item.Code,
                    Name = item.Name,
                    Description = item.Description,
                    CreatedBy = item.CreatedBy_Navigation.UserName,
                    CreatedDate = item.CreatedDate.ToString("dd/MM/yyyy")
                })
                .ToListAsync();

            return result;
        }
    }
}
