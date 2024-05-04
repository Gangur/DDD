namespace Presentation.Adstraction
{
    public class ListResultDto<T> where T : class
    {
        private ListResultDto(int total, IReadOnlyCollection<T> values)
        {
            Total = total;
            Values = values;
        }

        public int Total { get; private set; }

        public IReadOnlyCollection<T> Values { get; private set; }

        public static ListResultDto<T> Create(int total, IReadOnlyCollection<T> values)
            => new ListResultDto<T>(total, values);
    }
}
