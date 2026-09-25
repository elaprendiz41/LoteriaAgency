using Core.Domain.Common;
using Core.Domain.Enums;

namespace Core.Domain.Entities;

/// <summary>
/// Sales ticket belonging to a <see cref="Draw"/>. Holds one or more <see cref="Play"/> lines.
/// </summary>
public class Ticket : BaseEntity
{
    public Guid DrawId { get; private set; }
    public string Serial { get; private set; } = string.Empty;
    public TicketStatus Status { get; private set; }
    public decimal TotalAmount { get; private set; }

    private readonly List<Play> _plays = [];
    public IReadOnlyCollection<Play> Plays => _plays.AsReadOnly();

    private Ticket()
    {
    }

    public Ticket(Guid drawId, string serial, IEnumerable<Play> plays)
    {
        if (drawId == Guid.Empty)
            throw new ArgumentException("DrawId is required.", nameof(drawId));
        if (string.IsNullOrWhiteSpace(serial))
            throw new ArgumentException("Ticket serial is required.", nameof(serial));

        var playList = plays?.ToList() ?? [];
        if (playList.Count == 0)
            throw new InvalidOperationException("A ticket must contain at least one play.");

        DrawId = drawId;
        Serial = serial.Trim();
        Status = TicketStatus.Confirmed;
        _plays.AddRange(playList);
        TotalAmount = _plays.Sum(p => p.Amount);
    }

    public void Cancel()
    {
        if (Status == TicketStatus.Paid)
            throw new InvalidOperationException("A paid ticket cannot be cancelled.");
        if (Status == TicketStatus.Cancelled)
            throw new InvalidOperationException("Ticket is already cancelled.");

        Status = TicketStatus.Cancelled;
    }

    public void MarkPaid()
    {
        if (Status != TicketStatus.Confirmed)
            throw new InvalidOperationException("Only confirmed tickets can be paid.");

        Status = TicketStatus.Paid;
    }
}
