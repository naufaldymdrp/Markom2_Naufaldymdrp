using Markom2.Repository.Models;
using Markom2.Repository.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Markom2.Repository.Business.Masters
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

        public async Task<IList<VMEmployee>> GetAllAsync()
        {
            _logger.LogInformation("Employees data is being collected");

            var result = await _dbContext.MEmployees
                .Include(item => item.MCompany_Navigation)
                .Include(item => item.CreatedBy_Navigation)
                .Select(item => new VMEmployee
                {
                    Id = item.Id,
                    Code = item.Code,
                    Name = item.FirstName + " " + item.LastName,
                    CompanyName = item.MCompany_Navigation.Name,
                    CreatedDate = item.CreatedDate,
                    CreatedBy = item.CreatedBy_Navigation.UserName
                })
                .ToListAsync();

            return result;
        }

        public async Task<IList<MEmployee>> GetAsync(MEmployee entity)
        {
            _logger.LogInformation("Employees data is being data collected based on search");

            var result = await _dbContext.MEmployees
                .Include(item => item.MCompany_Navigation)
                .Include(item => item.CreatedBy_Navigation)
                .Where(item => EF.Functions.Like(item.Code, $"%{entity.Code}%"))
                .ToListAsync();

            return result;
        }

        public async Task AddAsync(MEmployee entity)
        {
            _logger.LogInformation("Adding new employee, entity : {@entity}", entity);

            var matchResult = Regex.Match(entity.Code, @"^(\d{2}.){3}(\d{2})$", RegexOptions.Compiled);

            if (!matchResult.Success)
                throw new Exception("The format is not correct");

            _dbContext.MEmployees
                .Add(entity);

            await _dbContext.SaveChangesAsync();
        }
    }
}
