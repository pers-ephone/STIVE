using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace LibrarySTIVE.Models
{
    [Table("supplier")]
    public class Fournisseur
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("name")]
        public string Name { get; set; }
        [Column("address")]
        public string Adresse { get; set; }
        [JsonIgnore]
        public List<Article> Articles { get; set; } = new List<Article>();  // Relation avec Article
    }


}
