using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplicationCheckUrl.Models
{
    public class JobModel
    {
        public string Key { get; set; }
        public string Url { get; set; }
        public string MailAddress { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int Minutes { get; set; }
        public bool RepeatForever { get; set; }
    }
}
