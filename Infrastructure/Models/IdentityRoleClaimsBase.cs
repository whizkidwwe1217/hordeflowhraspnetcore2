using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace HordeFlow.HR.Infrastructure.Models
{
    public abstract class IdentityRoleClaimsBase: IdentityRoleClaim<int>, ICompanyEntity
    {
        [Timestamp]
        public byte[] RowVersion { get; set; }
        public bool? IsDeleted { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int? UserModifiedId { get; set; }
        public int? UserCreatedId { get; set; }
        public int? CompanyId { get; set; }
        public Company Company { get; set; }    
    }
}