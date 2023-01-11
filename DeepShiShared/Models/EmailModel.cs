using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace DeepShiShared.Models
{
    public class EmailModel
    {
        public string EmailFrom { get; set; }
        public string EmailTo { get; set; }
        public string EmailCc { get; set; }
        public string EmailBcc { get; set; }
        public string EmailSubject { get; set; }
        public string EmailBody { get; set; }
        public bool IsEmailBodyHtml { get; set; }
        public MailPriority EmailPriority { get; set; }
    }
}
