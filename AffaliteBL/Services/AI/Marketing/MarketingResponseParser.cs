using System.Text.Json;
using AffaliteBL.DTOs.AiDTOS;

namespace AffaliteBL.Services.AI.Marketing
{
    public interface IMarketingResponseParser
    {
        PlatformPostsDto Parse(string rawResponse);
    }

    public class MarketingResponseParser : IMarketingResponseParser
    {
        public PlatformPostsDto Parse(string rawResponse)
        {
            var cleaned = Cleanup(rawResponse);
            var parsed = JsonSerializer.Deserialize<PlatformPostsDto>(cleaned, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            if (parsed == null)
            {
                throw new InvalidOperationException("AI response JSON could not be parsed.");
            }

            return parsed;
        }

        private static string Cleanup(string value)
        {
            var cleaned = value.Trim();
            if (cleaned.StartsWith("```"))
            {
                cleaned = cleaned.Replace("```json", string.Empty)
                                 .Replace("```", string.Empty)
                                 .Trim();
            }

            return cleaned;
        }
    }
}
