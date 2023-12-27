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
    public class ProductRepository : IProductRepository
    {
        ApplicationDbContext _db;
        private readonly IMapper _mapper;

        public ProductRepository(ApplicationDbContext i_db, IMapper i_mapper)
        {
            _db = i_db;
            _mapper = i_mapper;
        }

        public async Task<ProductDTO> Create(ProductDTO i_productDTO)
        {
            Product i_productDB = _mapper.Map<ProductDTO, Product>(i_productDTO);

            _db.Products.Add(i_productDB);
            await _db.SaveChangesAsync();

            return _mapper.Map<Product, ProductDTO>(i_productDB);
        }

        public async Task<ProductDTO> Update(ProductDTO i_productDTO)
        {
            var i_productDB = await _db.Products.FirstOrDefaultAsync(item => item.Id == i_productDTO.Id);
            if (i_productDB != null)
            {
                i_productDB.Name = i_productDTO.Name;
                i_productDB.Description = i_productDTO.Description;
                i_productDB.ImageUrl = i_productDTO.ImageUrl;
                i_productDB.CategoryId = i_productDTO.CategoryId;
                i_productDB.Color = i_productDTO.Color;
                i_productDB.ShopFavorites = i_productDTO.ShopFavorites;
                i_productDB.CustomerFavorites = i_productDTO.CustomerFavorites;
                _db.Products.Update(i_productDB);
                await _db.SaveChangesAsync();
                return _mapper.Map<Product, ProductDTO>(i_productDB);
            }
            return i_productDTO;

        }

        public async Task<int> Delete(int i_id)
        {
            var i_productDB = await _db.Products.FirstOrDefaultAsync(item => item.Id == i_id);
            if (i_productDB != null)
            {
                _db.Products.Remove(i_productDB);
                return await _db.SaveChangesAsync();
            }
            return 0;
        }

        public async Task<ProductDTO> Get(int i_id)
        {
            //Recuperation des données de produit tout en precisant que c'est y compris ses clés étrangère avec leur informations
            var obj = await _db.Products.Include(item=>item.Category).Include(item => item.ProductPrices).FirstOrDefaultAsync(item => item.Id == i_id);
            if (obj != null)
            {
                return _mapper.Map<Product, ProductDTO>(obj);
            }
            return new ProductDTO();
        }

        public async Task<IEnumerable<ProductDTO>> GetAll()
        {
            return _mapper.Map<IEnumerable<Product>, IEnumerable<ProductDTO>>(_db.Products.Include(item => item.Category).Include(item => item.ProductPrices));
        }
    }
}
