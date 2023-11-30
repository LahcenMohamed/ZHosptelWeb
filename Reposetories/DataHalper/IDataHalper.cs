using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ZHosptel.Reposetories.DataHalper
{
    public interface IDataHalper<Table>
    {
        public Task Add(Table table);
        public Task Update(Table table);
        public Task Delete(int id);
        public Task<Table> GetById(int id);
        public Task<List<Table>> GetAll();
    }
}
