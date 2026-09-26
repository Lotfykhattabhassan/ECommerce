using AutoMapper;
using MiniECommerce.Modules.Payment.Application.Abstractions;
using MiniECommerce.Modules.Payment.Application.Abstractions.Persistence;
using MiniECommerce.Modules.Payment.Application.DTOs;

namespace MiniECommerce.Modules.Payment.Application.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IPaymentRepository _paymentRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public PaymentService(
            IPaymentRepository paymentRepository,
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _paymentRepository = paymentRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<PaymentResponseDto> CreateAsync(
            CreatePaymentDto dto,
            CancellationToken cancellationToken = default)
        {
            var payment = Domain.Entities.Payment.Create(
                dto.OrderId,
                dto.UserId,
                dto.Amount,
                dto.Method);

            await _paymentRepository.AddAsync(
                payment,
                cancellationToken);

            await _unitOfWork.SaveChangesAsync(
                cancellationToken);

            return _mapper.Map<PaymentResponseDto>(payment);
        }

        public async Task<PaymentResponseDto?> GetByIdAsync(
            Guid id,
            CancellationToken cancellationToken = default)
        {
            var payment = await _paymentRepository.GetByIdAsync(
                id,
                cancellationToken);

            return payment is null
                ? null
                : _mapper.Map<PaymentResponseDto>(payment);
        }

        public async Task<PaymentResponseDto?> GetByOrderIdAsync(
            Guid orderId,
            CancellationToken cancellationToken = default)
        {
            var payment = await _paymentRepository.GetByOrderIdAsync(
                orderId,
                cancellationToken);

            return payment is null
                ? null
                : _mapper.Map<PaymentResponseDto>(payment);
        }

        public async Task<IReadOnlyList<PaymentResponseDto>> GetByUserIdAsync(
            Guid userId,
            CancellationToken cancellationToken = default)
        {
            var payments = await _paymentRepository.GetByUserIdAsync(
                userId,
                cancellationToken);

            return _mapper.Map<IReadOnlyList<PaymentResponseDto>>(
                payments);
        }

        public async Task<PaymentResponseDto> MarkAsProcessingAsync(
            Guid paymentId,
            CancellationToken cancellationToken = default)
        {
            var payment = await GetPaymentOrThrow(
                paymentId,
                cancellationToken);

            payment.MarkAsProcessing();

            await _unitOfWork.SaveChangesAsync(
                cancellationToken);

            return _mapper.Map<PaymentResponseDto>(payment);
        }

        public async Task<PaymentResponseDto> MarkAsPaidAsync(
            Guid paymentId,
            MarkPaymentAsPaidDto dto,
            CancellationToken cancellationToken = default)
        {
            var payment = await GetPaymentOrThrow(
                paymentId,
                cancellationToken);

            payment.MarkAsPaid(dto.TransactionId);

            await _unitOfWork.SaveChangesAsync(
                cancellationToken);

            return _mapper.Map<PaymentResponseDto>(payment);
        }

        public async Task<PaymentResponseDto> MarkAsFailedAsync(
            Guid paymentId,
            CancellationToken cancellationToken = default)
        {
            var payment = await GetPaymentOrThrow(
                paymentId,
                cancellationToken);

            payment.MarkAsFailed();

            await _unitOfWork.SaveChangesAsync(
                cancellationToken);

            return _mapper.Map<PaymentResponseDto>(payment);
        }

        public async Task<PaymentResponseDto> CancelAsync(
            Guid paymentId,
            CancellationToken cancellationToken = default)
        {
            var payment = await GetPaymentOrThrow(
                paymentId,
                cancellationToken);

            payment.Cancel();

            await _unitOfWork.SaveChangesAsync(
                cancellationToken);

            return _mapper.Map<PaymentResponseDto>(payment);
        }
        public async Task<PaymentResponseDto> DeleteAsync(
            Guid paymentId,
            CancellationToken cancellationToken = default)
        {
            var payment = await GetPaymentOrThrow(
                paymentId,
                cancellationToken);

            payment.MarkAsDeleted();

            await _unitOfWork.SaveChangesAsync(
                cancellationToken);

            return _mapper.Map<PaymentResponseDto>(payment);
        }

        private async Task<Domain.Entities.Payment> GetPaymentOrThrow(
            Guid paymentId,
            CancellationToken cancellationToken)
        {
            var payment = await _paymentRepository.GetByIdAsync(
                paymentId,
                cancellationToken);

            if (payment is null)
                throw new KeyNotFoundException(
                    $"Payment with id '{paymentId}' was not found.");

            return payment;
        }
    }
}