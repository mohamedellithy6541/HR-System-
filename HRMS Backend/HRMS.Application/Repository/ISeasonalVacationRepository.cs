using HRMS.Application.Models.SeasonalVacationDto;
using HRMS.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace HRMS.Application.Repository
{
    public interface ISeasonalVacationRepository
    {
        IEnumerable<SeasonalVacation> GetAll();
        SeasonalVacation GetById(int id);
        bool Create(SeasonalVacationDto createModel);
        bool Update(int id, SeasonalVacationDto updateModel);
        bool Delete(int id);
    }
}
