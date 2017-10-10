using HordeFlow.HR.Infrastructure.Extensions;
using HordeFlow.HR.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HordeFlow.HR.Infrastructure.Extensions
{
    public class MultiCompanyFilter : EntityCompanyFilter<Employee>
    {
        public override void Filter(EntityTypeBuilder<Employee> b)
        {
            b.HasQueryFilter(e => e.CompanyId == 1);
        }
    }
}