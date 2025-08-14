using AutoMapper;
using MarketWeb_Business.Repository.IRepository;
using MarketWeb_DataAccess;
using MarketWeb_DataAccess.Data;
using MarketWeb_Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarketWeb_Business.Repository
{
    public class ProductPriceRepository : IProductPriceRepository
    {
        private readonly IDbContextFactory<ApplicationDbContext> _dbContextFactory;
        private readonly IMapper _mapper;

        public ProductPriceRepository(IDbContextFactory<ApplicationDbContext> dbContextFactory, IMapper mapper)
        {
            _dbContextFactory = dbContextFactory;
            _mapper = mapper;
        }

        public async Task<ProductPriceDTO> Create(ProductPriceDTO productPriceDTO)
        {
            using (var dbContext = _dbContextFactory.CreateDbContext())
            {
                var productPriceDB = _mapper.Map<ProductPriceDTO, ProductPrice>(productPriceDTO);

                dbContext.ProductPrices.Add(productPriceDB);
                await dbContext.SaveChangesAsync();

                return _mapper.Map<ProductPrice, ProductPriceDTO>(productPriceDB);
            }
        }

        public async Task<ProductPriceDTO> Update(ProductPriceDTO productPriceDTO)
        {
            using (var dbContext = _dbContextFactory.CreateDbContext())
            {
                var productPriceDB = await dbContext.ProductPrices.FirstOrDefaultAsync(item => item.Id == productPriceDTO.Id);
                if (productPriceDB != null)
                {
                    productPriceDB.ProductId = productPriceDTO.ProductId;
                    productPriceDB.Size = productPriceDTO.Size;
                    productPriceDB.Price = productPriceDTO.Price;
                    dbContext.ProductPrices.Update(productPriceDB);
                    await dbContext.SaveChangesAsync();
                    return _mapper.Map<ProductPrice, ProductPriceDTO>(productPriceDB);
                }
                return productPriceDTO;
            }
        }

        public async Task<int> Delete(int id)
        {
            using (var dbContext = _dbContextFactory.CreateDbContext())
            {
                var productPriceDB = await dbContext.ProductPrices.FirstOrDefaultAsync(item => item.Id == id);
                if (productPriceDB != null)
                {
                    dbContext.ProductPrices.Remove(productPriceDB);
                    return await dbContext.SaveChangesAsync();
                }
                return 0;
            }
        }

        public async Task<ProductPriceDTO> Get(int id)
        {
            using (var dbContext = _dbContextFactory.CreateDbContext())
            {
                var productPrice = await dbContext.ProductPrices.FirstOrDefaultAsync(item => item.Id == id);
                return productPrice != null ? _mapper.Map<ProductPrice, ProductPriceDTO>(productPrice) : new ProductPriceDTO();
            }
        }

        public async Task<IEnumerable<ProductPriceDTO>> GetAll(int? id = null)
        {
            using (var dbContext = _dbContextFactory.CreateDbContext())
            {
                var query = dbContext.ProductPrices.AsQueryable();
                if (id.HasValue && id > 0)
                {
                    query = query.Where(item => item.ProductId == id);
                }
                var productPrices = await query.ToListAsync();
                return _mapper.Map<IEnumerable<ProductPrice>, IEnumerable<ProductPriceDTO>>(productPrices);
            }
        }
    }
}
