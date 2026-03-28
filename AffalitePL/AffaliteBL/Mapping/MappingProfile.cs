using AffaliteBL.DTOs.AffiliateDTOs;
using AffaliteBL.DTOs.CommissionDTOs;
using AffaliteBL.DTOs.OrderDTOs;
using AffaliteDAL.Entities;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AffaliteBL.Mapping
{
    public class MappingProfile :Profile
    {
        public MappingProfile()
        {
            //Affiliate
            CreateMap<Affiliate, GetAffiliateDTO>().ReverseMap();
            CreateMap<Affiliate, CreateAffiliateDTO>().ReverseMap();
            CreateMap<Affiliate, UpdateAffiliateDTO>().ReverseMap();
            CreateMap<Affiliate, AffiliateBalanceDTO>().ReverseMap();

            CreateMap<Order, OrderReadDTO>().ReverseMap();
            CreateMap<Commission, CommissionReadDTO>().ReverseMap();
            



            //// User
            //CreateMap<AppUser, UserDTO>().ReverseMap();
            //CreateMap<CartItem, CartItemDTO>()
            //    .ForMember(d => d.ProductName,
            //        o => o.MapFrom(s => s.Product.Name))
            //    .ForMember(d => d.Price,
            //        o => o.MapFrom(s => s.Product.Price))
            //    .ForMember(d => d.TotalPrice,
            //    o => o.MapFrom(s => (s.Product.Price * s.Quantity)))
            //    .ForMember(d => d.PictureUrl,
            //        o => o.MapFrom<CartItemImageUrlResolver>()); // 👈 هنا بدل MapFrom مباشرة
            //// OrderItem
            //CreateMap<OrderItem, OrderItemDTO>()
            //    .ForMember(d => d.ProductName,
            //        o => o.MapFrom(s => s.Product.Name))
            //    .ForMember(d => d.Price,
            //        o => o.MapFrom(s => s.Product.Price));

            //// Cart
            //CreateMap<Cart, CartDTO>()
            //    .ForMember(d => d.TotalPrice,
            //        o => o.MapFrom(s => s.Items.Sum(i => i.Product.Price * i.Quantity)));

            //// Order
            //CreateMap<Order, OrderDTO>().ReverseMap();
            //CreateMap<Order, CreateOrderDTO>().ReverseMap();

            //CreateMap<OrderItem, OrderItemDTO>()
            //    .ForMember(dest => dest.ProductName,
            //               opt => opt.MapFrom(src => src.Product.Name));

        }
    }
}