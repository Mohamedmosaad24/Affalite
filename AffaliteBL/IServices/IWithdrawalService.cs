using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AffaliteBL.DTOs.WithdrawalDto;
using AffaliteDAL.Entities;

namespace AffaliteBL.IServices
{
    public interface IWithdrawalService
    {
        IEnumerable<WithdrawRequestDto> GetAll();
        WithdrawRequest Add(string userId, CreateWithdrawalDto dto);
        WithdrawRequest Update(UpdateWithdrawalDto dto);
        Task<IEnumerable<WithdrawRequest>> GetByAffiliateId(int affiliateId);
        Task<IEnumerable<WithdrawRequest>> GetByMerchantId(int merchantId);
    }
}
