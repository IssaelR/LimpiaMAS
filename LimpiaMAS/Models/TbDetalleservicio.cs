using System;
using System.Collections.Generic;

namespace LimpiaMAS.Models;

public partial class TbDetalleservicio
{
    public string IdDetserv { get; set; } = null!;

    public string? IdServ { get; set; }

    public string IdCli { get; set; } = null!;

    public string IdLimp { get; set; } = null!;

    public string CatServ { get; set; } = null!;

    public DateTime FecServ { get; set; }

    public TimeSpan DurServ { get; set; }

    public decimal ImpServ { get; set; }

    public TimeSpan HoraDetserv { get; set; }

    public decimal Area { get; set; }

    public decimal TarifaLimp { get; set; }

    public Guid Guidetserv { get; set; }

    public string NomapeCli { get; set; } = null!;

    public string DirCli { get; set; } = null!;

    public string NomapeLim { get; set; } = null!;

    public virtual TbCliente IdCliNavigation { get; set; } = null!;

    public virtual TbLimpiador IdLimpNavigation { get; set; } = null!;

    public virtual TbServicio? IdServNavigation { get; set; }
}
