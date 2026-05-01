namespace AffaliteBL.DTOs.AiDTOS
{
    public class MarketingGenerationRequestDto
    {
        public string Audience { get; set; } = "عام";
        public string Tone { get; set; } = "مقنع";
        public string CampaignGoal { get; set; } = "زيادة المبيعات";
        public bool IncludeHashtags { get; set; } = true;
        public string Language { get; set; } = "ar";
    }
}
