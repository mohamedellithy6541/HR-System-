using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Application.Helpers.GenericSearchFilter.Enum
{
    public enum FilterOptions
    {
        StartsWith = 1,
        EndsWith,
        Contains,
        DoesNotContain,
        IsEmpty,
        IsNotEmpty,
        IsGreaterThan,
        IsGreaterThanOrEqualTo,
        IsLessThan,
        IsLessThanOrEqualTo,
        IsEqualTo,
        IsNotEqualTo
    }   
}
