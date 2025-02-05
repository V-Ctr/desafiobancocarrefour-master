using FluxoDeCaixa.Services;
using Microsoft.AspNetCore.Mvc;
using System;

public class HomeController : Controller
{
    private readonly IFluxoDeCaixaService _fluxoDeCaixaService;

    public HomeController(IFluxoDeCaixaService fluxoDeCaixaService)
    {
        _fluxoDeCaixaService = fluxoDeCaixaService;
    }

    public IActionResult Index()
    {
        var lancamentos = _fluxoDeCaixaService.ObterLancamentos();
        return View(lancamentos);
    }

    [HttpPost]
    public IActionResult AdicionarLancamento(DateTime data, decimal valor, string tipo)
    {
        _fluxoDeCaixaService.AdicionarLancamento(data, valor, tipo);
        return RedirectToAction("Index");
    }
}