using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace E_Commerce.Application.DTOs.Baskets
{
    public class BasketItemDto
    {
        public int Id { get; set; }
        public string ProductName { get; set; } = default!;
        public string PictureUrl { get; set; } = default!;
        [Range(1,double.MaxValue)]
        public decimal Price { get; set; }
        [Range(1, 90)]

        public int Quantity { get; set; }
    }
}
