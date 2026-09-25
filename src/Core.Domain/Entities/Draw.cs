using Core.Domain.Common;
using Core.Domain.Enums;

namespace Core.Domain.Entities;

/// <summary>
/// Lottery draw (sorteo). Sales must stop at <see cref="SalesCloseAt"/> (time-fencing).
/// </summary>
public class Draw : BaseEntity
{
    public string Code { get; private set; } = string.Empty;
    public string Name { get; private set; } = string.Empty;
    public DateTime DrawAt { get; private set; }
    public DateTime SalesCloseAt { get; private set; }
    public DrawStatus Status { get; private set; }

    private readonly List<Ticket> _tickets = [];
    public IReadOnlyCollection<Ticket> Tickets => _tickets.AsReadOnly();

    private Draw()
    {
    }

    public Draw(string code, string name, DateTime drawAtUtc, DateTime salesCloseAtUtc)
    {
        if (string.IsNullOrWhiteSpace(code))
            throw new ArgumentException("Draw code is required.", nameof(code));
        if (salesCloseAtUtc >= drawAtUtc)
            throw new InvalidOperationException("SalesCloseAt must be before DrawAt.");

        Code = code.Trim();
        Name = name.Trim();
        DrawAt = DateTime.SpecifyKind(drawAtUtc, DateTimeKind.Utc);
        SalesCloseAt = DateTime.SpecifyKind(salesCloseAtUtc, DateTimeKind.Utc);
        Status = DrawStatus.Scheduled;
    }

    public bool AreSalesOpen(DateTime utcNow) =>
        Status is DrawStatus.Scheduled or DrawStatus.Open
        && utcNow < SalesCloseAt;

    public void OpenSales() => Status = DrawStatus.Open;

    public void CloseSales() => Status = DrawStatus.SalesClosed;
}
