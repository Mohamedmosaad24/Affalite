using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace AffaliteDAL.Entities
{
    public class MerchantCommissions
    {
        public int Id { get; set; }
        public int MerchantId { get; set; }
        public int CommissionId { get; set; }
        public decimal value { get; set; }
        [JsonIgnore]
        public Commission Commission { get; set; }

    }
}
