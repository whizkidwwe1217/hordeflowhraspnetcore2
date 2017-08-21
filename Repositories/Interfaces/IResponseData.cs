using System.Collections.Generic;

namespace HordeFlow.HR.Repositories.Interfaces
{
    public interface IResponseData<T>
    {
        IEnumerable<T> data { get; set; }
        int? total { get; set; }
    }
}