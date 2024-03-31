using MediatR;

namespace Domain.Abstraction
{
    public interface IDomainEvent<out TEntityId> : INotification
        where TEntityId : IEntityId
    {
        public TEntityId Id { get; }
    }
}
