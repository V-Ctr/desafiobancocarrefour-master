// filepath: /C:/Users/vladi/.dev/.desafiocarrefour/FluxoDeCaixa.Tests/Services/FluxoDeCaixaServiceTests.cs
using Xunit;
using Moq;
using FluxoDeCaixa.Services;
using FluxoDeCaixa.Repositories;
using System;

public class FluxoDeCaixaServiceTests
{
    private readonly FluxoDeCaixaService _service;
    private readonly Mock<ILancamentoRepository> _repositoryMock;

    public FluxoDeCaixaServiceTests()
    {
        _repositoryMock = new Mock<ILancamentoRepository>();
        _service = new FluxoDeCaixaService(_repositoryMock.Object);
    }

    [Fact]
    public void AdicionarLancamento_DeveAdicionarLancamento()
    {
        // Arrange
        var data = DateTime.Now;
        var valor = 100.00m;
        var tipo = "Credito";

        // Act
        _service.AdicionarLancamento(data, valor, tipo);

        // Assert
        _repositoryMock.Verify(r => r.Adicionar(It.IsAny<Lancamento>()), Times.Once);
    }
}