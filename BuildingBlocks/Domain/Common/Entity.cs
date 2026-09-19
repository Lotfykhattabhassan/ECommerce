using MiniECommerce.BuildingBlocks.Domain.Events;

namespace MiniECommerce.BuildingBlocks.Domain.Common
{
    public abstract class Entity<TId>
    {
        public TId Id { get; private set; } = default!;

        public DateTime CreatedAt { get; protected set; }
        public TId? CreatedBy { get; protected set; }
        public DateTime? UpdatedAt { get; protected set; }
        public TId? UpdatedBy { get; protected set; }
        public bool IsDeleted { get; protected set; }

        public DateTime? DeletedAt { get; protected set; }
        public TId? DeletedBy { get; protected set; }

        protected Entity()
        {
            CreatedAt = DateTime.UtcNow;
        }

        protected Entity(TId id)
        {
            Id = id;
            CreatedAt = DateTime.UtcNow;
        }

        public void MarkAsDeleted(TId? deletedBy = default)
        {
            if (IsDeleted)
                return;

            IsDeleted = true;
            DeletedAt = DateTime.UtcNow;
            DeletedBy = deletedBy;
        }

        public void Restore()
        {
            if (!IsDeleted)
                return;

            IsDeleted = false;
            DeletedAt = null;
            DeletedBy = default;
        }

        public void MarkAsUpdated(TId? updatedBy = default)
        {
            UpdatedAt = DateTime.UtcNow;
            UpdatedBy = updatedBy;
        }

        private readonly List<IDomainEvent> _domainEvents = [];

        public IReadOnlyCollection<IDomainEvent> DomainEvents
            => _domainEvents.AsReadOnly();

        protected void AddDomainEvent(IDomainEvent domainEvent)
        {
            _domainEvents.Add(domainEvent);
        }

        protected void RemoveDomainEvent(IDomainEvent domainEvent)
        {
            _domainEvents.Remove(domainEvent);
        }

        public void ClearDomainEvents()
        {
            _domainEvents.Clear();
        }
    }
}