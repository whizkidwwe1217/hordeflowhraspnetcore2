using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HordeFlow.HR.Infrastructure.Models
{
    public class Designation : CompanyEntity
    {
        public string Name { get; set; }
        public bool? Active { get; set; }
        public ICollection<Employee> Employees { get; set; }
    }
}