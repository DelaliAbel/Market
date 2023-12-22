using AutoMapper;
using MarketWeb_Business.Repository.IRepository;
using MarketWeb_DataAccess;
using MarketWeb_DataAccess.Data;
using MarketWeb_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketWeb_Business.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        ApplicationDbContext _db;
        private readonly IMapper _mapper;

        public CategoryRepository(ApplicationDbContext i_db, IMapper i_mapper)
        {
            _db = i_db;
            _mapper = i_mapper;
        }

        public CategoryDTO Create(CategoryDTO categoryDTO)
        {
            //Category category = new Category()
            //{
            //    Id = categoryDTO.Id,
            //    Name = categoryDTO.Name,
            //    CreatedDate = DateTime.Now,
            //};

            //Equivalence
            var category = _mapper.Map<CategoryDTO, Category>(categoryDTO);

            _db.Categories.Add(category);
            _db.SaveChanges();

            return _mapper.Map<Category,CategoryDTO>(category);

            //Equivalence
            //return new CategoryDTO()
            //{
            //    Id = category.Id,
            //    Name = category.Name
            //};

        }

        public int Delete(int id)
        {
            throw new NotImplementedException();
        }

        public CategoryDTO Get()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<CategoryDTO> GetAll()
        {
            throw new NotImplementedException();
        }

        public CategoryDTO Update(CategoryDTO categoryDTO)
        {
            throw new NotImplementedException();
        }
    }
}
