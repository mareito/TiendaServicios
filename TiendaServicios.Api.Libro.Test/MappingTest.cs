﻿using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using TiendaServicios.Api.Libro.Aplicacion.Dto;
using TiendaServicios.Api.Libro.Modelo;

namespace TiendaServicios.Api.Libro.Test
{
    public class MappingTest : Profile
    {
        public MappingTest()
        {
            CreateMap<LibroMaterial, LibroMaterialDto>();
        }
    }
}
