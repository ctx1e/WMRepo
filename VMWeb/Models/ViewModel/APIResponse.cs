namespace VMWeb.Models.ViewModel
{
    public class APIResponse<T>
    {
        public string Message { get; set; }
        public T Data { get; set; }
    }
}
