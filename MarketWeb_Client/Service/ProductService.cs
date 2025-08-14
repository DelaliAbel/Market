using MarketWeb_Client.Service.IService;
using MarketWeb_Models;

namespace MarketWeb_Client.Service
{
    public class ProductService : IProductService
    {
        public Task<ProductDTO> Get(int i_productId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ProductDTO>> GetAll()
        {
            throw new NotImplementedException();
        }
    }
}
