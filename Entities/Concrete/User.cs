using Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entities.Concrete
{
    [Table("Tb_User")]
    public class User : Base
    {
        public string MailAddress { get; set; }
        public string Password { get; set; }
        public int IsActive { get; set; }
        public int IsAdmin { get; set; }
    }
}
