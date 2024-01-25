using MarketWeb_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketWeb_Business.Repository.IRepository
{
    public interface IOderRepository
    {
        public Task<OrderDTO> Get(int i_id);
        public Task<IEnumerable<OrderDTO>> GetAll(string? i_userId = null, string? i_status = null);
        public Task<OrderDTO> Create(OrderDTO i_objDTO);
        public Task<int> Delete(int i_id);
        public Task<OrderHeaderDTO> UpdateHeader(OrderHeaderDTO i_objDTO);
        public Task<OrderHeaderDTO> MarkPayementSuccessful(int id);
        public Task<bool> UpdateOrderStatus(int orderId, string status);
    }
}
