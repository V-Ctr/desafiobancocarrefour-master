using System;
using System.Collections.Generic;

namespace FluxoDeCaixa.Repositories
{
    public interface ILancamentoRepository
    {
        void Adicionar(Lancamento lancamento);
        IEnumerable<Lancamento> ObterPorData(DateTime data);
    }
}