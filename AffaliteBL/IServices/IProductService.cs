using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AffaliteBL.Helpers;
//using AffaliteBLL.DTOs.Products;
using AffaliteDAL.Entities;

namespace AffaliteBL.IServices
{
    public interface IProductService
    {
        IEnumerable<Product> GetAll(ProductQueryParams query);
        Product? GetById(int id);
        void Create(Product product);
        void Update(int id, Product product);
        void Delete(int id);
    }
}
