using MarketWeb_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketWeb_Business.Repository.IRepository
{
    public interface ICategoryRepository
    {
        public Task<CategoryDTO> Create(CategoryDTO i_categoryDTO);
        public Task<CategoryDTO> Update(CategoryDTO i_categoryDTO);
        public Task<int> Delete(int i_id);
        public Task<CategoryDTO> Get(int i_id);
        public Task<IEnumerable<CategoryDTO>> GetAll();
    }
}
