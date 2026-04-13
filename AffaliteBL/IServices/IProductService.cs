using AffaliteBL.Helpers;
using AffaliteBLL.DTOs.Products;
//using AffaliteBLL.DTOs.Products;
using AffaliteDAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AffaliteBL.IServices
{
    public interface IProductService
    {
        IEnumerable<Product> GetAll(ProductQueryParams query);
        Product? GetById(int id);
        void Create(Product product);
        void Update(int id, UpdateProductDto dto);
        void Delete(int id);
    }
}
