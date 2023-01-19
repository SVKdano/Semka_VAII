﻿using System;
using System.Collections.Generic;

namespace SemestralkaBE.Models;

public partial class League
{
    public int Id { get; set; }

    public virtual ICollection<Team> Teams { get; } = new List<Team>();
}
