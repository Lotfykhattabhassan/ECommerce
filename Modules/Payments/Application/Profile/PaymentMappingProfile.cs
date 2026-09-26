using AutoMapper;
using MiniECommerce.Modules.Payment.Application.DTOs;

namespace MiniECommerce.Modules.Payment.Application.Mapping
{
    public class PaymentMappingProfile : Profile
    {
        public PaymentMappingProfile()
        {
            CreateMap<Domain.Entities.Payment, PaymentResponseDto>();
        }
    }
}