using AffaliteDAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AffaliteDAL.IRepo;
public interface IOrderRepo
{
    Order GetById(int id);
    List<Order> GetByAffId(int id);
    List<Order> GetByMerId(int id);
}
