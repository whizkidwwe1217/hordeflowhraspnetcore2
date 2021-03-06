using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using HordeFlow.HR.Infrastructure.Models;

namespace HordeFlow.HR.Infrastructure.Models
{
    public class Team : CompanyEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public bool? Active { get; set; }
        public virtual ICollection<Employee> Employees { get; set; }
    }
} 