using Application.Enums;

namespace Application.Data
{
    public record Result
    {
        public bool Success { get => this.Type == ResultType.Success; } 
        public ResultType Type { get; init; }

        public string? ErrorMessage { get; init; }

        public Dictionary<string, string>? ValidationProblems { get; private set; }

        protected Result(ResultType type)
        {
            this.Type = type;
        }

        protected Result(string errorMessage, ResultType type)
        {
            this.ErrorMessage = errorMessage;
            this.Type = type;
        }

        protected Result(Dictionary<string, string> validationProblems, ResultType type)
        {
            this.ValidationProblems = validationProblems;
            this.Type = type;
        }

        public static Result CreateSuccessful()
            => new Result(ResultType.Success);

        public static Result CreateValidationProblem(Dictionary<string, string> validationProblems)
            => new Result(validationProblems, ResultType.BadRequest);

        public static Result CreateValidationProblem(string validationProblem)
            => new Result(new Dictionary<string, string> { { "Validation problem!", validationProblem} }, ResultType.BadRequest);

        public static Result CreateNotFount(string errorMessage)
            => new Result(errorMessage, ResultType.NotFount);

        public static Result CreateBadRequest(string errorMessage)
            => new Result(errorMessage, ResultType.BadRequest);

        public static Result CreateUnauthorized()
            => new Result("Unauthorized request failed!", ResultType.Unauthorized);
    }

    public sealed record Result<T> : Result
    {
        public T Value { get; set; }

        private Result(ResultType type, T value) : base(type)
        {
            Value = value;
        }

        private Result(string errorMessage, ResultType type) : base(errorMessage, type) { }

        private Result(Dictionary<string, string> validationProblems, ResultType type) : base(validationProblems, type) { }

        public static Result<T> CreateSuccessful(T value)
            => new Result<T>(ResultType.Success, value);

        public static new Result<T> CreateValidationProblem(Dictionary<string, string> validationProblems)
            => new Result<T>(validationProblems, ResultType.BadRequest);

        public static new Result<T> CreateValidationProblem(string validationProblem)
            => new Result<T>(new Dictionary<string, string> { { "Validation problem!", validationProblem } }, ResultType.BadRequest);

        public static new Result<T> CreateNotFount(string errorMessage)
            => new Result<T>(errorMessage, ResultType.NotFount);

        public static new Result<T> CreateBadRequest(string errorMessage)
            => new Result<T>(errorMessage, ResultType.BadRequest);

        public static new Result<T> CreateUnauthorized()
            => new Result<T>("Unauthorized request failed!", ResultType.Unauthorized);
    }
}
