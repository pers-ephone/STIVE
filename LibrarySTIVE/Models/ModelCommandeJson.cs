using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace LibrarySTIVE.Models
{
    public class ModelCommandeJson
    {

        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("idClient")]
        public int IdClient { get; set; }

        [JsonPropertyName("totalPrice")]
        public double TotalPrice { get; set; }

        [JsonPropertyName("status")]
        public string Status { get; set; } = "Pending";

        [JsonPropertyName("createdAt")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;


        public virtual ClientSTIVE? Client { get; set; }

        public virtual ICollection<ArticlesInCommande>? Articles { get; set; }
    }
}