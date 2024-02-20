using MarketWeb_Models;

namespace MarketWeb__Client.Service.IService
{
    public interface IOrderService
    {
        public Task<IEnumerable<OrderDTO>> GetAll();
        public Task<OrderDTO> Get(int orderId);
    }
}
