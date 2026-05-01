using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AffaliteDAL.Entities;

namespace AffaliteBL.DTOs.WithdrawalDto
{
    public class CreateWithdrawalDto
    {
        public decimal Amount { get; set; }
        public string Number { get; set; }
        public WithdrawalMethod Method { get; set; }
    }
}
