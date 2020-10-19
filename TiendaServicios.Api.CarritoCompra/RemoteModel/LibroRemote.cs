﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TiendaServicios.Api.CarritoCompra.RemoteModel
{
    public class LibroRemote
    {
        public Guid? LibroMaterialId { get; set; }
        public string Titulo { get; set; }
        public DateTime? FechaPublicacion { get; set; }
        public Guid? GuidAutorLibro { get; set; }
    }
}
