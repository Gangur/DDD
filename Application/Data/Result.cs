using Application.Enums;

namespace Application.Data
{
    public record Result
    {
        public bool Success { get => this.Type == ResultType.Success; } 
        public ResultType Type { get; init; }

        public string ErrorMessage { get; init; }

        internal Result(ResultType type)
        {
            this.Type = type;
        }

        internal Result(string errorMessage, ResultType type)
        {
            this.ErrorMessage = errorMessage;
            this.Type = type;
        }

        public static Result CreateSuccessful()
            => new Result(ResultType.Success);

        public static Result CreateValidationProblem(string errorMessage)
            => new Result(errorMessage, ResultType.ValidationProblem);

        public static Result CreateNotFount(string errorMessage)
            => new Result(errorMessage, ResultType.NotFount);
    }

    public sealed record Result<T> : Result
    {
        public T Value { get; set; }

        private Result(ResultType type, T value) : base(type) 
        {
            Value = value;
        }

        private Result(string errorMessage, ResultType type) : base(errorMessage, type)
        {

        }

        public static Result<T> CreateSuccessful(T value)
            => new Result<T>(ResultType.Success, value);

        public static new Result<T> CreateValidationProblem(string errorMessage)
            => new Result<T>(errorMessage, ResultType.ValidationProblem);

        public static new Result<T> CreateNotFount(string errorMessage)
            => new Result<T>(errorMessage, ResultType.NotFount);
    }
}
