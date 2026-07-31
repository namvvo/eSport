

namespace eSport.UI.Shared.Infrastructure
{
    public class ComponentMetadata
    {
        public string? Name { get; set; }
        public bool Selected { get; set; }
        public string? Url { get; set; }
        public Dictionary<string, object> Parameters { get; set; } =
            new Dictionary<string, object>();
    }
}
