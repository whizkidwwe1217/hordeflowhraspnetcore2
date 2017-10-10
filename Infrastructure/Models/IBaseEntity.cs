using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HordeFlow.HR.Infrastructure.Models
{  
    public interface IBaseEntity
    {
        int Id { get; set; } 

        [Timestamp]
        byte[] RowVersion { get; set; }
        bool? IsDeleted { get; set; }
        DateTime? CreatedDate { get; set; }
        DateTime? ModifiedDate { get; set;}
        int? UserModifiedId { get; set; }
        int? UserCreatedId { get; set; }
    }
}