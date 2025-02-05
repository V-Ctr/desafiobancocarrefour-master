using System;
using System.Collections.Generic;

public interface IFluxoDeCaixaService
{
    IEnumerable<Lancamento> ObterLancamentos();
    void AdicionarLancamento(DateTime data, decimal valor, string tipo);
}

namespace FluxoDeCaixa.Services
{
    public interface IFluxoDeCaixaService
    {
        void AdicionarLancamento(DateTime data, decimal valor, string tipo);
        void GerarRelatorioDiario(DateTime data);
    string ObterLancamentos();
  }
}