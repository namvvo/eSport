
namespace eSport.UI.Shared.Models.Common
{
    public partial record TabModel
    {
        public string Text { get; set; } = default!;    
        public string? Slug { get; set; } = default;
        public bool Selected { get; set; } = false;
    }

}
