using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace HordeFlow.HR.Infrastructure.Models
{
    public class Designation : CompanyEntity
    {
        public string Name { get; set; }
        public bool? Active { get; set; }
        [JsonIgnore] 
        public virtual ICollection<Employee> Employees { get; set; }
    }
}