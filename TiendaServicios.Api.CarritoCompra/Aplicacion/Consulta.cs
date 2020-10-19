using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TiendaServicios.Api.CarritoCompra.Aplicacion.Dto;
using TiendaServicios.Api.CarritoCompra.Persistencia;
using TiendaServicios.Api.CarritoCompra.RemoteInterface;

namespace TiendaServicios.Api.CarritoCompra.Aplicacion
{
    public class Consulta
    {
        public class Ejecuta : IRequest<CarritoDto> 
        {
            public int CarritoSesionId { get; set; }
        }

        public class Manejador : IRequestHandler<Ejecuta, CarritoDto>
        {
            private readonly CarritoContexto _context;
            private readonly ILibroService _libroService;

            public Manejador(CarritoContexto context, ILibroService libroService)
            {
                _context = context;
                _libroService = libroService;
            }

            public async Task<CarritoDto> Handle(Ejecuta request, CancellationToken cancellationToken)
            {                
                var carritoSesion = await _context.CarritoSesion.FirstOrDefaultAsync(x => x.CarritoSesionId == request.CarritoSesionId);
                var carritoDto = new CarritoDto {
                    CarritoId = carritoSesion.CarritoSesionId,
                    FechaCreacionSesion = carritoSesion.FechaCreacion
                };

                var carritoSesionDetalle = await _context.carritoSesionDetalle.Where(x => x.CarritoSesionId == request.CarritoSesionId).ToListAsync();
                var detalleCarritoDto = new List<CarritoDetalleDto>();
                foreach (var libro in carritoSesionDetalle)
                {
                    var response = await _libroService.GetLibro(new Guid(libro.ProductoSeleccionado));
                    if (response.resultado) 
                    {
                        detalleCarritoDto.Add(new CarritoDetalleDto 
                        { 
                            TituloLibro = response.Libro.Titulo,
                            FechaPublicacion = response.Libro.FechaPublicacion,
                            LibroId = response.Libro.LibroMaterialId
                        });
                    }
                }

                carritoDto.ListaProductos = detalleCarritoDto;

                return carritoDto;
            }
        }

    }
}
