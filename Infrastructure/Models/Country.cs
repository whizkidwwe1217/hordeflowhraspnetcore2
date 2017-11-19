using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HordeFlow.HR.Infrastructure.Models
{
    public class Country : BaseEntity
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public virtual ICollection<State> States { get; set; }
    }
}