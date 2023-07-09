using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace LimpiaMAS.Models;

public partial class TbLimpiador
{
    public string IdLimp { get; set; } = null!;

    public string Usr { get; set; } = null!;

    public string Pwd { get; set; } = null!;

    public string NomLimp { get; set; } = null!;

    public string ApeLimp { get; set; } = null!;

    public string DirLimp { get; set; } = null!;

    public string TelLimp { get; set; } = null!;

    public string DisLimp { get; set; } = null!;

    public decimal TarLimp { get; set; }

    public byte[] FotLimp { get; set; } = null!;

    public string GenLimp { get; set; } = null!;

    public string DistrLimp { get; set; } = null!;

    public string ServLimp { get; set; } = null!;
    [NotMapped]
    public List<string> ServiciosAJSON { get; set; } // Lista de servicios seleccionados en TbLimpiador
    public virtual ICollection<TbDetalleservicio> TbDetalleservicios { get; set; } = new List<TbDetalleservicio>();

    public virtual ICollection<TbDisponibilidad> TbDisponibilidads { get; set; } = new List<TbDisponibilidad>();

    public virtual ICollection<TbServicio> TbServicios { get; set; } = new List<TbServicio>();
}
