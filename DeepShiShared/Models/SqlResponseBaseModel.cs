using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeepShiShared.Models
{
    public class SqlResponseBaseModel
    {
        public long InsertedId { get; set; }
        public long RowAffected { get; set; }
        public long ErrorCode { get; set; }
        public string Message { get; set; }
    }
}
