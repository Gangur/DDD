namespace Domain.Abstraction
{
    public interface IEntity<T> where T : class, IEntityId
    {
        public T Id { get; }
    }
}
