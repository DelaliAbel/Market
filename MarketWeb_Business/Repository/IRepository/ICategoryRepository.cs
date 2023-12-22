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
        public CategoryDTO Create(CategoryDTO i_categoryDTO);
        public CategoryDTO Update(CategoryDTO i_categoryDTO);
        public int Delete(int i_id);
        public CategoryDTO Get(int i_id);
        public IEnumerable<CategoryDTO> GetAll();
    }
}
