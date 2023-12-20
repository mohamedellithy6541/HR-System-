﻿using HRMS.Application.Helpers.GenericSearchFilter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Threading.Tasks;

namespace HRMS.Application.Helpers.Pagination
{
    public class PaginatedInputModel
    {

        public IEnumerable<FilterParams> FilterParam { get; set; }
     
        int pageNumber = 1;

        public int PageNumber { get { return pageNumber; } set { if (value > 1) pageNumber = value; } }

        int pageSize = 9;
        public int PageSize { get { return pageSize; } set { if (value > 1) pageSize = value; } }

    }
}
