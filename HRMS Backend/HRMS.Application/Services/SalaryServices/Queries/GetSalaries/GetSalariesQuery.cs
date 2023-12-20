using HRMS.Application.Helpers.Pagination;
using MediatR;

namespace HRMS.Application.Services.SalaryServices.Queries.GetSalaries
{
    public class GetSalariesQuery: IRequest<PaginatedDtO>
    {

        int pageNumber = 1;

        public int PageNumber { get { return pageNumber; } set { if (value > 1) pageNumber = value; } }

        int pageSize = 9;   
        public int PageSize { get { return pageSize; } set { if (value > 1) pageSize = value; } }
        public string Search { get; set; } = "";
        public string Month { get; set; } = "";
        public string Year { get; set; } = "";
    }



}
