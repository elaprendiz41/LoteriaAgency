using Core.Domain.Enums;

namespace Core.Domain.Entities;

/// <summary>Single wager line inside a ticket.</summary>
public class Play
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public string Number { get; private set; } = string.Empty;
    public decimal Amount { get; private set; }
    public GameType GameType { get; private set; }

    private Play()
    {
    }

    public Play(string number, decimal amount, GameType gameType)
    {
        if (string.IsNullOrWhiteSpace(number))
            throw new ArgumentException("Play number is required.", nameof(number));
        if (amount <= 0)
            throw new InvalidOperationException("Play amount must be greater than zero.");

        Number = number.Trim();
        Amount = amount;
        GameType = gameType;
    }
}
