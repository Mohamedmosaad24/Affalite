using System.Text;
using System.Text.Json;
using AffaliteBL.DTOs.AiDTOS;

namespace AffaliteBL.Services.AI.Marketing
{
    public interface IMarketingPromptFactory
    {
        string BuildSystemPrompt();
        string BuildUserPrompt(MarketingProductContext context, MarketingGenerationRequestDto options);
    }

    public class MarketingPromptFactory : IMarketingPromptFactory
    {
        public string BuildSystemPrompt()
        {
            return """
                You are an expert affiliate marketing content strategist.
                Return strictly valid JSON only.
                No markdown. No extra text. No explanations.
                Generate platform-specific copy optimized for conversion and credibility.
                """;
        }

        public string BuildUserPrompt(MarketingProductContext context, MarketingGenerationRequestDto options)
        {
            var payload = new
            {
                context = new
                {
                    context.ProductId,
                    context.ProductName,
                    context.Description,
                    context.Details,
                    context.Price,
                    context.CategoryName,
                    context.SaleCount,
                    context.ReviewsCount,
                    context.AverageRating,
                    context.TopReviewHighlights
                },
                options = new
                {
                    options.Audience,
                    options.Tone,
                    options.CampaignGoal,
                    options.IncludeHashtags,
                    options.Language
                },
                platform_rules = new
                {
                    facebook = "Longer persuasive post, 3-5 sentences, clear CTA.",
                    instagram = "Short engaging caption with emojis and hashtags when enabled.",
                    twitter = "Concise catchy post, max 260 characters, no fluff.",
                    linkedIn = "Professional value-focused post, credibility and business tone."
                },
                output_schema = new
                {
                    facebook = "string",
                    instagram = "string",
                    twitter = "string",
                    linkedIn = "string"
                }
            };

            var sb = new StringBuilder();
            sb.AppendLine("Generate high-quality platform-specific marketing posts from this grounding data:");
            sb.AppendLine(JsonSerializer.Serialize(payload));
            sb.AppendLine("Return JSON exactly with keys: facebook, instagram, twitter, linkedIn");
            return sb.ToString();
        }
    }
}
