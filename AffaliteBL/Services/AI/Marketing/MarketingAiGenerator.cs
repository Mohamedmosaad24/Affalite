using AffaliteBL.DTOs.AiDTOS;
using AffaliteBLL.Services;

namespace AffaliteBL.Services.AI.Marketing
{
    public interface IMarketingAiGenerator
    {
        Task<PlatformPostsDto> GenerateAsync(MarketingProductContext context, MarketingGenerationRequestDto options);
    }

    public class MarketingAiGenerator : IMarketingAiGenerator
    {
        private readonly ClaudeAiClient _client;
        private readonly IMarketingPromptFactory _promptFactory;
        private readonly IMarketingResponseParser _responseParser;

        public MarketingAiGenerator(
            ClaudeAiClient client,
            IMarketingPromptFactory promptFactory,
            IMarketingResponseParser responseParser)
        {
            _client = client;
            _promptFactory = promptFactory;
            _responseParser = responseParser;
        }

        public async Task<PlatformPostsDto> GenerateAsync(MarketingProductContext context, MarketingGenerationRequestDto options)
        {
            var systemPrompt = _promptFactory.BuildSystemPrompt();
            var userPrompt = _promptFactory.BuildUserPrompt(context, options);
            var raw = await _client.SendMessageAsync(userPrompt, systemPrompt);
            return _responseParser.Parse(raw);
        }
    }
}
