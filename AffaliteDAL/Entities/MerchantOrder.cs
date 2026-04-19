using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace AffaliteDAL.Entities
{
    public class MerchantOrder
    {
        public int MerchantId { get; set; }
        public int OrderId { get; set; }
        [JsonIgnore]
        public Order? Order { get; set; }
        public Merchant? Merchant { get; set; }
    }
}
