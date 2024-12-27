using Domain.Abstraction;

namespace Domain.Extensions
{
    public static class EntityIdExtensions
    {
        public static bool HasValue<TEntityId>(this TEntityId? entityId)
            where TEntityId : IEntityId
            => entityId != null && entityId.Value != default;

        public static bool HasNoValue<TEntityId>(this TEntityId? entityId)
            where TEntityId : IEntityId
            => entityId == null || entityId.Value == default;
    }
}
