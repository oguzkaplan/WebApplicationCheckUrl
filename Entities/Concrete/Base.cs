using Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Entities.Concrete
{
    public class Base : IEntity
    {
        [Key]
        public int Id { get; set; }
        public DateTime CreateDate { get; set; }

    }
}
