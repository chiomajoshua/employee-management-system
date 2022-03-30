namespace EMS.Data.Models
{
    public class PagedRequest
    {
        public int Skip { get; set; } = 0;
        public int Take { get; set; } = 20;
    }
}