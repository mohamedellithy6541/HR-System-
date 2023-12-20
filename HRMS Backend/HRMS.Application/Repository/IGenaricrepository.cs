using HRMS.Application.Models.GeneralSettingDTO;
using HRMS.Domain.Base;
using HRMS.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Application.Repository
{
    public interface IGenaricrepository<T> where T : BaseEntity
    {
        Task<IEnumerable<T>> GetAllAsync();
        T GetById(int Id);
        void Create(T entity);
        Task Delete(T entity);
        void Edite(T entity);

    }
}
