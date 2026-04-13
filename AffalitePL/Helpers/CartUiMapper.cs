using AffaliteBL.DTOs.CartDTOs;
using AffaliteDAL.Entities;

namespace AffalitePL.Helpers
{
    public static class CartUiMapper
    {
        public static CartUiResponseDto Map(Cart? cart, string imagesBaseUrl)
        {
            if (cart == null)
            {
                return new CartUiResponseDto
                {
                    CartId = 0,
                    Items = new List<CartUiItemDto>(),
                    Summary = EmptySummary()
                };
            }

            var baseTrim = imagesBaseUrl.TrimEnd('/');
            var items = new List<CartUiItemDto>();
            decimal subtotal = 0;

            foreach (var line in cart.Items ?? Array.Empty<CartItem>())
            {
                var p = line.Product;
                var unit = p?.Price ?? 0;
                var lineTotal = unit * line.Quantity;
                subtotal += lineTotal;
                var file = p?.Images?.FirstOrDefault()?.ImageUrl;
                var fullImage = string.IsNullOrEmpty(file)
                    ? string.Empty
                    : $"{baseTrim}/images/products/{file}";
                //var file = p?.ImageUrl;
                //var fullImage = string.IsNullOrEmpty(file)
                //    ? string.Empty
                //    : $"{baseTrim}/images/products/{file}";

                items.Add(new CartUiItemDto
                {
                    ProductId = line.ProductId,
                    ProductName = p?.Name ?? string.Empty,
                    Description = p?.Description ?? string.Empty,
                    Price = unit,
                    //Image = fullImage,
                    Quantity = line.Quantity,
                    Total = lineTotal
                });
            }

            var itemCount = items.Sum(i => i.Quantity);
            const decimal discount = 0;
            const decimal deliveryFee = 0;
            var total = subtotal - discount + deliveryFee;

            return new CartUiResponseDto
            {
                CartId = cart.Id,
                Items = items,
                Summary = new CartUiSummaryDto
                {
                    Subtotal = subtotal,
                    Discount = discount,
                    DeliveryFee = deliveryFee,
                    Total = total,
                    ItemCount = itemCount
                }
            };
        }

        private static CartUiSummaryDto EmptySummary() => new()
        {
            Subtotal = 0,
            Discount = 0,
            DeliveryFee = 0,
            Total = 0,
            ItemCount = 0
        };
    }
}
