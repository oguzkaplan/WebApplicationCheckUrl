using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplicationCheckUrl.Models
{
    public class UrlAndSchedulerModel
    {
        [Required]
        public int UrlId { get; set; }
        
        [Required]
        public int ScheduleId { get; set; }

        [DataType(DataType.Text)]
        [Required]
        public string UrlName { get; set; }

        [DataType(DataType.Text)]
        [Required]
        public string UrlAddress { get; set; }
        
        [Required]
        public bool IsActive { get; set; }

        [Required]
        public bool RepeatForever { get; set; }        

        [Required]
        public string StartDate { get; set; }


        public string EndDate { get; set; }

        [Required]
        public int WithInternalInMinutes { get; set; }

        [Required]
        public int Hour { get; set; }
        
        [Required]
        public int Minute { get; set; }

    }
}
