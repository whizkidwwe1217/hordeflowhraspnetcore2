using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace HordeFlow.HR.Infrastructure.Models
{
    public abstract class BaseEntity : IBaseEntity
    {
        [Key]
        public int Id { get; set; }

        [Timestamp]
        public byte[] RowVersion { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int? UserModifiedId { get; set; }
        public int? UserCreatedId { get; set; }
    }
}