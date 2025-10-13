using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy_Management_System.Domain.Entities.Roles
{
    public sealed class Role : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }

    }
}
