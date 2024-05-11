namespace Presentation.Adstraction
{
    public class ListResultDto<T> where T : class
    {
        private ListResultDto(int total, T[] values)
        {
            Total = total;
            Values = values;
        }

        public int Total { get; private set; }

        public T[] Values { get; private set; }

        public static ListResultDto<T> Create(int total, T[] values)
            => new ListResultDto<T>(total, values);
    }
}
