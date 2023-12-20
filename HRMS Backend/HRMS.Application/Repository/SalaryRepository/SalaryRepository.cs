using HRMS.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Application.Repository.SalaryRepository
{
    public class SalaryRepository : ISalaryRepository
    {
        private readonly DBContext dbcontext;

        public SalaryRepository(DBContext dbcontext)
        {
            this.dbcontext = dbcontext;
        }
        public IQueryable<Employee> GetAll(Expression<Func<Employee, bool>> where)
        {
            return dbcontext.Set<Employee>().AsNoTracking().Where(where).AsQueryable();

        }

    }
}
