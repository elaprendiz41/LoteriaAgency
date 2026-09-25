using Core.Domain.Entities;

namespace Core.Application.Abstractions;

public interface IDrawRepository
{
    Task<Draw?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Draw>> ListAsync(CancellationToken cancellationToken = default);
    Task AddAsync(Draw draw, CancellationToken cancellationToken = default);
}
