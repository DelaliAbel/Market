using MarketWeb_Models;

namespace MarketWeb_Client.Service.IService
{
    public interface IProductService
    {
        Task<ProductDTO> Get(int i_productId);
        Task<IEnumerable<ProductDTO>> GetAll();
    }
}
