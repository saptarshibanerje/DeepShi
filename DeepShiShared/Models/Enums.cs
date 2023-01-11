using System;
using System.Collections.Generic;
using System.Text;

namespace DeepShiShared.Models
{
    public enum MessageType
    {
        Success,
        Error,
        Warning,
        Info,
        Question
    };
    public enum PageMode { Insert, Update, Delete, Read };
}
