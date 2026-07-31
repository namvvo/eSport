using eSport.ServiceDefaults.Domain;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace Media.API.Models
{
    public class Picture : Entity<int>
    {

        public required string Url { get; set; } // varbinary(max)
        public required string MimeType { get; set; } // nvarchar(40)
        public string SeoFilename { get; set; } = String.Empty; // nvarchar(300)
        public bool IsNew { get; set; } // bit
    }
}
