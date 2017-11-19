using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HordeFlow.HR.Infrastructure.Models
{
    public class Department : CompanyEntity
    {
        public string Name { get; set; }
        public bool? Active { get; set; }
        public virtual ICollection<Employee> Employees { get; set; }
    }
}