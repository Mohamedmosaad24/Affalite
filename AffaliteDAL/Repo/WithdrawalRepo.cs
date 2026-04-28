using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AffaliteDAL.Repo
{
    using AffaliteDAL.Data;
    using AffaliteDAL.Entities;
    using AffaliteDAL.IRepo;
    using Microsoft.EntityFrameworkCore;

    public class WithdrawalRepo : GenericRepository<WithdrawRequest>, IWithdrawalRepo
    {
        private readonly AffaliteDBContext _context;

        public WithdrawalRepo(AffaliteDBContext context) : base(context)
        {
            _context = context;
        }

        // ✅ كل الطلبات
        public async Task<IEnumerable<WithdrawRequest>> GetAllAsync()
        {
            return await _context.WithdrawRequests
                .OrderByDescending(x => x.CreatedAt)
                .ToListAsync();
        }

        // ✅ طلبات يوزر معين (Affiliate أو Merchant)
        public async Task<IEnumerable<WithdrawRequest>> GetByUserAsync(int userEntityId, UserType userType)
        {
            return await _context.WithdrawRequests
                .Where(x => x.UserRefId == userEntityId && x.UserType == userType)
                .OrderByDescending(x => x.CreatedAt)
                .ToListAsync();
        }
    }
}
