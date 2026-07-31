namespace eSport.UI.Shared.Models.Data
{
    public record FilterModel
    {
        public string Name { get; set; } = string.Empty;
        public bool Selected { get; set; } = false;
        public int Id { get; set; } = 0;
    }
}
