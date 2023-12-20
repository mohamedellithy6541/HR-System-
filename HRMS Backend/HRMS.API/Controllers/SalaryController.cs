
using HRMS.Application.Repository;
using HRMS.Application.Repository.SalaryRepository;
using HRMS.Application.Services.SalaryServices.Queries.GetSalaries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HRMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class SalaryController : ControllerBase
    {
        private ISalaryRepository salaryRepository;
        private IMediator _mediator { get; set; }

        public SalaryController( ISalaryRepository salaryRepository, IMediator mediator)
        {
            this.salaryRepository = salaryRepository;
            this._mediator = mediator;
            
        }

        [HttpGet]
        [Route("Get")]
       [Authorize(Policy = "Permission:Salary.View")]
        public async Task<PaginatedDtO>? Get(int pageNumber, int pageSize, string? search, string? month, string? year)
        {
            return  await _mediator.Send(new GetSalariesQuery()
            {
                Month = month,
                Year = year,
                PageNumber = pageNumber,
                PageSize = pageSize,
                Search = search,
            });

        }

    }
}
