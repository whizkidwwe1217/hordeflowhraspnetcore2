using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HordeFlow.HR.Infrastructure.Models;

namespace HordeFlow.HR.Infrastructure.Models
{
    public abstract class CompanyEntity : ICompanyEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Timestamp]
        public string ConcurrencyStamp { get; set; }
        public bool? IsDeleted { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int? UserModifiedId { get; set; }
        public int? UserCreatedId { get; set; }
        public int? CompanyId { get; set; }
        public Company Company { get; set; }
    }
} 