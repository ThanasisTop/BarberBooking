using BarberBooking.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarberBooking.Core.Entities
{
    public class EntityBase: IEntityState
    {
        public Guid Id { get; set; }
        public DateTime DateCreated { get; set; }

        public DateTime DateModified { get; set; }

        public Guid CreatedBy { get; set; }

        public Guid ModifiedBy { get; set; }

        public State State { get; set; }

        [NotMapped]
        public string? ErrorMessage { get; set; }

        [NotMapped]
        public string? HasError { get; set; }
    }
}
