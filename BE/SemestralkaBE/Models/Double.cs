using System;
using System.Collections.Generic;

namespace SemestralkaBE.Models;

public partial class Double
{
    public int Id { get; set; }

    public int EncounterId { get; set; }

    public int HostPlayerFirst { get; set; }

    public int HostPlayerSecond { get; set; }

    public int GuestPlayerFirst { get; set; }

    public int GuestPlayerSecond { get; set; }

    public int HostSetWin { get; set; }

    public int GuestSetWin { get; set; }

    public virtual Encounter Encounter { get; set; } = null!;

    public virtual Player GuestPlayerFirstNavigation { get; set; } = null!;

    public virtual Player GuestPlayerSecondNavigation { get; set; } = null!;

    public virtual Player HostPlayerFirstNavigation { get; set; } = null!;

    public virtual Player HostPlayerSecondNavigation { get; set; } = null!;
}
