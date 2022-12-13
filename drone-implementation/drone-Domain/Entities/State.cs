using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace drone_Domain.Entities
{
    public class State : BaseEntity
    {
        public string Value { get; set; }

    }
}
