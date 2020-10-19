using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TiendaServicios.Api.Autor.Aplicacion.Dto;
using TiendaServicios.Api.Autor.Modelo;
using TiendaServicios.Api.Autor.Persistencia;

namespace TiendaServicios.Api.Autor.Aplicacion
{
    public class ConsultaFiltro
    {
        public class AutorUnico : IRequest<AutorDto>
        {
            public string AutorGuid { get; set; }
        }

        public class Manejador : IRequestHandler<AutorUnico, AutorDto>
        {

            private readonly ContextoAutor _context;
            private readonly IMapper mapper;

            public Manejador(ContextoAutor context, IMapper mapper)
            {
                _context = context;
                this.mapper = mapper;
            }

            public async Task<AutorDto> Handle(AutorUnico request, CancellationToken cancellationToken)
            {
                var autor = await _context.AutorLibro.Where(c => c.AutorLibroGuid == request.AutorGuid).SingleOrDefaultAsync();
                if (autor is null) 
                {
                    throw new Exception("No se ha Encontrado el Autor");
                }

                return mapper.Map<AutorLibro, AutorDto>(autor);
            }
        }
    }
}
