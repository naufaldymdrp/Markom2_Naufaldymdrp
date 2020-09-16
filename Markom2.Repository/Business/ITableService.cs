using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Markom2.Repository.Business
{   
    public interface ITableService<T>
    {
        public Task<IList<T>> GetAllAsync();

        //public IList<T> Get(int dataId);

        //public IList<T> Get(string dataId);

        public Task AddAsync(T data);

        public Task EditAsync(T data);

        public Task DeleteAsync(int dataId);

        //public void Delete(string dataId);

        //public void Delete(T data);        
    }
}
