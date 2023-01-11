using System;
using System.Collections.Generic;
using System.Text;

namespace DeepShiShared.Models
{
    public class AppMessage
    {
        public string Code { get; set; }
        public MessageType Type { get; set; }
        public string Text { get; set; }
        public string Action { get; set; }
    }
}
