using AutoMapper;
using MarketWeb_Business.Repository.IRepository;
using MarketWeb_DataAccess;
using MarketWeb_DataAccess.Data;
using MarketWeb_Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketWeb_Business.Repository
{
    public class ProductPriceRepository : IProductPriceRepository
    {
        ApplicationDbContext _db;
        private readonly IMapper _mapper;

        public ProductPriceRepository(ApplicationDbContext i_db, IMapper i_mapper)
        {
            _db = i_db;
            _mapper = i_mapper;
        }

        public async Task<ProductPriceDTO> Create(ProductPriceDTO i_productPriceDTO)
        {
            ProductPrice productPriceDB = _mapper.Map<ProductPriceDTO, ProductPrice>(i_productPriceDTO);

            _db.ProductPrices.Add(productPriceDB);
            await _db.SaveChangesAsync();

            return _mapper.Map<ProductPrice, ProductPriceDTO>(productPriceDB);
        }

        public async Task<ProductPriceDTO> Update(ProductPriceDTO i_productPriceDTO)
        {
            var productPriceDB = await _db.ProductPrices.FirstOrDefaultAsync(item => item.Id == i_productPriceDTO.Id);
            if (productPriceDB != null)
            {
                productPriceDB.ProductId = i_productPriceDTO.ProductId;
                productPriceDB.Size = i_productPriceDTO.Size;
                productPriceDB.Price = i_productPriceDTO.Price;
                _db.ProductPrices.Update(productPriceDB);
                await _db.SaveChangesAsync();
                return _mapper.Map<ProductPrice, ProductPriceDTO>(productPriceDB);
            }
            return i_productPriceDTO;
        }

        public async Task<int> Delete(int i_id)
        {
            var productPriceDB = await _db.ProductPrices.FirstOrDefaultAsync(item => item.Id == i_id);
            if (productPriceDB != null)
            {
                _db.ProductPrices.Remove(productPriceDB);
                return await _db.SaveChangesAsync();
            }
            return 0;
        }

        public async  Task<ProductPriceDTO> Get(int i_id)
        {
            var obj = await _db.ProductPrices.FirstOrDefaultAsync(item => item.Id == i_id);
            if (obj != null)
            {
                return _mapper.Map<ProductPrice, ProductPriceDTO>(obj);
            }
            return new ProductPriceDTO();
        }

        public async Task<IEnumerable<ProductPriceDTO>> GetAll(int? i_id = null)
        {
            if(i_id!=null & i_id>0)
            {
                return _mapper.Map<IEnumerable<ProductPrice>, IEnumerable<ProductPriceDTO>>
                    (_db.ProductPrices.Where(item => item.ProductId==i_id));
            }
            else
            {
                return _mapper.Map<IEnumerable<ProductPrice>, IEnumerable<ProductPriceDTO>>(_db.ProductPrices);
            }
        }
    }
}
