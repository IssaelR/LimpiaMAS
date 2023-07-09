using System;
using System.Collections.Generic;

namespace LimpiaMAS.Models;

public partial class TbCliente
{
    public string IdCli { get; set; } = null!;

    public string NomCli { get; set; } = null!;

    public string ApeCli { get; set; } = null!;

    public string DirCli { get; set; } = null!;

    public string TelCli { get; set; } = null!;

    public string Usr { get; set; } = null!;

    public string Pwd { get; set; } = null!;

    public virtual ICollection<TbDetalleservicio> TbDetalleservicios { get; set; } = new List<TbDetalleservicio>();

    public virtual ICollection<TbServicio> TbServicios { get; set; } = new List<TbServicio>();
}
