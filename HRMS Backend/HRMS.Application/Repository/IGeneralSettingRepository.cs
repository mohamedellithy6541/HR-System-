using HRMS.Application.Models.GeneralSettingDTO;
using HRMS.Domain.Models;

namespace HRMS.Application.Repository
{
    public interface IGeneralSettingRepository : IGenaricrepository<GeneralSettings>
    {
        int? GetGeneralSettingingID();
        GeneralSettings IsExist(GeneralDataDTO obj);
    }
}