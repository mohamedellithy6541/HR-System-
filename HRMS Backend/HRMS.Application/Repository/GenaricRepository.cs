using HRMS.Application.Models.GeneralSettingDTO;
using HRMS.Domain.Base;
using HRMS.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Application.Repository
{
    public class GenaricRepository<T> : IGenaricrepository<T> where T : BaseEntity
    {
        private readonly DBContext _dBContext;

        public GenaricRepository(DBContext dBContext)
        {
            _dBContext = dBContext;
        }
        public T GetById(int Id)
           => _dBContext.Set<T>().Find(Id);

        public async Task<IEnumerable<T>> GetAllAsync()

         => await _dBContext.Set<T>().ToListAsync();

        public void Create(T entity)
        {
            _dBContext.Set<T>().Add(entity);
            _dBContext.SaveChanges();
        }
        public async Task Delete(T entity)
        {
            _dBContext.Set<T>().Remove(entity);
            await _dBContext.SaveChangesAsync();

        }
        public void Edite(T entity)
        {
            _dBContext.Set<T>().Update(entity);
            _dBContext.SaveChanges();
        }
    }
}
