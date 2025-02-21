using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace LibrarySTIVE.Models;


[Table("Commande")]
public class Commande
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("idClient")]
    public int IdClient { get; set; }

    [Column("totalPrice")]
    public double TotalPrice { get; set; }

    [Column("status")]
    public string Status { get; set; } = "Pending";

    [Column("createdAt")]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    [ForeignKey("IdClient")]
    public virtual ClientSTIVE? Client { get; set; }

    public virtual ICollection<ArticlesInCommande>? Articles { get; set; }
}


public class CommandeDTO
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("idClient")]
    public int IdClient { get; set; }

    [Column("totalPrice")]
    public double TotalPrice { get; set; }

    [Column("status")]
    public string Status { get; set; } = "Pending";

    [Column("createdAt")]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public int Client { get; set; }
    
    public List<int> Articles { get; set; } = new List<int>();
}
