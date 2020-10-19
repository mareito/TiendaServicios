using FluentValidation;
using MediatR;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TiendaServicios.Api.Libro.Modelo;
using TiendaServicios.Api.Libro.Persistencia;

namespace TiendaServicios.Api.Libro.Aplicacion
{
    public class Nuevo
    {
        public class Ejecuta : IRequest 
        {
            public string Titulo { get; set; }
            public DateTime? FechaPublicacion { get; set; }
            public Guid? AutorLibro { get; set; }
        }

        public class EjecutaVailidacion : AbstractValidator<Ejecuta> 
        {
            public EjecutaVailidacion()
            {
                RuleFor(c => c.Titulo).NotEmpty();
                RuleFor(c => c.FechaPublicacion).NotEmpty();
                RuleFor(c => c.AutorLibro).NotEmpty();
            }
        }

        public class Manejador : IRequestHandler<Ejecuta>
        {

            private readonly ContextoLibreria _context;

            public Manejador(ContextoLibreria context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                var libro = new LibroMaterial
                {                    
                    Titulo = request.Titulo,
                    FechaPublicacion = request.FechaPublicacion,
                    GuidAutorLibro = request.AutorLibro
                };

                _context.LibroMaterial.Add(libro);
                var result = await _context.SaveChangesAsync();

                if (result > 0) 
                {
                    return Unit.Value;
                }

                throw new Exception("El Libro no se ha Guardado");
                
            }
        }
    }
}
