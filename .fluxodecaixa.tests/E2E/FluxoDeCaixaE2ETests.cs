// filepath: /C:/Users/vladi/.dev/.desafiocarrefour/FluxoDeCaixa.Tests/E2E/FluxoDeCaixaE2ETests.cs
using Xunit;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

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