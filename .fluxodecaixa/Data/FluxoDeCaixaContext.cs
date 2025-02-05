using Microsoft.EntityFrameworkCore;

public class FluxoDeCaixaContext : DbContext
{
    public FluxoDeCaixaContext(DbContextOptions<FluxoDeCaixaContext> options)
        : base(options)
    {
    }

    public DbSet<Lancamento> Lancamentos { get; set; }
}