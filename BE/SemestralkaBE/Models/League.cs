using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace SemestralkaBE.Models;

public partial class League
{
    public int Id { get; set; }

    public string? Name { get; set; }

    [JsonIgnore]
    public virtual ICollection<Team>? Teams { get; } = new List<Team>();
}
