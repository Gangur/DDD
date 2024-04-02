namespace IntegrationEvents
{
    public record ProductCreatedIntegrationEvent
    {
        public ProductCreatedIntegrationEvent(Guid id, string name, decimal price)
        {
            Id = id;
            Name = name;
            Price = price;
        }

        public Guid Id { get; init; }

        public string Name { get; init; } = string.Empty;

        public decimal Price { get; init; }
        
    }
}
