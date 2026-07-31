namespace eSport.UI.Shared.Models.Catalog
{
    public partial record CountryModel
    {
        public string ThreeLetterIsoCode { get; set; }
        public string Name { get; set; }
        public string CountryCSS { get; set; }
    }

}
