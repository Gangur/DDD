namespace Application.Data
{
    public record Result
    {
        public bool Success { get; init; }

        public string ErrorMessage { get; init; }

        internal Result(bool success)
        {
            this.Success = success;
        }

        internal Result(string errorMessage)
        {
            this.ErrorMessage = errorMessage;
        }

        public static Result CreateSuccessful()
            => new Result(true);

        public static Result CreateFailed(string errorMessage)
            => new Result(errorMessage);
    }

    public sealed record Result<T> : Result
    {
        public T Value { get; set; }

        private Result(bool success, T value) : base(success) 
        {
            Value = value;
        }

        private Result(string errorMessage) : base(errorMessage)
        {

        }

        public static Result<T> CreateSuccessful(T value)
            => new Result<T>(true, value);

        public static new Result<T> CreateFailed(string errorMessage)
            => new Result<T>(errorMessage);
    }
}
