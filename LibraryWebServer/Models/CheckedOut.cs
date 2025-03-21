using System;
using System.Collections.Generic;

namespace LibraryWebServer.Models;

public partial class CheckedOut
{
    public uint CardNum { get; set; }

    public uint Serial { get; set; }
}
