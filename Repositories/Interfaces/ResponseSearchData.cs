using System.Collections.Generic;

namespace HordeFlow.HR.Repositories.Interfaces
{
    public class ResponseSearchData : IResponseData<object>
    {
        public virtual IEnumerable<object> data { get; set; }
        public int? total { get; set; }
        public int? pageSize { get; set; }
        public int? pageCount { get; set; }
        public int? currentPage { get; set; }
    }
}