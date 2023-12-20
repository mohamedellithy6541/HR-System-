using HRMS.Domain.Models;
using System.Linq.Expressions;

namespace HRMS.Application.Repository.SalaryRepository
{
    public interface ISalaryRepository
    {
        IQueryable<Employee> GetAll(Expression<Func<Employee, bool>> where);
    }
}