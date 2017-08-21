using System;
using System.Collections.Generic;

namespace HordeFlow.HR.Repositories.Interfaces
{
    public class ResponseData<T> : IResponseData<T>
    {
        public virtual IEnumerable<T> data { get; set; }
        public int? total { get; set; }
    }
}