using AutoMapper;
using Exe.Starot.Application.Common.Mappings;
using Exe.Starot.Application.Product;
using Exe.Starot.Domain.Entities.Base;
using System.Linq;

namespace Exe.Starot.Application.Payment
{
    public class PaymentDto : IMapFrom<PaymentEntity>
    {
        public string PaymentId { get; set; } = string.Empty;
        public string PaymentMethod { get; set; } = string.Empty;
        public DateTime PaymentDate { get; set; } = DateTime.Now;
        public string PaymentStatus { get; set; } = string.Empty;
        public string TransactionId { get; set; } = string.Empty;

        public string ProductName { get; set; } = string.Empty;
        public decimal ProductPrice { get; set; }
        public List<ProductDto> Products { get; set; } = new List<ProductDto>();


        public void Mapping(Profile profile)
        {
            profile.CreateMap<PaymentEntity, PaymentDto>()
                .ForMember(dest => dest.PaymentId, opt => opt.MapFrom(src => src.PaymentId))
                .ForMember(dest => dest.PaymentMethod, opt => opt.MapFrom(src => src.Method))
                .ForMember(dest => dest.PaymentDate, opt => opt.MapFrom(src => src.PaymentDate))
                .ForMember(dest => dest.PaymentStatus, opt => opt.MapFrom(src => src.Status))
                .ForMember(dest => dest.TransactionId, opt => opt.MapFrom(src => src.TransactionId));
            //.ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Transaction.Order.OrderDetails.FirstOrDefault().Product.Name))
            //.ForMember(dest => dest.ProductPrice, opt => opt.MapFrom(src => src.Transaction.Order.OrderDetails.FirstOrDefault().Product.Price))
            //  .ForMember(dest => dest.Products, opt => opt.MapFrom(src => src.Transaction.Order.OrderDetails.Select(od => od.Product)));
        }
    }
}



