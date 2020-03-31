using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entities.Concrete
{
    [Table("Tb_Url")]
    public class Url:Base
    {

        public int UserId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public int IsActive { get; set; }
    }
}
