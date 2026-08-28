namespace eSport.UI.Shared.Models.Catalog
{
    public partial record CountryModel
    {
        public string ThreeLetterIsoCode { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string CountryCSS { get; set; } = string.Empty;
    }

}
