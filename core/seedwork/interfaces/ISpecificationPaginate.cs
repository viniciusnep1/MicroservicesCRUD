using System;
using System.Collections.Generic;
using System.Text;

namespace core.seedwork.interfaces
{
    public interface ISpecificationPaginate<T> : ISpecification<T>
    {
        bool Descedescending { get; }
        string Order { get; }
        int Page { get; set; }
        int PageSize { get; set; }
    }
}
