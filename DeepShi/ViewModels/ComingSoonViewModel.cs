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
            LaunchDate = DateTime.Now.AddDays(1).ToString("yyyy/MM/dd");
        }

    }
}
