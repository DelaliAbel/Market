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
        public CategoryDTO Create(CategoryDTO categoryDTO);
        public CategoryDTO Update(CategoryDTO categoryDTO);
        public int Delete(int id);
        public CategoryDTO Get();
        public IEnumerable<CategoryDTO> GetAll();
    }
}
