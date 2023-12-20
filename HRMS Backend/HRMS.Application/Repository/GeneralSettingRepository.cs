using HRMS.Application.Models.GeneralSettingDTO;
using HRMS.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Application.Repository
{
    public class GeneralSettingRepository : GenaricRepository<GeneralSettings>, IGeneralSettingRepository
    {
        private readonly DBContext _dBContext;
        public GeneralSettingRepository(DBContext dBContext) : base(dBContext)
        {

            _dBContext = dBContext;

        }

        public int? GetGeneralSettingingID()
        {
            var data = _dBContext.Employees.GroupBy(x => x.SpecialSetting)
              .Select(group => new
              {
                  Value = group.Key,
                  Count = group.Count()
              })
              .OrderByDescending(x => x.Count)
              .FirstOrDefault();
            if (data != null)
                return data.Value;
            return null;
        }

        public GeneralSettings IsExist(GeneralDataDTO obj)

        => _dBContext.GeneralSettings.FirstOrDefault(x => x.Bonus == obj.Bonus && x.VacationDay1 == obj.VacationDay1 && x.Penality == obj.Discount && x.VacationDay2 == obj.VacationDay2);

    }
}
