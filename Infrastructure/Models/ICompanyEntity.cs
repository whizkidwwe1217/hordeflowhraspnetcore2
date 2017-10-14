using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using HordeFlow.HR.Infrastructure.Models;

namespace HordeFlow.HR.Infrastructure.Models
{
    public interface ICompanyEntity : IBaseEntity
    {
        int? CompanyId { get; set; }
        Company Company { get; set; }
    }
} 