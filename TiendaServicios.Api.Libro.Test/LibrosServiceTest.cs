    using AutoMapper;
using GenFu;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TiendaServicios.Api.Libro.Aplicacion;
using TiendaServicios.Api.Libro.Modelo;
using TiendaServicios.Api.Libro.Persistencia;
using Xunit;

namespace TiendaServicios.Api.Libro.Test
{

    public class LibrosServiceTest
    {

        private IEnumerable<LibroMaterial> ObtenerDataPrueba() 
        {
            A.Configure<LibroMaterial>()
                .Fill(x => x.Titulo).AsArticleTitle()
                .Fill(x => x.LibroMaterialId, () => { return Guid.NewGuid(); });

            var lista = A.ListOf<LibroMaterial>(30);

            lista[0].LibroMaterialId = Guid.Empty;

            return lista;
        }

        private Mock<ContextoLibreria> CrearContexto() 
        {
            var dataPrueba = ObtenerDataPrueba().AsQueryable();
            var dbSet = new Mock<DbSet<LibroMaterial>>();

            dbSet.As<IQueryable<LibroMaterial>>().Setup(x => x.Provider).Returns(dataPrueba.Provider);
            dbSet.As<IQueryable<LibroMaterial>>().Setup(x => x.Expression).Returns(dataPrueba.Expression);
            dbSet.As<IQueryable<LibroMaterial>>().Setup(x => x.ElementType).Returns(dataPrueba.ElementType);
            dbSet.As<IQueryable<LibroMaterial>>().Setup(x => x.GetEnumerator()).Returns(dataPrueba.GetEnumerator());

            dbSet.As<IAsyncEnumerable<LibroMaterial>>().Setup(x => x.GetAsyncEnumerator(new System.Threading.CancellationToken()))
                .Returns(new AsyncEnumerator<LibroMaterial>(dataPrueba.GetEnumerator()));

            dbSet.As<IQueryable<LibroMaterial>>().Setup(x => x.Provider).Returns(new AsyncQueryProvider<LibroMaterial>(dataPrueba.Provider));

            var contexto = new Mock<ContextoLibreria>();
            contexto.Setup(x => x.LibroMaterial).Returns(dbSet.Object);

            return contexto;
        }


        [Fact]
        public async void GetLibroPorId() 
        {
            var mockContexto = CrearContexto();
            var mapCopnfig = new MapperConfiguration(cfg => {
                cfg.AddProfile(new MappingTest());
            });
            var mapper = mapCopnfig.CreateMapper();
            var request = new ConsultaLibro.LibroUnico();
            request.LibroId = Guid.Empty;

            var manejador = new ConsultaLibro.Manejador(mockContexto.Object, mapper);
            var libro = await manejador.Handle(request, new System.Threading.CancellationToken());

            Assert.NotNull(libro);
            Assert.True(libro.LibroMaterialId == Guid.Empty);

        }


        [Fact]
        public async void GetLibros() 
        {            
            var mockContexto = CrearContexto();
            var mapConfig = new MapperConfiguration(cfg => 
            {
                cfg.AddProfile(new MappingTest());
            });
            var mapper = mapConfig.CreateMapper();

            Consulta.Manejador manejador = new Consulta.Manejador(mockContexto.Object, mapper);
            Consulta.Ejecuta request = new Consulta.Ejecuta();

            var lista = await manejador.Handle(request, new System.Threading.CancellationToken());

            Assert.True(lista.Any());
            
        }


        [Fact]
        public async void GuardarLibro() 
        {
            var options = new DbContextOptionsBuilder<ContextoLibreria>()
                .UseInMemoryDatabase(databaseName: "BaseDatosLibro")
                .Options;

            var contexto = new ContextoLibreria(options);

            var request = new Nuevo.Ejecuta {
                Titulo = "Libro de MicroService",
                AutorLibro = Guid.Empty,
                FechaPublicacion = DateTime.Now           
            };

            var manejador = new Nuevo.Manejador(contexto);
            var resultado = await manejador.Handle(request, new System.Threading.CancellationToken());

            Assert.True(resultado != null);

        }
    }
}
