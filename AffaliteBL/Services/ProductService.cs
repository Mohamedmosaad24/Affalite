using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AffaliteBL.Helpers;
using AffaliteBL.IServices;
using AffaliteDAL.Entities;
using AffaliteDAL.Entities.Enums;
using AffaliteDAL.IRepo;

namespace AffaliteBL.Services
{


    public class ProductService : IProductService
    {
        private readonly IProductRepository _repo;

        public ProductService(IProductRepository repo)
        {
            _repo = repo;
        }

        public IEnumerable<Product> GetAll(ProductQueryParams query)

        {
            var products = _repo.GetQueryable();

            // 🔍 Search
            if (!string.IsNullOrEmpty(query.Search))
                products = products.Where(p => p.Name.Contains(query.Search));

            // 💰 Price filter
            if (query.MinPrice.HasValue)
                products = products.Where(p => p.Price >= query.MinPrice.Value);

            if (query.MaxPrice.HasValue)
                products = products.Where(p => p.Price <= query.MaxPrice.Value);

            // 📌 Status
            if (query.Status.HasValue)
                products = products.Where(p => p.Status == query.Status.Value);

            // 🏷 Category filter
            if (query.CategoryId.HasValue)
                products = products.Where(p => p.CategoryId == query.CategoryId.Value);

            // 🏪 Merchant filter
            if (query.MerchantId.HasValue)
                products = products.Where(p => p.MerchantId == query.MerchantId.Value);

            // 📝 Pagination
            products = products
                .Skip((query.PageNumber - 1) * query.PageSize)
                .Take(query.PageSize);

            // Type
            if (!string.IsNullOrEmpty(query.Type))
                if(query.Type == "new")
                {
                products = products
                .OrderByDescending(p => p.CreatedAt);
                }
            if (query.Type == "top")
            {
                products = products
                .OrderByDescending(p => p.SaleCount);
            }




            return products.ToList();
        }

        public Product? GetById(int id)
        {
            return _repo.GetQueryable().FirstOrDefault(p => p.Id == id);
        }

        public void Create(Product product)
        {
            _repo.Add(product);
            _repo.SaveChanges();
        }

        public void Update(int id, Product product)
        {
            var existing = _repo.GetById(id);
            if (existing == null) return;

            existing.Name = product.Name;
            existing.Description = product.Description;
            existing.Price = product.Price;
            existing.Stock = product.Stock;
            existing.Images = product.Images;
            existing.Reviews = product.Reviews;
            existing.SaleCount = product.SaleCount;
            existing.CategoryId = product.CategoryId;
            existing.MerchantId = product.MerchantId;
            existing.PlatformCommissionPct = product.PlatformCommissionPct;
            existing.Status = product.Status;

            _repo.Update(existing);
            _repo.SaveChanges();
        }

        public void Delete(int id)
        {
            var existing = _repo.GetById(id);
            if (existing == null) return;

            _repo.Delete(existing);
            _repo.SaveChanges();
        }


    }
}
