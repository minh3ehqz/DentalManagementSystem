using System;
using System.Collections.Generic;
namespace DentalManagementSystem.Models
{
    public class Log
    {
        public long Id { get; set; }    
        public DateTime eventTime { get; set; }
        public string content { get; set; }
    }
}
