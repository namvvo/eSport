namespace eSport.Catalog.API.GraphQL.Dto;

public record CountryModel
{
    public string ThreeLetterIsoCode { get; set; } = String.Empty;
    public string Name { get; set; } = String.Empty;
    public string CountryCSS { get; set; } = String.Empty;
}
