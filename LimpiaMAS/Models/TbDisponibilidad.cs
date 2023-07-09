using System;
using System.Collections.Generic;

namespace LimpiaMAS.Models;

public partial class TbDisponibilidad
{
    public string Id { get; set; } = null!;

    public string IdLimp { get; set; } = null!;

    public DateTime FecDis { get; set; }

    public TimeSpan TStart { get; set; }

    public TimeSpan TDone { get; set; }

    public virtual TbLimpiador IdLimpNavigation { get; set; } = null!;
}
