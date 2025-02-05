// filepath: /C:/Users/vladi/.dev/.desafiocarrefour/FluxoDeCaixa.Tests/Integration/FluxoDeCaixaIntegrationTests.cs
using Xunit;
using Microsoft.EntityFrameworkCore;
using FluxoDeCaixa.Data;
using FluxoDeCaixa.Services;
using System;

public class FluxoDeCaixaIntegrationTests
{
    private readonly FluxoDeCaixaContext _context;
    private readonly FluxoDeCaixaService _service;

    public FluxoDeCaixaIntegrationTests()
    {
        var options = new DbContextOptionsBuilder<FluxoDeCaixaContext>()
            .UseInMemoryDatabase(databaseName: "FluxoDeCaixaTest")
            .Options;

        _context = new FluxoDeCaixaContext(options);
        _service = new FluxoDeCaixaService(new LancamentoRepository(_context));
    }

    [Fact]
    public void AdicionarLancamento_DeveSalvarNoBancoDeDados()
    {
        // Arrange
        var data = DateTime.Now;
        var valor = 100.00m;
        var tipo = "Credito";

        // Act
        _service.AdicionarLancamento(data, valor, tipo);

        // Assert
        var lancamento = _context.Lancamentos.FirstOrDefault();
        Assert.NotNull(lancamento);
        Assert.Equal(valor, lancamento.Valor);
        Assert.Equal(tipo, lancamento.Tipo);
    }
}