using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AffaliteDAL.Entities;

namespace AffaliteBL.DTOs.WithdrawalDto
{
    public class UpdateWithdrawalDto
    {
        public int Id { get; set; }
        public WithdrawalStatus Status { get; set; }
    }
}
