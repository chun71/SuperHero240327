using System;
using System.Collections.Generic;

namespace SuperHero240327.Models;

public partial class Character
{
    public long ID { get; set; }

    public string Name { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string Place { get; set; }

    public DateTime CreateTime { get; set; }

    public DateTime UpdateTime { get; set; }
}
