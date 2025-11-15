
namespace Meteo.ViewModels
{
    public class Pagination
    {
        public int TotalCount { get; set; }
        public int TotalPage =>TotalCount>0? Math.Max(1, (int)Math.Ceiling((double)TotalCount / ItemsPerPage)):0;
        public int CurrentPage { get; set; } = 1;
        public short ItemsPerPage { get; set; } = 10;
    }
}
