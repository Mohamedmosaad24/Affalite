using AffaliteDAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AffaliteDAL.IRepo
{
    public interface ICommissionRepo:IGenericRepository<Commission>
    {
        IEnumerable<Commission> GetAllCommissionsByMerchant(int merchantId);

    }
}
