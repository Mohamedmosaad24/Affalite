using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AffaliteBL.IServices;
using AffaliteDAL.Entities;
using AffaliteDAL.IRepo;

namespace AffaliteBL.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepo _repo;

        public CategoryService(ICategoryRepo repo)
        {
            _repo = repo;
        }

        public IEnumerable<Category> GetAllCategories()
        {
            return _repo.GetAll();
        }

        public Category? GetCategoryById(int id)
        {
            return _repo.GetById(id);
        }

        public void CreateCategory(Category category)
        {
            if (category == null)
                throw new ArgumentNullException(nameof(category));

            _repo.Add(category);
            _repo.SaveChanges();
        }

        public void UpdateCategory(Category category)
        {
            if (category == null)
                throw new ArgumentNullException(nameof(category));

            var existingCategory = _repo.GetById(category.Id);

            if (existingCategory != null)
            {
                existingCategory.Name = category.Name;
                existingCategory.Slug = category.Slug;

                _repo.Update(existingCategory);
                _repo.SaveChanges();
            }
        }

        public void DeleteCategory(int id)
        {
            var category = _repo.GetById(id);

            if (category != null)
            {
                _repo.Delete(category);
                _repo.SaveChanges();
            }
        }
    }
}