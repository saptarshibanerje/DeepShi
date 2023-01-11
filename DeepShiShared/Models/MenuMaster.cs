using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeepShiShared.Models
{
    public class MenuMaster : UserData
    {
        public string MenuIdentity { get; set; }
        public string MenuName { get; set; }
        public string ParentMenuID { get; set; }
        public string ParentMenuName { get; set; }
        public string MenuArea { get; set; }
        public string MenuController { get; set; }
        public string MenuView { get; set; }
        public string MenuStatus { get; set; }
        public string CreatedDate { get; set; }
        public string MenuDisplayNumber { get; set; }
        public string MenuIconClass { get; set; }
    }

    public class UserWiseMenu
    {
        public string UserFullName { get; set; }
        public List<MenuMaster> UserMenu { get; set; }
        public UserWiseMenu()
        {
            UserMenu = new List<MenuMaster>();
        }
    }
}
