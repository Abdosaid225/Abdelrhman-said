namespace FuzzyAdventure.Core;

using FuzzySharp;

/// <summary>
/// Parses player input into commands using fuzzy matching.
/// </summary>
public class Parser
{
    private List<string> _verbs = ["look", "go", "take", "drop", "inventory", "examine", "use", "help", "quit"];
    private const int FuzzyThreshold = 70;

    public Command Parse(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
            throw new ArgumentException("Input cannot be empty.", nameof(input));

        var parts = input.Trim().Split(' ', 2, StringSplitOptions.RemoveEmptyEntries);
        var verbInput = parts[0];
        var objectInput = parts.Length > 1 ? parts[1] : null;

        var matchedVerb = FuzzyMatchVerb(verbInput);
        var confidence = Fuzz.Ratio(verbInput, matchedVerb);

        return new Command(matchedVerb, objectInput, confidence);
    }

    private string FuzzyMatchVerb(string input)
    {
        var matches = _verbs
            .Select(verb => (verb, score: Fuzz.Ratio(input.ToLowerInvariant(), verb)))
            .OrderByDescending(x => x.score)
            .ToList();

        if (matches.Count > 0 && matches[0].score >= FuzzyThreshold)
            return matches[0].verb;

        return matches[0].verb;
    }

    public void RegisterVerb(string verb)
    {
        if (!string.IsNullOrWhiteSpace(verb) && !_verbs.Contains(verb.ToLowerInvariant()))
        {
            _verbs.Add(verb.ToLowerInvariant());
        }
    }
}
