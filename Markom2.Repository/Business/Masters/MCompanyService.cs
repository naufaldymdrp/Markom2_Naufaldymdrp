﻿using Markom2.Repository.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Markom2.Repository.Business.Masters
{
    public class MCompanyService
        : ITableService<MCompany>
    {
        private readonly ApplicationDbContext _context;

        private readonly ILogger _logger;

        public MCompanyService(ApplicationDbContext context, ILogger<MCompanyService> logger)
        {
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// Mengambil semua data MCompany
        /// </summary>
        /// <returns>Semua data MCompany berupa IList task</returns>
        public async Task<IList<MCompany>> GetAllAsync()
        {
            _logger.LogInformation("Pengambilan data MCompany dimulai");

            var result = await _context.MCompanies
                .Include(item => item.CreatedBy_Navigation)
                .Where(item => item.IsDelete == false)
                .ToListAsync();

            return result;
        }

        public async Task<MCompany> GetAsync(int id)
        {
            _logger.LogInformation("Pengambilan data tunggal MCompany berdasarkan data id dimulai");

            var result = await _context.MCompanies
                .Include(item => item.CreatedBy_Navigation)
                .Where(item => item.Id == id)
                .FirstOrDefaultAsync();

            return result;
        }

          

        public async Task AddAsync(MCompany data)
        {
            _logger.LogInformation("Penambahan data MCompany dimulai");

            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    data.Code = "xxxx"; // hanya sementara
                    _context.Add(data);
                    await _context.SaveChangesAsync();

                    var lastId = data.Id.ToString();
                    var leadingZeros = "0000";
                    var cutLeadingZeros = leadingZeros.AsMemory().Slice(lastId.Length);
                    data.Code = "CP" + cutLeadingZeros.ToString() + lastId;

                    _context.Attach(data).State = EntityState.Modified;

                    await _context.SaveChangesAsync();

                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();

                    _logger.LogError("MCompanyService error : {@ex}", ex);
                    throw new Exception("MCompanyService error", ex);
                }
            }            
        }

        public async Task<IList<MCompany>> GetAsync(MCompany targetData)
        {
            _logger.LogInformation("Pencarian data MCompany dimulai");

            var result = await _context.MCompanies
                            .Include(item => item.CreatedBy_Navigation)
                            .Where(item => EF.Functions.Like(item.Code, $"%{targetData.Code}%") 
                                && EF.Functions.Like(item.Name, $"%{targetData.Name}%"))                            
                            .ToListAsync();

            IList<MCompany> result2 = result;
            if (targetData.CreatedBy != null)
            {
                result2 = result
                .Where(item => item.CreatedBy_Navigation.UserName.Contains(targetData.CreatedBy))
                .ToList();
            }

            return result2;
        }

        public async Task EditAsync(MCompany data)
        {
            _logger.LogInformation("Perubahan data MCompany dimulai");

            var currentData = await _context.MCompanies
                .Where(item => item.Id == data.Id)
                .FirstAsync();

            currentData.Name = data.Name;
            currentData.Address = data.Address;
            currentData.Phone = data.Phone;
            currentData.Email = data.Email;
            currentData.UpdatedBy = data.UpdatedBy;
            currentData.UpdatedDate = data.UpdatedDate;

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int dataId, string updatedBy, DateTime updatedDate)
        {
            _logger.LogInformation("Penghapusan data MCompany (ubah IsDelete) dimulai");

            var entity = await _context.MCompanies
                .Where(item => item.Id == dataId)
                .FirstOrDefaultAsync();

            entity.IsDelete = true;
            entity.UpdatedBy = updatedBy;
            entity.UpdatedDate = updatedDate;

            await _context.SaveChangesAsync();
        }
    }
}
