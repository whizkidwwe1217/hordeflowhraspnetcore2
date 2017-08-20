using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HordeFlow.HR.Infrastructure.Models
{
    public class EmployeeAddress
    {
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }
        public int AddressId { get; set; }
        public Address Address { get; set; }
    }
}