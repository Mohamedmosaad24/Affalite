using AffaliteDAL.Data;
using AffaliteDAL.Entities;
using AffaliteDAL.IRepo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AffaliteDAL.Repo
{
    public class ProductReviewRepo : IGenericRepository<ProductReviews> , IProductReviewRepo
    {
        private readonly AffaliteDBContext _context;
        public ProductReviewRepo(AffaliteDBContext context)
        { 
            _context = context;
        }
        public void Add(ProductReviews entity)
        {
             _context.ProductReviews.Add(entity);

        }

        public void Delete(ProductReviews entity , int id ) 
            {
                var entityToDelete = _context.ProductReviews.FirstOrDefault(r => r.Id == id);
                if (entityToDelete != null) {
                    _context.ProductReviews.Remove(entityToDelete);
                    _context.SaveChanges();
                }
            }

        public IEnumerable<ProductReviews> GetAll()
        {
            return _context.ProductReviews
                   .OrderByDescending(a=> a.CreatedAt).ToList();
        }

        public IQueryable<ProductReviews> GetAllQueryable()
        {
            return _context.ProductReviews.AsQueryable();
        }
        public ProductReviews? GetById(int id)
        {
            return _context.ProductReviews.FirstOrDefault(r => r.Id == id);    
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public void Update(ProductReviews entity, int id)
        {
            var entityToUpdate = _context.ProductReviews.FirstOrDefault(r => r.Id == id);
            if (entityToUpdate != null)
            {
                entityToUpdate.Comment = entity.Comment;
                entityToUpdate.Rating = entity.Rating;
                _context.ProductReviews.Update(entityToUpdate);
                _context.SaveChanges();
            }
        }

        void IGenericRepository<ProductReviews>.Delete(ProductReviews entity)
        {
             _context.ProductReviews.Remove(entity);

        }

        void IGenericRepository<ProductReviews>.Update(ProductReviews entity)
        {
            _context.ProductReviews.Update(entity);
        }
    }
}
