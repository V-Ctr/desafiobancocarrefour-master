# Fluxo de Caixa - Microservices Architecture🚀

## Visão Geral

Este projeto implementa um sistema de controle de fluxo de caixa diário usando uma arquitetura de microservices. Ele inclui lançamentos de débitos e créditos, e gera um relatório com o saldo diário consolidado.

## Arquitetura🏗️

A arquitetura de microservices é composta pelos seguintes componentes:

- **Frontend**: Interface do usuário.🖥️
- **API Gateway**: Ponto de entrada para todas as solicitações de API.🌐
- **Services**:
  - Authentication Service 🔐
  - User Service 👤
  - Product Service 📦
  - Order Service 🛒
  - Payment Service 💳
  - CashFlow Service 💰
- **Infrastructure**:
  - Message Broker 📬
  - Cache (Redis) 🗄️
  - Database (SQL/NoSQL) 🗃️

### Diagrama de Arquitetura📊 

#Utilizei a biblioteca do plantUML para versionar todos os desenhos de arquitetura.

```plantuml
@startuml MicroservicesArchitecture

package "Frontend" {
    [User Interface] << (C, #ADD1B2) >>
}

package "API Gateway" {
    [Authentication Service] << (S, #ADD1B2) >>
}

package "Services" {
    [Authentication Service] << (S, #ADD1B2) >>
    [User Service] << (S, #ADD1B2) >>
    [Product Service] << (S, #ADD1B2) >>
    [Order Service] << (S, #ADD1B2) >>
    [Payment Service] << (S, #ADD1B2) >>
    [CashFlow Service] << (S, #ADD1B2) >>
}

package "Infrastructure" {
    [Message Broker] << (I, #ADD1B2) >>
    [Cache (Redis)] << (I, #ADD1B2) >>
    [Database (SQL/NoSQL)] << (I, #ADD1B2) >>
}

[User Interface] --> [API Gateway] : HTTP/HTTPS (JSON)
[API Gateway] --> [Authentication Service] : HTTP/HTTPS (JSON)
[API Gateway] --> [User Service] : HTTP/HTTPS (JSON)
[API Gateway] --> [Product Service] : HTTP/HTTPS (JSON)
[API Gateway] --> [Order Service] : HTTP/HTTPS (JSON)
[API Gateway] --> [Payment Service] : HTTP/HTTPS (JSON)
[API Gateway] --> [CashFlow Service] : HTTP/HTTPS (JSON)

[Authentication Service] --> [User Service] : gRPC (Protobuf)
[User Service] --> [Database (SQL/NoSQL)] : SQL/NoSQL
[Product Service] --> [Database (SQL/NoSQL)] : SQL/NoSQL
[Order Service] --> [Database (SQL/NoSQL)] : SQL/NoSQL
[Payment Service] --> [Database (SQL/NoSQL)] : SQL/NoSQL
[CashFlow Service] --> [Database (SQL/NoSQL)] : SQL/NoSQL

[Product Service] --> [Message Broker] : AMQP (Protobuf)
[Order Service] --> [Message Broker] : AMQP (Protobuf)
[Payment Service] --> [Message Broker] : AMQP (Protobuf)
[CashFlow Service] --> [Message Broker] : AMQP (Protobuf)

[Product Service] --> [Cache (Redis)] : Redis Protocol
[Order Service] --> [Cache (Redis)] : Redis Protocol
[Payment Service] --> [Cache (Redis)] : Redis Protocol
[CashFlow Service] --> [Cache (Redis)] : Redis Protocol

@enduml


#Configuração do Projeto

1. Criar o Projeto
Crie projeto console em .NET 8:

2. Estrutura do Código
Modelo de arquivos

// filepath: /FluxoDeCaixa/Models/Lancamento.cs
using System;

namespace FluxoDeCaixa.Models
{
    public class Lancamento
    {
        public int Id { get; set; }
        public DateTime Data { get; set; }
        public decimal Valor { get; set; }
        public string Tipo { get; set; } // "Debito" ou "Credito"
    }
}

Interface de Repositório
// filepath: /FluxoDeCaixa/Repositories/ILancamentoRepository.cs
using System;
using System.Collections.Generic;
using FluxoDeCaixa.Models;

namespace FluxoDeCaixa.Repositories
{
    public interface ILancamentoRepository
    {
        void Adicionar(Lancamento lancamento);
        IEnumerable<Lancamento> ObterPorData(DateTime data);
    }
}

Implementação do Repositório
// filepath: /FluxoDeCaixa/Repositories/LancamentoRepository.cs
using System;
using System.Collections.Generic;
using System.Linq;
using FluxoDeCaixa.Data;
using FluxoDeCaixa.Models;

namespace FluxoDeCaixa.Repositories
{
    public class LancamentoRepository : ILancamentoRepository
    {
        private readonly FluxoDeCaixaContext _context;

        public LancamentoRepository(FluxoDeCaixaContext context)
        {
            _context = context;
        }

        public void Adicionar(Lancamento lancamento)
        {
            _context.Lancamentos.Add(lancamento);
            _context.SaveChanges();
        }

        public IEnumerable<Lancamento> ObterPorData(DateTime data)
        {
            return _context.Lancamentos
                .Where(l => l.Data.Date == data.Date)
                .ToList();
        }
    }
}

Serviço de Fluxo de Caixa
// filepath: /FluxoDeCaixa/Services/IFluxoDeCaixaService.cs
using System;

namespace FluxoDeCaixa.Services
{
    public interface IFluxoDeCaixaService
    {
        void AdicionarLancamento(DateTime data, decimal valor, string tipo);
        void GerarRelatorioDiario(DateTime data);
    }
}

// filepath: /FluxoDeCaixa/Services/FluxoDeCaixaService.cs
using System;
using System.Linq;
using FluxoDeCaixa.Models;
using FluxoDeCaixa.Repositories;

namespace FluxoDeCaixa.Services
{
    public class FluxoDeCaixaService : IFluxoDeCaixaService
    {
        private readonly ILancamentoRepository _lancamentoRepository;

        public FluxoDeCaixaService(ILancamentoRepository lancamentoRepository)
        {
            _lancamentoRepository = lancamentoRepository;
        }

        public void AdicionarLancamento(DateTime data, decimal valor, string tipo)
        {
            var lancamento = new Lancamento
            {
                Data = data,
                Valor = valor,
                Tipo = tipo
            };

            _lancamentoRepository.Adicionar(lancamento);
        }

        public void GerarRelatorioDiario(DateTime data)
        {
            var lancamentos = _lancamentoRepository.ObterPorData(data);

            var saldo = lancamentos
                .Sum(l => l.Tipo == "Credito" ? l.Valor : -l.Valor);

            Console.WriteLine($"Relatório do dia {data:dd/MM/yyyy}");
            Console.WriteLine("----------------------------");
            foreach (var lancamento in lancamentos)
            {
                Console.WriteLine($"{lancamento.Data:HH:mm:ss} - {lancamento.Tipo} - R$ {lancamento.Valor:F2}");
            }
            Console.WriteLine("----------------------------");
            Console.WriteLine($"Saldo do dia: R$ {saldo:F2}");
        }
    }
}

Contexto do Banco de Dados
// filepath: /FluxoDeCaixa/Data/FluxoDeCaixaContext.cs
using Microsoft.EntityFrameworkCore;
using FluxoDeCaixa.Models;

namespace FluxoDeCaixa.Data
{
    public class FluxoDeCaixaContext : DbContext
    {
        public DbSet<Lancamento> Lancamentos { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("YourConnectionStringHere");
        }
    }
}


Programa Principal
// filepath: /FluxoDeCaixa/Program.cs
using System;
using Microsoft.Extensions.DependencyInjection;
using FluxoDeCaixa.Data;
using FluxoDeCaixa.Repositories;
using FluxoDeCaixa.Services;

class Program
{
    static void Main(string[] args)
    {
        var serviceProvider = new ServiceCollection()
            .AddDbContext<FluxoDeCaixaContext>()
            .AddScoped<ILancamentoRepository, LancamentoRepository>()
            .AddScoped<IFluxoDeCaixaService, FluxoDeCaixaService>()
            .BuildServiceProvider();

        using var context = serviceProvider.GetService<FluxoDeCaixaContext>();
        context.Database.EnsureCreated();

        var fluxoDeCaixaService = serviceProvider.GetService<IFluxoDeCaixaService>();

        // Adicionar lançamentos
        fluxoDeCaixaService.AdicionarLancamento(DateTime.Now, 100.00m, "Credito");
        fluxoDeCaixaService.AdicionarLancamento(DateTime.Now, 50.00m, "Debito");

        // Gerar relatório diário
        fluxoDeCaixaService.GerarRelatorioDiario(DateTime.Now);
    }
}

#Executar o Projeto

Execute o projeto para ver o controle de fluxo de caixa em ação:

dotnet run

#Testes End-to-End (E2E)

Para garantir que o sistema funcione corretamente de ponta a ponta, incluímos testes E2E usando Selenium.

#Configuração dos Testes E2E

1. Adicionar dependências: Certifique-se de que você tem o pacote Selenium WebDriver instalado:

dotnet add package Selenium.WebDriver

2. Criar a classe de testes E2E:

// filepath: /FluxoDeCaixa.Tests/E2E/FluxoDeCaixaE2ETests.cs
using Xunit;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;

public class FluxoDeCaixaE2ETests : IDisposable
{
    private readonly IWebDriver _driver;

    public FluxoDeCaixaE2ETests()
    {
        _driver = new ChromeDriver();
    }

    [Fact]
    public void AdicionarLancamento_DeveExibirLancamentoNaLista()
    {
        // Arrange
        _driver.Navigate().GoToUrl("http://localhost:5000");

        // Act
        _driver.FindElement(By.Id("data")).SendKeys("2023-01-01");
        _driver.FindElement(By.Id("valor")).SendKeys("100.00");
        _driver.FindElement(By.Id("tipo")).SendKeys("Credito");
        _driver.FindElement(By.Id("adicionar")).Click();

        // Assert
        var lancamento = _driver.FindElement(By.XPath("//table/tbody/tr[last()]/td[2]"));
        Assert.Equal("100.00", lancamento.Text);
    }

    public void Dispose()
    {
        _driver.Quit();
    }
}

3. Executar os testes E2E: Para executar os testes E2E, use o comando:

dotnet test

### Explicação:
- **Seção de Testes E2E**: Adicionamos uma seção específica para testes E2E, explicando como configurar e executar esses testes.
- **Blocos de Código**: Usamos blocos de código para mostrar exemplos de código e comandos de terminal.
- **Comandos de Terminal**: Formatamos os comandos de terminal usando blocos de código com `sh` para indicar que são comandos de shell.

Isso deve fornecer uma documentação clara e completa sobre como configurar e executar testes E2E no seu projeto.
### Explicação:
- **Seção de Testes E2E**: Adicionamos uma seção específica para testes E2E, explicando como configurar e executar esses testes.
- **Blocos de Código**: Usamos blocos de código para mostrar exemplos de código e comandos de terminal.
- **Comandos de Terminal**: Formatamos os comandos de terminal usando blocos de código com `sh` para indicar que são comandos de shell.


#Conclusão

Este projeto implementa um sistema de controle de fluxo de caixa diário usando uma arquitetura de microservices. Ele segue os princípios SOLID e usa padrões de design, como o padrão de repositório e o padrão de serviço. 

Este arquivo `README.md` fornece uma visão geral do projeto, a arquitetura de microservices, a configuração do projeto, a estrutura do código e as instruções para executar a aplicação. Certifique-se de ajustar a string de conexão do banco de dados conforme necessário.