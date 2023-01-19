using System;
using System.Collections.Generic;

namespace SemestralkaBE.Models;

public partial class Team
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int League { get; set; }

    public virtual ICollection<Encounter> EncounterGuestNavigations { get; } = new List<Encounter>();

    public virtual ICollection<Encounter> EncounterHostNavigations { get; } = new List<Encounter>();

    public virtual League LeagueNavigation { get; set; } = null!;

    public virtual ICollection<Place> Places { get; } = new List<Place>();

    public virtual ICollection<Player> Players { get; } = new List<Player>();
}
