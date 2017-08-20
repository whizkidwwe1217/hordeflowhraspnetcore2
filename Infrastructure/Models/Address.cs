using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HordeFlow.HR.Infrastructure.Models
{
    public class Address : BaseEntity
    {
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string AddressType { get; set; }
        public bool IsPrimary { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
        public int? StateId { get; set; }
        public State State { get; set; }

        public ICollection<EmployeeAddress> EmployeeAddresses { get; set; }
        public ICollection<CompanyAddress> CompanyAddresses { get; set; }
    }
}