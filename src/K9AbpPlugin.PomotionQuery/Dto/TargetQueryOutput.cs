using System.Collections.Generic;

namespace K9AbpPlugin.PomotionQuery.Dto
{
    public sealed class TargetQueryOutput
    {
        public int PromotionId { get; set; }
        public string PromotionName { get; set; }
        public Dictionary<string, string> Columns { get; set; }
        public string Phone { get; set; }
        public Dictionary<string, string> Row { get; set; }
        public string Error { get; set; }
        public bool Success => string.IsNullOrEmpty(Error);
    }
}