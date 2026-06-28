using AutoMapper;
using E_Commerce.Application.DTOs.Products;
using E_Commerce.Domain.Entities.Products;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace E_Commerce.Application.Profiles
{
    public class PictureUrlResolver(IOptions<UrlSetting> options) : IValueResolver<Product, ProductDto, string?>
    {
        private readonly UrlSetting _urlSetting=options.Value;
        public string? Resolve(Product source, ProductDto destination, string? destMember, ResolutionContext context)
        {
        if(string.IsNullOrEmpty(source.PictureUrl))
                return null;
        var baseUrl=_urlSetting.BaseUrl.TrimEnd('/');
            var path=source.PictureUrl.TrimStart("/");
            return $"{baseUrl}/Files/{path}";

        }
    }
    public class UrlSetting {
        public string BaseUrl { get; set; } = string.Empty;
    }
}
