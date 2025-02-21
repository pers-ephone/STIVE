using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace LibrarySTIVE.Models
{
    public class ModelArticleJson
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("nom")]
        public string Nom { get; set; } = string.Empty;

        [JsonPropertyName("contenance")]
        public int Contenance { get; set; }

        [JsonPropertyName("familleId")]
        public int FamilleId { get; set; }

        [JsonPropertyName("fournisseurId")]
        public int FournisseurId { get; set; }

        [JsonPropertyName("ref")]
        public string Ref { get; set; } = string.Empty;

        [JsonPropertyName("prix")]
        public double Prix { get; set; } = 0;

        [JsonPropertyName("age")]
        public DateTime Age { get; set; }

        [JsonPropertyName("artQuantity")]
        public int ArtQuantity { get; set; }

        public string ImagePath { get; set; } = "https://th.bing.com/th/id/R.8a1ca22578c609b0d59d1c8b52f5482b?rik=bfsxauk6UBuvaA&pid=ImgRaw&r=0";
    }
}


