using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AffaliteDAL.Data;
using AffaliteDAL.Entities;
using AffaliteDAL.IRepo;

namespace AffaliteDAL.Repo
{
    public class CategoryRepo : GenericRepository<Category>, ICategoryRepo
    {
        public CategoryRepo(AffaliteDBContext context) : base(context)
        {
        }
    }
}