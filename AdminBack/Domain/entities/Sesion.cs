﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace AdminBack.Models;

public partial class Sesion
{
    public int IdSesion { get; set; }

    public int? IdUsuario { get; set; }

    public Guid? Token { get; set; }

    public DateTime FechaInicio { get; set; }

    public DateTime FechaExpiracion { get; set; }

    public bool? Activo { get; set; }

    public virtual Usuario IdUsuarioNavigation { get; set; }
}