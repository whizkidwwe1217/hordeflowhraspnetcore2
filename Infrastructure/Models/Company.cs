using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HordeFlow.HR.Infrastructure.Models
{
    public class Company : BaseEntity
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public bool? Active { get; set; }
        public int? ParentCompanyId { get; set; }
        public Company ParentCompany { get; set; }
        public virtual ICollection<Company> Companies { get; set;}
        public virtual ICollection<Employee> Employees { get; set; }
        public virtual ICollection<Department> Departments { get; set; }
        public virtual ICollection<Team> Teams { get; set; }
        public virtual ICollection<Designation> Designations { get; set; }
        public int? CompanyAddressId { get; set; }
        public virtual ICollection<CompanyAddress> CompanyAddresses { get; set; }
    }
}