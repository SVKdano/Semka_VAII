using System;
using System.Collections.Generic;

namespace SemestralkaBE.Models;

public partial class Single
{
    public int Id { get; set; }

    public int EncounterId { get; set; }

    public int? HostPlayerId { get; set; }

    public int GuestPlayerId { get; set; }

    public int HostSetWin { get; set; }

    public int GuestSetWin { get; set; }

    public virtual Encounter Encounter { get; set; } = null!;

    public virtual Player GuestPlayer { get; set; } = null!;

    public virtual Player? HostPlayer { get; set; }
}
