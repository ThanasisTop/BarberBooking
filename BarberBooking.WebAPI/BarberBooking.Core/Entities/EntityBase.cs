using BarberBooking.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarberBooking.Core.Entities
{
    public class EntityBase: IEntityState
    {
        public DateTime DateCreated { get; set; }

        public DateTime DateModified { get; set; }

        public int CreatedBy { get; set; }

        public int ModifiedBy { get; set; }

        public State State { get; set; }
    }
}
