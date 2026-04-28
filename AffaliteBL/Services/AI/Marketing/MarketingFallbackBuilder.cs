using AffaliteBL.DTOs.AiDTOS;

namespace AffaliteBL.Services.AI.Marketing
{
    public interface IMarketingFallbackBuilder
    {
        PlatformPostsDto Build(MarketingProductContext context, MarketingGenerationRequestDto options);
    }

    public class MarketingFallbackBuilder : IMarketingFallbackBuilder
    {
        public PlatformPostsDto Build(MarketingProductContext context, MarketingGenerationRequestDto options)
        {
            var product = string.IsNullOrWhiteSpace(context.ProductName) ? "هذا المنتج" : context.ProductName;
            var categoryPart = string.IsNullOrWhiteSpace(context.CategoryName) ? string.Empty : $" ضمن فئة {context.CategoryName}";
            var socialProof = context.ReviewsCount > 0
                ? $"تقييم {context.AverageRating}/5 من {context.ReviewsCount} مراجعة."
                : "منتج موثوق بجودة ممتازة.";

            var pricePart = context.Price > 0 ? $"السعر {context.Price} جنيه." : "سعر تنافسي.";
            var hashtags = options.IncludeHashtags ? "\n#تسوق #Affiliate #عروض #منتجات" : string.Empty;

            return new PlatformPostsDto
            {
                Facebook = $"اكتشف {product}{categoryPart}.\n{context.Description}\n{socialProof}\n{pricePart}\nاطلب الآن واستفد من أفضل قيمة.",
                Instagram = $"{product} ✨\n{context.Description}\n{pricePart}{hashtags}",
                Twitter = $"{product} - {context.Description} {pricePart} اطلبه الآن.",
                LinkedIn = $"نقدم {product} كخيار احترافي{categoryPart}. {socialProof} {pricePart} مناسب لتحقيق {options.CampaignGoal}."
            };
        }
    }
}
