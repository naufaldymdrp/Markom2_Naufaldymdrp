using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Markom2.Repository.Business
{   
    public interface ITableService<T>
    {
        public Task<IList<T>> GetAllAsync();   

        public Task AddAsync(T data);

        public Task EditAsync(T data);

        public Task DeleteAsync(int dataId, string updatedBy, DateTime updatedDate);
    }
}
