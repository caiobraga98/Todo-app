using Microsoft.AspNetCore.Mvc;
using Moq;
using ToDo_app.Model;
using ToDo_app.Repository;

namespace Todo_app.Tests
{
    public class TarefasControllerTest
    {
        [Fact] // Atributo que identifica o método como um teste
        public async Task GetById_IdNaoExiste_RetornaNotFound()
        {
            // Arrange (Preparação)
            int idTarefaInexistente = 99;

            // 1. Criar o Mock da dependência IRepository<Tarefa>
            var mockRepository = new Mock<IRepository<Tarefa>>();

            // 2. 🎯 Configurar o Mock para retornar NULL quando GetByIdAsync(99) for chamado
            mockRepository.Setup(repo => repo.GetByIdAsync(idTarefaInexistente))
                          .ReturnsAsync((Tarefa)null); // 🎯 Moq: Retorna null

            // 3. Criar a instância real do Controller, injetando o mock
            var controller = new TarefasController(mockRepository.Object);

            // Act (Ação)
            var resultado = await controller.GetById(idTarefaInexistente);

            // Assert (Verificação)
            // Verificar se o resultado é do tipo NotFound (Status 404)
            Assert.IsType<NotFoundObjectResult>(resultado);
        }
        [Fact]
        public async Task GetById_IdFound()
        {
            //arrange
            int id = 1;
            var mockRepository = new Mock<IRepository<Tarefa>>();
            Tarefa tarefa = new Tarefa() { Titulo = "teste", Concluida = false, DataConclusao = new DateTime(), DataCriacao = new DateTime(), Id = 1, Descricao = "teste" };
            mockRepository.Setup(repo => repo.GetByIdAsync(id))
                          .ReturnsAsync(tarefa);
            var controller = new TarefasController(mockRepository.Object);
            //act
            var resultado = await controller.GetById(id);
            //assert
            var okResult = Assert.IsType<OkObjectResult>(resultado);
        }
    }

}
