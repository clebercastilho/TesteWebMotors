using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebMotors.Test.Domain.Entities;
using WebMotors.Test.Domain.Interfaces.Repositories;
using WebMotors.Test.Domain.Services;
using WebMotors.Teste.Domain.DataObjects;
using Xunit;

namespace WebMotors.Teste.Test
{
    public sealed class AnuncioServiceTest : Base.BaseTest
    {
        private readonly Mock<IAnuncioRepository> _anuncioRepository;
        private readonly AnuncioService _anuncioService;

        public AnuncioServiceTest() : base()
        {
            _anuncioRepository = new Mock<IAnuncioRepository>();
            SetupAnuncioRepository();
            SetupUnitOfWork();

            _anuncioService = new AnuncioService(handler, _uow.Object, _anuncioRepository.Object);
        }

        [Fact]
        public void Deve_Retornar_Lista_Anuncios()
        {
            var anuncios = _anuncioService.ListarAnuncios(new AnuncioFiltro());
            Assert.NotEmpty(anuncios);
        }

        [Fact]
        public void Deve_Retornar_Lista_Marca_Especifica()
        {
            var anuncios = _anuncioService.ListarAnuncios(new AnuncioFiltro
            {
                Marca = "Hyundai"
            });

            Assert.NotEmpty(anuncios);
            Assert.True(anuncios.Count == 2);
        }

        [Fact]
        public void Nao_Deve_Retornar_Nenhuma_Marca()
        {
            var anuncios = _anuncioService.ListarAnuncios(new AnuncioFiltro
            {
                Marca = "Ford"
            });

            Assert.Empty(anuncios);
        }

        private void SetupAnuncioRepository()
        {
            _anuncioRepository.Setup(r => r.Listar()).Returns(ModelListing().AsQueryable());
            _anuncioRepository.Setup(r => r.Incluir(It.IsAny<Anuncio>()));
            _anuncioRepository.Setup(r => r.Alterar(It.IsAny<Anuncio>()));
            _anuncioRepository.Setup(r => r.Excluir(It.IsAny<int>()));

            _anuncioRepository.Setup(r => r.Buscar(It.IsAny<int>())).Returns((int x) => {
                return ModelListing().FirstOrDefault(m => m.Id == x);
            });

            _anuncioRepository.Setup(r => r.ListarAnuncios(It.IsAny<AnuncioFiltro>()))
                .Returns((AnuncioFiltro filtro) => {
                    return ModelFiltered(filtro).AsQueryable();
                });

            _anuncioRepository.Setup(r => r.ListarAnunciosAsync(It.IsAny<AnuncioFiltro>()))
                .ReturnsAsync((AnuncioFiltro filtro) => {
                    return ModelFiltered(filtro);
                });
        }

        private void SetupUnitOfWork()
        {
            _uow.Setup(u => u.Anuncio).Returns(_anuncioRepository.Object);
            _uow.Setup(u => u.Commit());
        }

        private List<Anuncio> ModelListing()
        {
            return new List<Anuncio>
            {
                new Anuncio
                {
                    Id = 1,
                    Marca = "Chevrolet",
                    Modelo = "Onix",
                    Versao = "1.0 Flex 16v manual",
                    Ano = 2015,
                    Quilometragem = 20000
                },
                new Anuncio
                {
                    Id = 2,
                    Marca = "Chevrolet",
                    Modelo = "Prisma",
                    Versao = "1.0 Flex 16v manual",
                    Ano = 2016,
                    Quilometragem = 25300
                },
                new Anuncio
                {
                    Id = 3,
                    Marca = "Chevrolet",
                    Modelo = "Zafira",
                    Versao = "2.0 Gasolina 8v manual",
                    Ano = 2015,
                    Quilometragem = 20000
                },
                new Anuncio
                {
                    Id = 4,
                    Marca = "Fiat",
                    Modelo = "Palio",
                    Versao = "1.0 Flex ELX 8v manual",
                    Ano = 2000,
                    Quilometragem = 220000
                },
                new Anuncio
                {
                    Id = 5,
                    Marca = "Fiat",
                    Modelo = "500",
                    Versao = "1.4 Turbo 16v manual",
                    Ano = 2019,
                    Quilometragem = 5000
                },
                new Anuncio
                {
                    Id = 6,
                    Marca = "Honda",
                    Modelo = "Fit",
                    Versao = "1.5 HX Flex 16v automatica",
                    Ano = 2014,
                    Quilometragem = 80500
                },
                new Anuncio
                {
                    Id = 7,
                    Marca = "Honda",
                    Modelo = "HR-V",
                    Versao = "2.0 Flex 16v automatica",
                    Ano = 2016,
                    Quilometragem = 75000
                },
                new Anuncio
                {
                    Id = 8,
                    Marca = "Volkswagen",
                    Modelo = "Gol",
                    Versao = "1.0 Flex 8v manual",
                    Ano = 1999,
                    Quilometragem = 170530
                },
                new Anuncio
                {
                    Id = 9,
                    Marca = "Hyundai",
                    Modelo = "HB20",
                    Versao = "1.3 Flex 16v manual",
                    Ano = 2016,
                    Quilometragem = 35200
                },
                new Anuncio
                {
                    Id = 10,
                    Marca = "Hyundai",
                    Modelo = "HB20 S",
                    Versao = "1.6 turbo Flex 8v manual",
                    Ano = 2019,
                    Quilometragem = 1000
                }
            };
        }

        private List<Anuncio> ModelFiltered(AnuncioFiltro filtro)
        {
            return (from m in ModelListing()
                   where
                    (string.IsNullOrEmpty(filtro.Marca) || m.Marca == filtro.Marca) &&
                    (string.IsNullOrEmpty(filtro.Modelo) || m.Modelo == filtro.Modelo) &&
                    (filtro.AnoDesde <= 0 || m.Ano >= filtro.AnoDesde) &&
                    (filtro.AnoAte <= 0 || m.Ano <= filtro.AnoAte)
                   select m).ToList();
        }
    }
}
