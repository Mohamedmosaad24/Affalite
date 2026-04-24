using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AffaliteDAL.Entities;

namespace AffaliteDAL.IRepo
{
    public interface IWithdrawalRepo : IGenericRepository<WithdrawRequest>
    {
        Task<IEnumerable<WithdrawRequest>> GetAllAsync();
        Task<IEnumerable<WithdrawRequest>> GetByUserAsync(int userEntityId, UserType userType);
    }
}
