using AutoMapper;
using MarketWeb_Business.Repository.IRepository;
using MarketWeb_DataAccess;
using MarketWeb_DataAccess.Data;
using MarketWeb_Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarketWeb_Business.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly IDbContextFactory<ApplicationDbContext> _dbContextFactory;
        private readonly IMapper _mapper;

        public CategoryRepository(IDbContextFactory<ApplicationDbContext> dbContextFactory, IMapper mapper)
        {
            _dbContextFactory = dbContextFactory;
            _mapper = mapper;
        }

        public async Task<CategoryDTO> Create(CategoryDTO categoryDTO)
        {
            using (var dbContext = _dbContextFactory.CreateDbContext())
            {
                var category = _mapper.Map<CategoryDTO, Category>(categoryDTO);
                category.CreatedDate = DateTime.Now;

                dbContext.Categories.Add(category);
                await dbContext.SaveChangesAsync();

                return _mapper.Map<Category, CategoryDTO>(category);
            }
        }

        public async Task<int> Delete(int id)
        {
            using (var dbContext = _dbContextFactory.CreateDbContext())
            {
                var objFromDB = await dbContext.Categories.FirstOrDefaultAsync(item => item.Id == id);
                if (objFromDB != null)
                {
                    dbContext.Categories.Remove(objFromDB);
                    return await dbContext.SaveChangesAsync();
                }
                return 0;
            }
        }

        public async Task<CategoryDTO> Get(int id)
        {
            using (var dbContext = _dbContextFactory.CreateDbContext())
            {
                var obj = await dbContext.Categories.FirstOrDefaultAsync(item => item.Id == id);
                return obj != null ? _mapper.Map<Category, CategoryDTO>(obj) : new CategoryDTO();
            }
        }

        public async Task<IEnumerable<CategoryDTO>> GetAll()
        {
            using (var dbContext = _dbContextFactory.CreateDbContext())
            {
                return _mapper.Map<IEnumerable<Category>, IEnumerable<CategoryDTO>>(dbContext.Categories);
            }
        }

        public async Task<CategoryDTO> Update(CategoryDTO objFromDTO)
        {
            using (var dbContext = _dbContextFactory.CreateDbContext())
            {
                var objFromDB = await dbContext.Categories.FirstOrDefaultAsync(item => item.Id == objFromDTO.Id);
                if (objFromDB != null)
                {
                    objFromDB.Name = objFromDTO.Name;
                    dbContext.Categories.Update(objFromDB);
                    await dbContext.SaveChangesAsync();
                    return _mapper.Map<Category, CategoryDTO>(objFromDB);
                }
                return objFromDTO;
            }
        }
    }
}
