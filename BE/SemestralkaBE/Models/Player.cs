using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace SemestralkaBE.Models;

public partial class Player
{
    public int Id { get; set; }

    public int TeamId { get; set; }

    public string Name { get; set; } = null!;

    public string Surname { get; set; } = null!;

    [JsonIgnore]
    public virtual ICollection<Double?>? DoubleGuestPlayerFirstNavigations { get; } = new List<Double>();
    [JsonIgnore]
    public virtual ICollection<Double?>? DoubleGuestPlayerSecondNavigations { get; } = new List<Double>();
    [JsonIgnore]
    public virtual ICollection<Double?>? DoubleHostPlayerFirstNavigations { get; } = new List<Double>();
    [JsonIgnore]
    public virtual ICollection<Double?>? DoubleHostPlayerSecondNavigations { get; } = new List<Double>();
    [JsonIgnore]
    public virtual ICollection<Single?>? SingleGuestPlayers { get; } = new List<Single>();
    [JsonIgnore]
    public virtual ICollection<Single?>? SingleHostPlayers { get; } = new List<Single>();

    public virtual Team? Team { get; set; } = null!;
}
