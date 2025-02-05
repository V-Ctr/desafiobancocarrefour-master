using System;
using System.Linq;
using FluxoDeCaixa.Repositories;

namespace FluxoDeCaixa.Services
{
    public class FluxoDeCaixaService : IFluxoDeCaixaService
    {
        private readonly ILancamentoRepository _lancamentoRepository;

    public FluxoDeCaixaService()
    {
    }

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

    public string ObterLancamentos()
    {
      throw new NotImplementedException();
    }
  }
}