using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace SemestralkaBE.Models;

public partial class Place
{
    public int Id { get; set; }

    public int TeamId { get; set; }

    public string Address { get; set; } = null!;
    [JsonIgnore]
    public virtual ICollection<Encounter> Encounters { get; } = new List<Encounter>();
    public virtual Team Team { get; set; } = null!;
}
