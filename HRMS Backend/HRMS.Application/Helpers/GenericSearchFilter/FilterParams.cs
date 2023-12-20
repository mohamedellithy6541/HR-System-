using HRMS.Application.Helpers.GenericSearchFilter.Enum;

namespace HRMS.Application.Helpers.GenericSearchFilter
{
    public class FilterParams
    {
        public string ColumnName { get; set; } = string.Empty;
        public string FilterValue { get; set; } = string.Empty;
        public FilterOptions FilterOption { get; set; } = FilterOptions.Contains;
    }
}
