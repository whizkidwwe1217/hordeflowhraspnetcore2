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
        public ICollection<Company> Companies { get; set;}
        public ICollection<Employee> Employees { get; set; }
        public ICollection<Department> Departments { get; set; }
        public ICollection<Team> Teams { get; set; }
        public ICollection<Designation> Designations { get; set; }
        public ICollection<CompanyAddress> CompanyAddresses { get; set; }
    }
}