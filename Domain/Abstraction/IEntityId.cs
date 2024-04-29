namespace Domain.Abstraction
{
    public interface IEntityId
    {
        Guid Value { get; init; }
    }
}
