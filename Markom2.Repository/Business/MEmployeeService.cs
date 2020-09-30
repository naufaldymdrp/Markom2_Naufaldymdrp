using Markom2.Repository.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Markom2.Repository.Business
{
    public class MEmployeeService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly ILogger _logger;

        public MEmployeeService(ApplicationDbContext dbContext, ILogger<MEmployeeService> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public async Task<IList<MEmployee>> GetAllAsync()
        {
            _logger.LogInformation("Employees data is being collected");

            var result = await _dbContext.M_Employee
                .Include(item => item.MCompany_Navigation)
                .Include(item => item.CreatedBy_Navigation)
                .ToListAsync();

            return result;
        }

        public async Task<IList<MEmployee>> GetAsync(MEmployee entity)
        {
            _logger.LogInformation("Employees data is being data collected based on search");

            var result = await _dbContext.M_Employee
                .Include(item => item.MCompany_Navigation)
                .Include(item => item.CreatedBy_Navigation)
                .Where(item => EF.Functions.Like(item.Code, $"%{entity.Code}%"))
                .ToListAsync();

            return result;
        }
    }
}
