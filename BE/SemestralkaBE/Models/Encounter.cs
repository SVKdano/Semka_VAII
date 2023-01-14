using System;
using System.Collections.Generic;

namespace SemestralkaBE.Models;

public partial class Encounter
{
    public int Id { get; set; }

    public DateOnly Date { get; set; }

    public TimeOnly Time { get; set; }

    public int Place { get; set; }

    public int Host { get; set; }

    public int? Guest { get; set; }

    public int? HostsWins { get; set; }

    public int? GuestsWins { get; set; }

    public int Round { get; set; }

    public virtual ICollection<Double> Doubles { get; } = new List<Double>();

    public virtual Team? GuestNavigation { get; set; }

    public virtual Team HostNavigation { get; set; } = null!;

    public virtual Place PlaceNavigation { get; set; } = null!;

    public virtual ICollection<Single> Singles { get; } = new List<Single>();
}
