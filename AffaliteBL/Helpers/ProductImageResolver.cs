using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AffaliteBL.Helpers
{
    using System.Runtime;
    using AffaliteBLL.DTOs.Products;
    using AffaliteDAL.Entities;
    using AutoMapper;
    using Mattger_BL.Helpers;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Options;
    public class ImageUrlResolver : IValueResolver<Product, ProductDto, string>
    {
        private readonly ApiSettings _settings;

        public ImageUrlResolver(IOptions<ApiSettings> options)
        {
            _settings = options.Value;
        }

        public string Resolve(Product source, ProductDto destination, string destMember, ResolutionContext context)
        {
            if (string.IsNullOrEmpty(source.ImageUrl))
                return null;

            return $"{_settings.BaseUrl}images/products/{source.ImageUrl}";
        }

    }
    //public class CartItemImageUrlResolver : IValueResolver<CartItem, CartItemDTO, string>
    //{
    //    private readonly ApiSettings _settings;

    //    public CartItemImageUrlResolver(IOptions<ApiSettings> options)
    //    {
    //        _settings = options.Value;

    //    }

    //    public string Resolve(CartItem source, CartItemDTO destination, string destMember, ResolutionContext context)
    //    {
    //        if (source.Product == null || string.IsNullOrEmpty(source.Product.PictureUrl))
    //            return null;
    //        return $"{_settings.BaseUrl}images/{source.Product.PictureUrl}";

    //    }

    //}
}