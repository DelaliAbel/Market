using MarketWeb_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketWeb_Business.Repository.IRepository
{
    public interface IProductRepository
    {
        public Task<ProductDTO> Create(ProductDTO i_productDTO);
        public Task<ProductDTO> Update(ProductDTO i_productDTO);
        public Task<int> Delete(int i_id);
        public Task<ProductDTO> Get(int i_id);
        public Task<IEnumerable<ProductDTO>> GetAll();

    }
}
