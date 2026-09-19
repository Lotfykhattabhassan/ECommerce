
namespace MiniECommerce.BuildingBlocks.Domain.Events
{
    public interface IDomainEvent
    {
        public DateTime OccurredOn { get; }
    }
}
