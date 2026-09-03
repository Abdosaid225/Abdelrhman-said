namespace FuzzyAdventure.Core;

/// <summary>
/// Represents a parsed command from the player.
/// </summary>
public class Command
{
    public string Verb { get; set; }
    public string? Object { get; set; }
    public int ConfidenceScore { get; set; }

    public Command(string verb, string? obj = null, int confidenceScore = 100)
    {
        Verb = verb ?? throw new ArgumentNullException(nameof(verb));
        Object = obj;
        ConfidenceScore = confidenceScore;
    }

    public override string ToString()
    {
        return Object != null ? $"{Verb} {Object}" : Verb;
    }
}
