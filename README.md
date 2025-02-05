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

Utilizei a biblioteca do plantUML para versionar todos os desenhos de arquitetura.

MicroservicesArchitecture
![image](https://github.com/user-attachments/assets/e4a03ea2-f338-400f-81ad-aab85010bd55)


## Configuração do Projeto

1. Criar o Projeto em .NET 8:

2. Estrutura do Código
Modelo de arquivos

![image](https://github.com/user-attachments/assets/83b3aee1-5fe5-41bf-9019-1b4bf2ff68ed)



Contexto do Banco de Dados
// filepath: /FluxoDeCaixa/Data/FluxoDeCaixaContext.cs

Programa Principal
// filepath: /FluxoDeCaixa/Program.cs

## Executar o Projeto

Execute o projeto para ver o controle de fluxo de caixa em ação:

dotnet run

## Testes End-to-End (E2E)

Para garantir que o sistema funcione corretamente de ponta a ponta, incluí testes E2E usando Selenium.

Configuração dos Testes E2E

1. Adicionar dependências: Certifique-se de ter o pacote Selenium WebDriver instalado:

dotnet add package Selenium.WebDriver

2. Criar a classe de testes E2E:

// filepath: /FluxoDeCaixa.Tests/E2E/FluxoDeCaixaE2ETests.cs

3. Executar os testes E2E: Para executar os testes E2E, use o comando:

dotnet test

### Explicação:
- **Seção de Testes E2E**: Adicionei uma seção específica para testes E2E, explicando como configurar e executar esses testes.
- **Blocos de Código**: Blocos de código para mostrar exemplos de código e comandos de terminal.
- **Comandos de Terminal**: Os comandos de terminal usando blocos de código com `sh` para indicar que são comandos de shell.

## Conclusão

Este projeto implementa um sistema de controle de fluxo de caixa diário usando uma arquitetura de microservices. Ele segue os princípios SOLID e usa padrões de design, como o padrão de repositório e o padrão de serviço. 

Este arquivo `README.md` fornece uma visão geral do projeto, a arquitetura de microservices, a configuração do projeto, a estrutura do código e as instruções para executar a aplicação. Certifique-se de ajustar a string de conexão do banco de dados para executar o build local conforme necessário.
