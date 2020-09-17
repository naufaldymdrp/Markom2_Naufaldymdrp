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

            var result = await _context.M_Company
                .Include(item => item.CreatedBy_Navigation)
                .ToListAsync();

            return result;
        }

        /// <summary>
        /// Pengambilan data MCompany berdasarkan Id
        /// </summary>
        /// <param name="companyCode">kode dari MCompany</param>
        /// <returns>data MCompany berdasarkan Id dalam bentuk IList task</returns>
        public async Task<IList<MCompany>> GetAsync(string companyCode, string companyName)
        {
            _logger.LogInformation("Pengambilan data MCompany berdasarkan id dimulai");

            var result = await _context.M_Company
                .Where(item => item.Code.Contains(companyCode) || item.Code.Contains(companyName))
                .ToListAsync();

            return result;
        }

        public async Task AddAsync(MCompany data)
        {
            _logger.LogInformation("Penambahan data MCompany dimulai");
            
            await _context.AddAsync(data);                        
        }

        public async Task EditAsync(MCompany data)
        {
            _logger.LogInformation("Perubahan data MCompany dimulai");

            _context.Attach(data).State = EntityState.Modified;

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int dataId)
        {
            _logger.LogInformation("Penghapusan data MCompany (ubah IsDelete) dimulai");

            var entity = await _context.M_Company
                .Where(item => item.Id == dataId)
                .FirstOrDefaultAsync();

            entity.IsDelete = false;

            _context.Attach(entity).State = EntityState.Modified;

            await _context.SaveChangesAsync();
        }
    }
}
