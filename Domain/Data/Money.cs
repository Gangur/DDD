namespace Domain.Data
{
    public record Money
    {
        public const string USD = "USD";

        public const string EUR = "EUR";

        private Money(string currency, decimal amount)
        {
            Currency = currency;
            Amount = amount;
        }

        public string Currency { get; init; }
        public decimal Amount { get; init; }

        public static Money Create(string currency, decimal amount)
        {
            if (string.IsNullOrWhiteSpace(currency) || currency.Length != 3)
                throw new ArgumentException("Invalid format of the currency!");

            if (amount < 0)
                throw new ArgumentException("Invalid amount!");

            return new Money(currency, amount);
        }
    }
}
