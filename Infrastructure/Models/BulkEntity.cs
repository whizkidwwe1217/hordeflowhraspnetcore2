using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace HordeFlow.HR.Infrastructure.Models
{
    [NotMapped]
    public class BulkEntity<T> where T : class, IBaseEntity, new()
    {
        public List<T> Added { get; set; }
        public List<T> Edited { get; set; }
        public List<T> Removed { get; set; }
    }
}