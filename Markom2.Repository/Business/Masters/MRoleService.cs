﻿using Dapper;
using Markom2.Repository.Models;
using Markom2.Repository.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Markom2.Repository.Business.Masters
{
    public class MRoleService
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ApplicationDbContext _dbContext;
        private readonly ILogger _logger;

        public MRoleService(
            RoleManager<IdentityRole> roleManager,
            ApplicationDbContext dbContext, 
            ILogger<MRoleService> logger)
        {
            _roleManager = roleManager;
            _dbContext = dbContext;
            _logger = logger;
        }

        public async Task<MRole> GetAsync(int dataId)
        {
            _logger.LogInformation("Collecting single role data ...");

            var result = await _dbContext.MRoles
                .Where(item => item.Id == dataId)
                .FirstOrDefaultAsync();

            _logger.LogInformation("The MRole entity is {@result}", result);

            return result;
        }

        public async Task<IList<VMRole>> GetAllAsync()
        {
            _logger.LogInformation("Collecting Role data");

            var result = await _dbContext.MRoles
                .Include(item => item.CreatedBy_Navigation)
                .Where(item => item.IsDelete == false)
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

        public async Task AddAsync(MRole entity)
        {
            _logger.LogInformation("Add new role both to MRole table and AspNetRoles table, entity: {@entity}", entity);

            using (var transaction = _dbContext.Database.BeginTransaction())
            {
                try
                {
                    var identityRoleResult = await _roleManager.CreateAsync(new IdentityRole
                    {
                        Name = entity.Name,
                        NormalizedName = entity.Name.ToUpper()
                    });
                    if (!identityRoleResult.Succeeded)
                        throw new Exception("Failed to add role to AspNetRoles");

                    entity.Code = "xxxx"; // hanya sementara
                    _dbContext.MRoles.Add(entity);

                    await _dbContext.SaveChangesAsync();

                    var lastId = entity.Id.ToString();
                    var leadingZeros = "0000";
                    var cutLeadingZeros = leadingZeros.AsMemory().Slice(lastId.Length);
                    var result = "RO" + cutLeadingZeros.ToString() + lastId;

                    entity.Code = result;

                    _dbContext.Attach(entity).State = EntityState.Modified;

                    await _dbContext.SaveChangesAsync();

                    transaction.Commit();
                }
                catch (Exception ex)
                {                                      
                    await transaction.RollbackAsync();

                    throw new Exception("Error adding role", ex);
                }
            }
        }

        public async Task EditAsync(MRole entity)
        {
            var targetEntity = await _dbContext.MRoles
                .Where(item => item.Id == entity.Id)
                .FirstAsync();

            targetEntity.Name = entity.Name;
            targetEntity.Description = entity.Description;

            targetEntity.UpdatedBy = entity.UpdatedBy;
            targetEntity.UpdatedDate = entity.UpdatedDate;

            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(int dataId, string updatedBy, DateTime updatedDate)
        {
            _logger.LogInformation("Deleting role (IsDelete=1) based on Id : {@dataId}", dataId);

            var targetEntity = await _dbContext.MRoles
                .Where(item => item.Id == dataId)
                .FirstAsync();

            targetEntity.IsDelete = true;

            targetEntity.UpdatedBy = updatedBy;
            targetEntity.UpdatedDate = updatedDate;

            await _dbContext.SaveChangesAsync();
        }

        public async Task<IList<VMRole>> SearchAsync(VMRole entity)
        {
            _logger.LogInformation("Searching for role using 'Dapper', {@entity}", entity);

            using (var connection = _dbContext.Database.GetDbConnection())
            {
                var paramObj = new
                {
                    Code = entity.Code == null ? "" : entity.Code,
                    Name = entity.Name == null ? "" : entity.Name,
                    CreatedBy = entity.CreatedBy == null ? "" : entity.CreatedBy,
                    CreatedDate = entity.CreatedDate == null ? "" : entity.CreatedDate
                };
                var result = await connection.QueryAsync<VMRole>("SearchRole", 
                    param: paramObj, 
                    commandType: CommandType.StoredProcedure);

                if (result == null || result.Count() == 0)
                    throw new Exception("Data not found");

                var resultList = result.ToList();

                return resultList;
            }
        }
    }
}
