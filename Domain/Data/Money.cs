﻿namespace Domain.Data
{
    public record Money
    {
        private Money(string currency, decimal amount)
        {
            Currency = currency;
            Amount = amount;
        }

        public string Currency { get; init; }
        public decimal Amount { get; init; }

        public static Money? Create(string currency, decimal amount)
        {
            if (string.IsNullOrWhiteSpace(currency) || currency.Length != 3)
                return null;

            if (amount < 0)
                return null;

            return new Money(currency, amount);
        }
    }
}
