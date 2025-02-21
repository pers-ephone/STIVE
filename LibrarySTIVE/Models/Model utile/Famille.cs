using System.ComponentModel.DataAnnotations.Schema;

namespace LibrarySTIVE.Models
{
    [Table("famille")]
    public class Famille
    {
        [Column("id")]
        public int Id { get; private set; }

        [Column("name")]
        public string Name { get; set; }

        public List<Article> Articles { get; set; } = new List<Article>();  // Relation avec Article
    }
}