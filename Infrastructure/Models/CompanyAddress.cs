using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HordeFlow.HR.Infrastructure.Models
{
    public class CompanyAddress
    {
        public int CompanyId { get; set; }
        public Company Company { get; set; }
        public int AddressId { get; set; }
        public Address Address { get; set; }
    }
}