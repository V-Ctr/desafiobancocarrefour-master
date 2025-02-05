using System;
using System.Collections.Generic;
using System.Linq;

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