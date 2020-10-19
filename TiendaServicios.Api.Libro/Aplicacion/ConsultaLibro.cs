using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TiendaServicios.Api.Libro.Aplicacion.Dto;
using TiendaServicios.Api.Libro.Modelo;
using TiendaServicios.Api.Libro.Persistencia;

namespace TiendaServicios.Api.Libro.Aplicacion
{
    public class ConsultaLibro
    {


        public class LibroUnico : IRequest<LibroMaterialDto> 
        {
            public Guid? LibroId { get; set; }
        }


        public class Manejador : IRequestHandler<LibroUnico, LibroMaterialDto> 
        {
            private readonly ContextoLibreria _context;
            private readonly IMapper _mapper;

            public Manejador(ContextoLibreria context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<LibroMaterialDto> Handle(LibroUnico request, CancellationToken cancellationToken)
            {
                var libro = await _context.LibroMaterial.Where(c => c.LibroMaterialId == request.LibroId).SingleOrDefaultAsync();
                return _mapper.Map<LibroMaterial, LibroMaterialDto>(libro);
            }
        }
        
    }
}
