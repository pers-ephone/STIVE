using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibrarySTIVE.Models
{
    public class ClientModel
    {
        [Key] // Définit cette propriété comme clé primaire
        [Column("id")]
        public int Id { get; set; }

        [Required] // Rend la valeur obligatoire
        [Column("name")]
        public string Name { get; set; }

        [Column("address")]
        public string Address { get; set; }

        [Column("mail")]
        public string Mail { get; set; }

        [Column("postcode")]
        public string Postcode { get; set; }

        [Column("city")]
        public string City { get; set; }

        [Column("country")]
        public string Country { get; set; }
    }
}