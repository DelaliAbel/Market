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
using System.Threading.Tasks;

namespace MarketWeb_Business.Repository
{
    public class OrderRepository : IOderRepository
    {
        private readonly IDbContextFactory<ApplicationDbContext> _dbContextFactory;
        private readonly IMapper _mapper;

        public OrderRepository(IDbContextFactory<ApplicationDbContext> dbContextFactory, IMapper mapper)
        {
            _dbContextFactory = dbContextFactory;
            _mapper = mapper;
        }

        public async Task<OrderDTO> Create(OrderDTO objDTO)
        {
            using (var dbContext = _dbContextFactory.CreateDbContext())
            {
                try
                {
                    var obj = _mapper.Map<OrderDTO, Order>(objDTO);
                    dbContext.OrderHeaders.Add(obj.OrderHeader);
                    await dbContext.SaveChangesAsync();

                    foreach (var details in obj.OrderDetails)
                    {
                        details.OrderHeaderId = obj.OrderHeader.Id;
                    }
                    dbContext.OrderDetails.AddRange(obj.OrderDetails);
                    await dbContext.SaveChangesAsync();

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
            }
        }

        public async Task<int> Delete(int id)
        {
            using (var dbContext = _dbContextFactory.CreateDbContext())
            {
                var objHeader = await dbContext.OrderHeaders.FirstOrDefaultAsync(u => u.Id == id);

                if (objHeader != null)
                {
                    var objDetail = dbContext.OrderDetails.Where(u => u.OrderHeaderId == id);
                    dbContext.OrderDetails.RemoveRange(objDetail);
                    dbContext.OrderHeaders.Remove(objHeader);
                    return await dbContext.SaveChangesAsync();
                }
                return 0;
            }
        }

        public async Task<OrderDTO> Get(int id)
        {
            using (var dbContext = _dbContextFactory.CreateDbContext())
            {
                var order = new Order()
                {
                    OrderHeader = await dbContext.OrderHeaders.FirstOrDefaultAsync(u => u.Id == id),
                    OrderDetails = dbContext.OrderDetails.Where(u => u.OrderHeaderId == id).ToList()
                };

                return order != null ? _mapper.Map<Order, OrderDTO>(order) : new OrderDTO();
            }
        }

        public async Task<IEnumerable<OrderDTO>> GetAll(string? userId = null, string? status = null)
        {
            using (var dbContext = _dbContextFactory.CreateDbContext())
            {
                var ordersFromDb = new List<Order>();

                var orderHeaders = dbContext.OrderHeaders.ToList();
                var orderDetails = dbContext.OrderDetails.ToList();

                foreach (var header in orderHeaders)
                {
                    var order = new Order()
                    {
                        OrderHeader = header,
                        OrderDetails = orderDetails.Where(u => u.OrderHeaderId == header.Id).ToList()
                    };
                    ordersFromDb.Add(order);
                }
                // Faire certains filtres #TODO

                return _mapper.Map<IEnumerable<Order>, IEnumerable<OrderDTO>>(ordersFromDb);
            }
        }

        public async Task<OrderHeaderDTO> MarkPayementSuccessful(int id)
        {
            using (var dbContext = _dbContextFactory.CreateDbContext())
            {
                var data = await dbContext.OrderHeaders.FindAsync(id);

                if (data == null)
                {
                    return new OrderHeaderDTO();
                }
                if (data.Status == StaticDetails.Status_Pending)
                {
                    data.Status = StaticDetails.Status_Confirmed;
                    await dbContext.SaveChangesAsync();
                    return _mapper.Map<OrderHeader, OrderHeaderDTO>(data);
                }
                return new OrderHeaderDTO();
            }
        }

        public async Task<OrderHeaderDTO> UpdateHeader(OrderHeaderDTO objDTO)
        {
            using (var dbContext = _dbContextFactory.CreateDbContext())
            {
                if (objDTO != null)
                {
                    var orderHeader = _mapper.Map<OrderHeaderDTO, OrderHeader>(objDTO);
                    dbContext.OrderHeaders.Update(orderHeader);
                    await dbContext.SaveChangesAsync();
                    return _mapper.Map<OrderHeader, OrderHeaderDTO>(orderHeader);
                }
                return new OrderHeaderDTO();
            }
        }

        public async Task<bool> UpdateOrderStatus(int orderId, string status)
        {
            using (var dbContext = _dbContextFactory.CreateDbContext())
            {
                var data = await dbContext.OrderHeaders.FindAsync(orderId);

                if (data == null)
                {
                    return false;
                }

                data.Status = status;

                if (data.Status == StaticDetails.Status_Shipped)
                {
                    data.ShippingDate = DateTime.Now;
                }

                await dbContext.SaveChangesAsync();
                return true;
            }
        }
    }
}
