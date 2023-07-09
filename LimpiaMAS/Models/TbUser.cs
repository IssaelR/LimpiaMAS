using System;
using System.Collections.Generic;

namespace LimpiaMAS.Models;

public partial class TbUser
{
    public string IdUsr { get; set; } = null!;

    public string Usr { get; set; } = null!;

    public string Pwd { get; set; } = null!;

    public string Nom { get; set; } = null!;

    public string Ape { get; set; } = null!;
}
