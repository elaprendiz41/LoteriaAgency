using Core.Application.Abstractions;
using Core.Domain.Entities;

namespace Infrastructure.Persistence;

/// <summary>In-memory draw store (Hito 1 stub — EF Core arrives in H2/H3).</summary>
public sealed class InMemoryDrawRepository : IDrawRepository
{
    private readonly List<Draw> _draws = [];

    public Task<Draw?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var draw = _draws.FirstOrDefault(d => d.Id == id);
        return Task.FromResult(draw);
    }

    public Task<IReadOnlyList<Draw>> ListAsync(CancellationToken cancellationToken = default)
        => Task.FromResult<IReadOnlyList<Draw>>(_draws.ToList());

    public Task AddAsync(Draw draw, CancellationToken cancellationToken = default)
    {
        _draws.Add(draw);
        return Task.CompletedTask;
    }
}
