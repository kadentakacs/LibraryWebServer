using System;
using System.Collections.Generic;

namespace LibraryWebServer.Models;

public partial class Patrons
{
    public uint CardNum { get; set; }

    public string Name { get; set; } = null!;
}
