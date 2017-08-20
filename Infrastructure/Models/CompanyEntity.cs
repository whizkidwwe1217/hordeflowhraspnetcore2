using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using HordeFlow.HR.Infrastructure.Models;

namespace HordeFlow.HR.Infrastructure.Models
{
    public abstract class CompanyEntity : BaseEntity
    {
        public int? CompanyId { get; set; }
        public Company Company { get; set; }
    }
} 