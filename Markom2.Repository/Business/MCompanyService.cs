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
    class MCompanyService
        //: ITableService<MCompany>
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

            try
            {
                var result = await _context.M_company.ToListAsync();

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError("Pengambilan data MCompany gagal, keterangan {0}", ex.Message);
                throw new Exception("Pengambilan semua data MCompany gagal", ex);
            }
        }

        /// <summary>
        /// Pengambilan data MCompany berdasarkan Id
        /// </summary>
        /// <param name="companyCode">kode dari MCompany</param>
        /// <returns>data MCompany berdasarkan Id dalam bentuk IList task</returns>
        public async Task<IList<MCompany>> GetAsync(string companyCode, string companyName)
        {
            _logger.LogInformation("Pengambilan data MCompany berdasarkan id dimulai");

            try
            {
                var result = await _context.M_company
                    .Where(item => item.Code.Contains(companyCode) || item.Code.Contains(companyName))
                    .ToListAsync();

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError("Pengambilan data MCompany berdasarkan id gagal, keterangan {0}", ex.Message);
                throw new Exception("Pengambilan data MCompany berdasarkan id gagal", ex);
            }
        }

        public async Task AddAsync(MCompany data)
        {
            _logger.LogInformation("Penambahan data MCompany dimulai");

            try
            {
                await _context.AddAsync(data);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Telah terjadi error, karena {ex.Message}");
                throw new Exception($"Telah terjadi error", ex);
            }
        }

        public async Task EditAsync(MCompany data)
        {
            _logger.LogInformation("Perubahan data MCompany dimulai");

            try
            {
                var result = _context.Attach(data).State = EntityState.Modified;

                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError("Terjadi error, keterangan : {0}", ex.Message);
                throw new Exception("Perubahan data error", ex);
            }
        }
    }
}
