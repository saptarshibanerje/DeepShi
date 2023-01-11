using System;
using System.Collections.Generic;
using System.Text;

namespace DeepShiShared.Models
{
    public class ApiResponse<T>
    {
        public ApiResponse()
        {
            Status = 0;  
        }
        public int Status{ get; set; }
        public string StatusMessage { get; set; }        
        public T Data { get; set; }
        public AppMessage Message { get; set; }
    }

    public class ApiResponse
    {
        public ApiResponse()
        {
            Status = 0;

        }
        public int Status { get; set; }
        public string StatusMessage { get; set; }
        public AppMessage Message { get; set; }
    }
}
