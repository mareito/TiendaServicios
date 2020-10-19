﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TiendaServicios.Api.Libro.Aplicacion.Dto
{
    public class LibroMaterialDto
    {
        public Guid? LibroMaterialId { get; set; }
        public string Titulo { get; set; }
        public DateTime? FechaPublicacion { get; set; }
        public Guid? GuidAutorLibro { get; set; }
    }
}
