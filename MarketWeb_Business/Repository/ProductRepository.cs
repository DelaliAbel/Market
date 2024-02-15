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
    public class ProductRepository : IProductRepository
    {
        private readonly IDbContextFactory<ApplicationDbContext> _dbContextFactory;
        private readonly IMapper _mapper;

        public ProductRepository(IDbContextFactory<ApplicationDbContext> dbContextFactory, IMapper mapper)
        {
            _dbContextFactory = dbContextFactory;
            _mapper = mapper;
        }

        public async Task<ProductDTO> Create(ProductDTO productDTO)
        {
            using (var dbContext = _dbContextFactory.CreateDbContext())
            {
                var productDB = _mapper.Map<ProductDTO, Product>(productDTO);

                dbContext.Products.Add(productDB);
                await dbContext.SaveChangesAsync();

                return _mapper.Map<Product, ProductDTO>(productDB);
            }
        }

        public async Task<ProductDTO> Update(ProductDTO productDTO)
        {
            using (var dbContext = _dbContextFactory.CreateDbContext())
            {
                var productDB = await dbContext.Products.FirstOrDefaultAsync(item => item.Id == productDTO.Id);
                if (productDB != null)
                {
                    productDB.Name = productDTO.Name;
                    productDB.Description = productDTO.Description;
                    productDB.ImageUrl = productDTO.ImageUrl;
                    productDB.CategoryId = productDTO.CategoryId;
                    productDB.Color = productDTO.Color;
                    productDB.ShopFavorites = productDTO.ShopFavorites;
                    productDB.CustomerFavorites = productDTO.CustomerFavorites;
                    dbContext.Products.Update(productDB);
                    await dbContext.SaveChangesAsync();
                    return _mapper.Map<Product, ProductDTO>(productDB);
                }
                return productDTO;
            }
        }

        public async Task<int> Delete(int id)
        {
            using (var dbContext = _dbContextFactory.CreateDbContext())
            {
                var productDB = await dbContext.Products.FirstOrDefaultAsync(item => item.Id == id);
                if (productDB != null)
                {
                    dbContext.Products.Remove(productDB);
                    return await dbContext.SaveChangesAsync();
                }
                return 0;
            }
        }

        public async Task<ProductDTO> Get(int id)
        {
            using (var dbContext = _dbContextFactory.CreateDbContext())
            {
                var product = await dbContext.Products
                    .Include(item => item.Category)
                    .Include(item => item.ProductPrices)
                    .FirstOrDefaultAsync(item => item.Id == id);

                return product != null ? _mapper.Map<Product, ProductDTO>(product) : new ProductDTO();
            }
        }

        public async Task<IEnumerable<ProductDTO>> GetAll()
        {
            using (var dbContext = _dbContextFactory.CreateDbContext())
            {
                var products = await dbContext.Products
                    .Include(item => item.Category)
                    .Include(item => item.ProductPrices)
                    .ToListAsync();

                return _mapper.Map<IEnumerable<Product>, IEnumerable<ProductDTO>>(products);
            }
        }
    }
}
