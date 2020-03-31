using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entities.Concrete
{
    [Table("Tb_UrlCheckList")]
    public class UrlCheckList:Base
    {
        public int UrlId { get; set; }
        public string StatusCode { get; set; }

    }
}
