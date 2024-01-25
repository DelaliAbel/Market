using AutoMapper;
using MarketWeb_Business.Repository.IRepository;
using MarketWeb_Common;
using MarketWeb_DataAccess;
using MarketWeb_DataAccess.Data;
using MarketWeb_DataAccess.ViewModel;
using MarketWeb_Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketWeb_Business.Repository
{
    public class OrderRepository : IOderRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;

        public OrderRepository(ApplicationDbContext i_db, IMapper i_mapper)
        {
            _db = i_db;
            _mapper = i_mapper;
        }

        public async Task<OrderDTO> Create(OrderDTO i_objDTO)
        {
            try
            {
                var obj = _mapper.Map<OrderDTO, Order>(i_objDTO);
                _db.OrderHeaders.Add(obj.OrderHeader);
                await _db.SaveChangesAsync();

                foreach(var details in obj.OrderDetails)
                {
                    details.OrderHeaderId = obj.OrderHeader.Id;
                }
                _db.OrderDetails.AddRange(obj.OrderDetails); // AddRange permet d'ajouter une liste directement
                await _db.SaveChangesAsync();

                return new OrderDTO()
                {
                    OrderHeader = _mapper.Map<OrderHeader, OrderHeaderDTO>(obj.OrderHeader),
                    OrderDetails = _mapper.Map<IEnumerable<OrderDetail>, IEnumerable<OrderDetailDTO>>(obj.OrderDetails).ToList()
                };

            }
            catch (Exception)
            {

                throw;
            }

            return i_objDTO;
        }

        public async Task<int> Delete(int i_id)
        {
            var objHeader = await _db.OrderHeaders.FirstOrDefaultAsync(u=>u.Id == i_id);
            
            if(objHeader == null)
            {
                IEnumerable<OrderDetail> objDetail = _db.OrderDetails.Where(u=>u.OrderHeaderId == i_id);

                _db.OrderDetails.RemoveRange(objDetail);
                _db.OrderHeaders.Remove(objHeader);
                return _db.SaveChanges();
            }
            return 0;
        }

        public async Task<OrderDTO> Get(int i_id)
        {
            Order order = new()
            {
                OrderHeader = _db.OrderHeaders.FirstOrDefault(u => u.Id == i_id),
                OrderDetails = _db.OrderDetails.Where(u => u.OrderHeaderId == i_id)
            };

            if(order == null)
            {
                return _mapper.Map<Order,OrderDTO>(order);
            }
            return new OrderDTO();
        }

        public async Task<IEnumerable<OrderDTO>> GetAll(string? i_userId = null, string? i_status = null)
        {
            List<Order> OrderFromDb = new List<Order>();
            IEnumerable<OrderHeader> orderHeaderList = _db.OrderHeaders;
            IEnumerable<OrderDetail> orderDetailList = _db.OrderDetails;

            foreach(OrderHeader header in orderHeaderList)
            {
                Order order = new()
                {
                    OrderHeader = header,
                    OrderDetails = orderDetailList.Where(u => u.OrderHeaderId == header.Id)
                };
                OrderFromDb.Add(order);
            }
            //do some filtering #TODO

            return _mapper.Map<IEnumerable<Order>, IEnumerable<OrderDTO>>(OrderFromDb);

        }

        public async Task<OrderHeaderDTO> MarkPayementSuccessful(int id)
        {
            var data = await _db.OrderHeaders.FindAsync(id);

            if(data==null)
            {
                return new OrderHeaderDTO();
            }
            if (data.Status == StaticDetails.Status_Pending)
            {
                data.Status = StaticDetails.Status_Confirmed;
                await _db.SaveChangesAsync();
                return _mapper.Map<OrderHeader, OrderHeaderDTO > (data);
            }
            return new OrderHeaderDTO();
        }

        public async Task<OrderHeaderDTO> UpdateHeader(OrderHeaderDTO i_objDTO)
        {
            if(i_objDTO != null)
            {
                var orderHeader = _mapper.Map<OrderHeaderDTO, OrderHeader>(i_objDTO);
                _db.OrderHeaders.Update(orderHeader);
                await _db.SaveChangesAsync();
                return _mapper.Map<OrderHeader, OrderHeaderDTO>(orderHeader);
            }
            return new OrderHeaderDTO();
        }

        public async Task<bool> UpdateOrderStatus(int orderId, string status)
        {
            var data = await _db.OrderHeaders.FindAsync(orderId);

            if (data == null)
            {
                return false;
            }
            
            data.Status = status;

            if(data.Status == StaticDetails.Status_Shipped)
            {
                data.ShippingDate = DateTime.Now;
            }

            await _db.SaveChangesAsync();
            return true;

        }
    }
}
