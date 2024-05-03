namespace Domain.Abstraction.Transport
{
    public class ListParameters
    {
        public string OrderBy { get; set; } = string.Empty;

        public bool Descending { get; set; }

        public int PageNumber { get; set; } = 1;

        public int PageSize { get; set; } = 30;
    }
}
