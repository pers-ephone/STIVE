using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibrarySTIVE.Models;
[Table("ArticlesInCommande")]
public class ArticlesInCommande
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("idCommande")]
    public int IdCommande { get; set; }

    [Column("idArticle")]
    public int IdArticle { get; set; }

    [Column("quantity")]
    public int Quantity { get; set; }

    [Column("price")]
    public double Price { get; set; }
    
    [ForeignKey("IdCommande")]
    public virtual Commande? Commande { get; set; }

    [ForeignKey("IdArticle")]
    public virtual Article? Article { get; set; }
}
public class ArticlesInCommandeDTO
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("idCommande")]
    public int IdCommande { get; set; }

    [Column("idArticle")]
    public int IdArticle { get; set; }

    [Column("quantity")]
    public int Quantity { get; set; }

    [Column("price")]
    public double Price { get; set; }
    
    [ForeignKey("IdCommande")]
    public int Commande { get; set; }

    [ForeignKey("IdArticle")] public List<int> Article { get; set; } = new List<int>();
}
