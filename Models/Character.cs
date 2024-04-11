using System;
using System.Collections.Generic;

namespace SuperHero240327.Models;

public partial class Character
{
    public long ID { get; set; }

    public string Name { get; set; } = null!;

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string Place { get; set; } = null!;
}
