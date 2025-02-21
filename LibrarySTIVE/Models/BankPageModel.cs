using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibrarySTIVE.Models
{
    public class BankPageModel
    {
        [Key] // Définit cette propriété comme clé primaire
        [Column("id")]
        public int Id { get; set; }

        [Required] // Rend la valeur obligatoire
        [Column("name")]
        public string Name { get; set; }

        [Column("number")]
        public long Number { get; set; }

        [Column("date")]
        public string Date { get; set; }

        [Column("crypto")]
        public long Crypto { get; set; }

    }
}