using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace SemestralkaBE.Models;

public partial class Team
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int League { get; set; }

    [JsonIgnore]
    public virtual ICollection<Encounter> EncounterGuestNavigations { get; } = new List<Encounter>();
    [JsonIgnore]
    public virtual ICollection<Encounter> EncounterHostNavigations { get; } = new List<Encounter>();
    [JsonIgnore]
    public virtual League LeagueNavigation { get; set; } = null!;
    [JsonIgnore]
    public virtual ICollection<Place> Places { get; } = new List<Place>();
    [JsonIgnore]
    public virtual ICollection<Player> Players { get; } = new List<Player>();
}
