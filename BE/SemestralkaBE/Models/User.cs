using System;
using System.Collections.Generic;

namespace SemestralkaBE.Models;

public partial class User
{
    public int Id { get; set; }

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string? Token { get; set; }

    public DateOnly? Verifieddate { get; set; }
}
