using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entities.Concrete
{
    [Table("Tb_UrlScheduler")]
    public class UrlScheduler:Base
    {
        public int UrlId { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int WithInternalInMinutes { get; set; }
        public int RepeatForever { get; set; }
        public int IsActive { get; set; }
    }
}
