namespace EMS.Data.Models
{
    public class GenericResponse<T> where T : class
    {
        public bool Status { get; set; } = false;
        public string Message { get; set; }
        public T Data { get; set; }
    }
}