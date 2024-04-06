namespace Domain.Data
{
    // Stock Keeping Unit
    public record Sku
    {
        private const int DefaultLength = 15;
        private Sku(string value) => Value = value;
        public string Value { get; init; }
        public static Sku Create(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Sku is empty!");

            if (value.Length != DefaultLength)
                throw new ArgumentException($"Sku length is invalid! It is supposed to be {DefaultLength} symbols!");

            return new Sku(value);
        }
    }
}
