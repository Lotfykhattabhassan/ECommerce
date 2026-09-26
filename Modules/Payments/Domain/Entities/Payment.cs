using MiniECommerce.BuildingBlocks.Domain.Common;
using MiniECommerce.Modules.Payment.Domain.Enums;

namespace MiniECommerce.Modules.Payment.Domain.Entities
{
    public class Payment : Entity<Guid>
    {
        public Guid OrderId { get; private set; }

        public Guid UserId { get; private set; }

        public decimal Amount { get; private set; }

        public PaymentMethod Method { get; private set; }

        public PaymentStatus Status { get; private set; }

        public string? TransactionId { get; private set; }

        public DateTime CreatedAt { get; private set; }

        public DateTime? PaidAt { get; private set; }

        private Payment()
        {
        }

        private Payment(
            Guid id,
            Guid orderId,
            Guid userId,
            decimal amount,
            PaymentMethod method)
            : base(id)
        {

            OrderId = orderId;
            UserId = userId;
            Amount = amount;
            Method = method;

            Status = PaymentStatus.Pending;

            CreatedAt = DateTime.UtcNow;
        }

        public static Payment Create(
            Guid orderId,
            Guid userId,
            decimal amount,
            PaymentMethod method)
        {
            if (orderId == Guid.Empty)
                throw new ArgumentException("Order ID is required.");

            if (userId == Guid.Empty)
                throw new ArgumentException("User ID is required.");

            if (amount <= 0)
                throw new ArgumentException("Payment amount must be greater than zero.");
            
            return new Payment( Guid.NewGuid(),
                orderId,
                userId,
                amount,
                method);
        }

        public void MarkAsProcessing()
        {
            if (Status != PaymentStatus.Pending)
                throw new InvalidOperationException(
                    "Only pending payments can be marked as processing.");

            Status = PaymentStatus.Processing;
        }

        public void MarkAsPaid(string transactionId)
        {
            if (Status != PaymentStatus.Processing)
                throw new InvalidOperationException(
                    "Only processing payments can be marked as paid.");

            if (string.IsNullOrWhiteSpace(transactionId))
                throw new ArgumentException(
                    "Transaction ID is required.");

            Status = PaymentStatus.Paid;
            TransactionId = transactionId;
            PaidAt = DateTime.UtcNow;
        }

        public void MarkAsFailed()
        {
            if (Status != PaymentStatus.Processing)
                throw new InvalidOperationException(
                    "Only processing payments can be marked as failed.");

            Status = PaymentStatus.Failed;
        }

        public void Cancel()
        {
            if (Status == PaymentStatus.Paid)
                throw new InvalidOperationException(
                    "Paid payment cannot be cancelled.");

            if (Status == PaymentStatus.Cancelled)
                throw new InvalidOperationException(
                    "Payment is already cancelled.");

            Status = PaymentStatus.Cancelled;
        }
    }
}