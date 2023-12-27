using MarketWeb_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketWeb_Business.Repository.IRepository
{
    public interface IProductPriceRepository
    {
        public Task<ProductPriceDTO> Create(ProductPriceDTO i_productPriceDTO);
        public Task<ProductPriceDTO> Update(ProductPriceDTO i_productPriceDTO);
        public Task<int> Delete(int i_id);
        public Task<ProductPriceDTO> Get(int i_id);
        public Task<IEnumerable<ProductPriceDTO>> GetAll(int? i_id = null);

    }
}
