using JediApi.Models;
using JediApi.Repositories;
using JediApi.Services;
using Moq;
using Xunit;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JediApi.Tests.Services
{
    public class JediServiceTests
    {
        // não mexer
        private readonly JediService _service;
        private readonly Mock<IJediRepository> _repositoryMock;

        public JediServiceTests()
        {
            // não mexer
            _repositoryMock = new Mock<IJediRepository>();
            _service = new JediService(_repositoryMock.Object);
        }

        [Fact]
        public async Task GetById_Success()
        {
            var JediId = 1;
            var JediChamado = new Jedi { Id = JediId, Name = "gabrielQueiroz" };

            _repositoryMock.Setup(repo => repo.GetByIdAsync(JediId)).ReturnsAsync(JediChamado);

            var result = await _service.GetByIdAsync(JediId);

            Assert.Equal(JediChamado.Id, result.Id);
        }

        [Fact]
        public async Task GetById_NotFound()
        {
            var JediId = 1;
            _repositoryMock.Setup(repo => repo.GetByIdAsync(JediId)).ReturnsAsync((Jedi)null);

            var result = await _service.GetByIdAsync(JediId);

            Assert.Null(result);
        }

        [Fact]
        public async Task GetAll()
        {
            var listaJediChamado = new List<Jedi>
            {
                new Jedi { Id = 0,  Name = "gabrielQueiroz"},
                new Jedi { Id = 1, Name = "Luke Skywalker" }
            };

            _repositoryMock.Setup(repo => repo.GetAllAsync()).ReturnsAsync(listaJediChamado);

            var result = await _service.GetAllAsync();

            Assert.Equal(listaJediChamado[0].Id, result[0].Id);
            Assert.Equal(listaJediChamado[1].Id, result[1].Id);
        }
    }
}
