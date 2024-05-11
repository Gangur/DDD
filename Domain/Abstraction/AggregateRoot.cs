namespace Domain.Abstraction
{
    public abstract class AggregateRoot
    {
        private readonly List<IDomainEvent<IEntityId>> _domainEvents = new();

        protected void Raise(IDomainEvent<IEntityId> domainEvent)
        {
            _domainEvents.Add(domainEvent);
        }

        public IDomainEvent<IEntityId>[] DomainEvents { get => _domainEvents.ToArray(); }

        public void ClearDomainEvents()
            => _domainEvents.Clear();
    }
}
