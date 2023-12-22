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

        public CategoryDTO Create(CategoryDTO i_categoryDTO)
        {
            //Category category = new Category()
            //{
            //    Id = categoryDTO.Id,
            //    Name = categoryDTO.Name,
            //    CreatedDate = DateTime.Now,
            //};

            //L'auto mapping permet de faire passer les données persistante DTO de l'appli à la BD,
            // puis les mise à jour
            //de la BD sont renvoyer au DTO pour nourir l'appli
            //Très simple

            //Equivalence
            Category category = _mapper.Map<CategoryDTO, Category>(i_categoryDTO);
            category.CreatedDate = DateTime.Now;

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

        public int Delete(int i_id)
        {
            var objFromDB = _db.Categories.FirstOrDefault(item => item.Id==i_id);
            if(objFromDB != null)
            {
                _db.Categories.Remove(objFromDB);
                return _db.SaveChanges();
            }
            return 0;
        }

        public CategoryDTO Get(int i_id)
        {
            var obj = _db.Categories.FirstOrDefault(item => item.Id == i_id);
            if(obj!=null)
            {
                return _mapper.Map<Category,CategoryDTO>(obj);
            }
            return new CategoryDTO();
        }

        public IEnumerable<CategoryDTO> GetAll()
        {
            return _mapper.Map<IEnumerable<Category>, IEnumerable<CategoryDTO>>(_db.Categories);
        }

        public CategoryDTO Update(CategoryDTO objFromDTO)
        {
            var objFromDB = _db.Categories.FirstOrDefault(item => item.Id == objFromDTO.id);
            if (objFromDB)
            {
                objFromDB.Name = objFromDTO.Name;
                _db.Categories.Update(objFromDB);
                _db.SaveChanges();
                return _mapper.Map<Category, CategoryDTO>(objFromDB);
            }
            return objFromDTO;
        }
    }
}
