using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeepShiEntityModels.Models
{
    public class MenuUserMapping : UserData
    {
        public string MenuUserMappingId { get; set; }
        public string UserId { get; set; }
        public string MenuId { get; set; }
        public string MenuName { get; set; }
        public bool IsCreate { get; set; }
        public bool IsRead { get; set; }
        public bool IsUpdate { get; set; }
        public bool IsDelete { get; set; }
    }
}
