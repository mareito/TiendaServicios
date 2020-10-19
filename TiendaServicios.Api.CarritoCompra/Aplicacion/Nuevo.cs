using MediatR;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TiendaServicios.Api.CarritoCompra.Model;
using TiendaServicios.Api.CarritoCompra.Persistencia;

namespace TiendaServicios.Api.CarritoCompra.Aplicacion
{
    public class Nuevo
    {

        public class Ejecuta : IRequest
        {
            public DateTime? FechaCreacionSesion { get; set; }
            public List<string> ProductoLista { get; set; }

        }

        public class Manejador : IRequestHandler<Ejecuta>
        {

            private readonly CarritoContexto _context;

            public Manejador(CarritoContexto context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                var sesion = new CarritoSesion 
                { 
                    FechaCreacion = request.FechaCreacionSesion
                };

                _context.CarritoSesion.Add(sesion);
                var value = await _context.SaveChangesAsync();

                if (value == 0) 
                {
                    throw new Exception("Errores en la inserción"); 
                }

                var id = sesion.CarritoSesionId;

                foreach (var producto in request.ProductoLista) 
                {
                    var detalleSesion = new CarritoSesionDetalle
                    {
                        FechaCreacion = DateTime.Now,
                        CarritoSesionId = id,
                        ProductoSeleccionado = producto
                    };

                    _context.carritoSesionDetalle.Add(detalleSesion);
                }

                value = await _context.SaveChangesAsync();

                if (value > 0) 
                {
                    return Unit.Value;
                }

                throw new Exception("No se Pudo Insertar el Detalle del Carrito de Compras");

            }
        }

    }
}
