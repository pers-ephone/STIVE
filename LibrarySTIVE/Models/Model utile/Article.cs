using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibrarySTIVE.Models
{
    [Table("article")]
    public class Article
    {

        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("name")]
        public string Nom { get; set; }

        [Column("contenance")]
        public int Contenance { get; set; }

        [Column("idFamille")]
        public int FamilleId { get; set; }

        public Famille? Famille { get; set; }

        [Column("idSupplier")]
        public int FournisseurId { get; set; }

        public Fournisseur? Fournisseur { get; set; }


        [Column("ref")]
        public string Ref { get; set; }

        [Column("price")]
        public double Prix { get; set; }

        [Column("age")]
        public DateTime Age { get; set; }

        [Column("artQuantity")]
        public int ArtQuantity { get; set; }



        public Article() { }


    }
}
