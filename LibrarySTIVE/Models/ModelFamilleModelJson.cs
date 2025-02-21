using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace LibrarySTIVE.Models
{
    public class ModelFamilleModelJson
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;

        [JsonPropertyName("articles")]
        public List<string> Articles { get; set; } = new();
    }
}