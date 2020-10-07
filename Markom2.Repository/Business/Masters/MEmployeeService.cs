using Markom2.Repository.Models;
using Markom2.Repository.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
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
                .Where(item => item.IsDelete == false)
                .Select(item => new VMEmployee
                {
                    Id = item.Id,
                    Code = item.Code,
                    Name = item.FirstName + " " + item.LastName,
                    CompanyName = item.MCompany_Navigation.Name,
                    CreatedBy = item.CreatedBy_Navigation.UserName,
                    CreatedDate = item.CreatedDate.ToString("dd/MM/yyyy")
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

        public async Task<MEmployee> GetAsync(int dataId)
        {
            _logger.LogInformation("Single employee data is being collected based on Id");

            var result = await _dbContext.MEmployees
                .Include(item => item.MCompany_Navigation)
                .FirstOrDefaultAsync(item => item.Id == dataId);

            _logger.LogInformation("The employee data is : {@result}", result);

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

        public async Task EditAsync(MEmployee entity)
        {
            _logger.LogInformation("Editing employee data based on its id, entity : {@entity}", entity);

            var targetEntity = await _dbContext.MEmployees
                .Where(item => item.Id == entity.Id)
                .FirstOrDefaultAsync();

            targetEntity.Code = entity.Code;
            targetEntity.FirstName = entity.FirstName;
            targetEntity.LastName = entity.LastName;
            targetEntity.MCompanyId = entity.MCompanyId;
            targetEntity.Email = entity.Email;

            targetEntity.UpdatedBy = entity.UpdatedBy;
            targetEntity.UpdatedDate = entity.UpdatedDate;

            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(int dataId, string updatedBy, DateTime updatedDate)
        {
            _logger.LogInformation("Deleting employee (IsDelete=1) based on id : {@dataId}", dataId);

            var targetEntity = await _dbContext.MEmployees
                .Where(item => item.Id == dataId)
                .FirstOrDefaultAsync();

            targetEntity.IsDelete = true;

            targetEntity.UpdatedBy = updatedBy;
            targetEntity.UpdatedDate = updatedDate;

            await _dbContext.SaveChangesAsync();
        }

        public async Task<IList<VMEmployee>> SearchAsync(VMEmployee searchEntity)
        {

            searchEntity.DefaultIfNullProperties();
            _logger.LogInformation("The search entity is {@searchEntity}", searchEntity);

            var createdDate = searchEntity.CreatedDate == null ? "%%" : $"%{searchEntity.CreatedDate}%";

            var result = await _dbContext.MEmployees                
                .Include(item => item.CreatedBy_Navigation)
                .Include(item => item.MCompany_Navigation)
                .Where(item => EF.Functions.Like(item.FirstName + " " + item.LastName, $"%{searchEntity.Name}%")
                    && EF.Functions.Like(item.Code, $"%{searchEntity.Code}%")
                    && EF.Functions.Like(item.MCompany_Navigation.Name, $"%{searchEntity.CompanyName}%")
                    && EF.Functions.Like(item.CreatedBy_Navigation.UserName, $"%{searchEntity.CreatedBy}%")
                    //&& EF.Functions.Like(item.CreatedDate.ToString(), createdDate)
                    && item.IsDelete == false
                 )
                .Select(item => new VMEmployee
                {
                    Id = item.Id,
                    Code = item.Code,
                    Name = $"{item.FirstName} {item.LastName}",
                    CompanyName = $"{item.MCompany_Navigation.Name}",
                    CreatedBy = $"{item.CreatedBy_Navigation.UserName}",
                    CreatedDate = item.CreatedDate.ToString("dd/MM/yyyy")
                })
                .ToListAsync();

            return result;
        }
    }
}
