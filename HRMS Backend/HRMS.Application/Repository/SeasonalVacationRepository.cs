using HRMS.Application.Models.SeasonalVacationDto;
using HRMS.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Application.Repository
{
    public class SeasonalVacationRepository : ISeasonalVacationRepository
    {
        private readonly DBContext _context;

        public SeasonalVacationRepository(DBContext context)
        {
            _context = context;
        }

        public bool Create(SeasonalVacationDto createModel)
        {
            var seasonalVacation = new SeasonalVacation
            {
                Name = createModel.Name,
                VacationDate = createModel.VacationDate,
            };

            _context.SeasonalVacations.Add(seasonalVacation);
            _context.SaveChanges();

            return seasonalVacation.Id != null;
        }

        public IEnumerable<SeasonalVacation> GetAll()
        {
            return _context.SeasonalVacations.ToList();
        }

        public SeasonalVacation GetById(int id)
        {
            return _context.SeasonalVacations.FirstOrDefault(s => s.Id == id);
        }

        public bool Update(int id, SeasonalVacationDto updateModel)
        {
            var seasonalVacation = _context.SeasonalVacations.FirstOrDefault(s => s.Id == id);
            if (seasonalVacation == null)
            {
                return false;
            }

            seasonalVacation.Name = updateModel.Name;
            seasonalVacation.VacationDate = updateModel.VacationDate;

            _context.SaveChanges();

            return true;
        }

        public bool Delete(int id)
        {
            var seasonalVacation = _context.SeasonalVacations.FirstOrDefault(s => s.Id == id);
            if (seasonalVacation == null)
            {
                return false;
            }

            _context.SeasonalVacations.Remove(seasonalVacation);
            _context.SaveChanges();

            return true;
        }
    }
}
