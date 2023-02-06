using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeepShi.ViewModels
{
    public class ComingSoonViewModel
    {
        public string Name { get; set; }
        public string MobileNo { get; set; }
        public string LaunchDate { get; set; }

        public ComingSoonViewModel()
        {
            LaunchDate = "2023/02/14"; //DateTime.Now.AddDays(1).ToString("yyyy/MM/dd");
        }

    }
}
