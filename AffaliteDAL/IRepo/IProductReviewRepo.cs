using AffaliteDAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AffaliteDAL.IRepo
{
    public interface IProductReviewRepo : IGenericRepository<ProductReviews>
    {
        IQueryable<ProductReviews> GetAllQueryable();
        void Update(ProductReviews entity, int id);
        void Delete(ProductReviews entity, int id);
    }
}
