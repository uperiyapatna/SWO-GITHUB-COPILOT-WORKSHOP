using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

public class Trick
{
    public string Action { get; set; }
    public int RepetitionCount { get; set; }
    public char DifficultyModifier { get; set; }
    public double Score { get; set; }
}

public class BikeTrickSequence
{
    public List<Trick> Tricks { get; set; } = new List<Trick>();
    public double Difficulty { get; set; }

    public static BikeTrickSequence Parse(string sequence)
    {
        var bikeTrickSequence = new BikeTrickSequence();
        var tricks = Regex.Matches(sequence, @"[A-Z]\d+[A-E]");

        foreach (Match trick in tricks)
        {
            var action = trick.Value[0].ToString();
            var repetitionCount = int.Parse(trick.Value.Substring(1, trick.Value.Length - 2));
            var difficultyModifier = trick.Value[^1];

            var score = CalculateScore(action, repetitionCount, difficultyModifier, bikeTrickSequence);
            bikeTrickSequence.Tricks.Add(new Trick { Action = action, RepetitionCount = repetitionCount, DifficultyModifier = difficultyModifier, Score = score });
            bikeTrickSequence.Difficulty += score;
        }

        bikeTrickSequence.Difficulty = Math.Round(bikeTrickSequence.Difficulty, 2);
        return bikeTrickSequence;
    }

    private static double CalculateScore(string action, int repetitionCount, char difficultyModifier, BikeTrickSequence bikeTrickSequence)
    {
        var difficulty = difficultyModifier switch
        {
            'A' => 1.0,
            'B' => 1.2,
            'C' => 1.4,
            'D' => 1.6,
            'E' => 1.8,
            _ => throw new Exception("Invalid difficulty modifier")
        };

        var score = repetitionCount * difficulty;

        if (bikeTrickSequence.Tricks.Count > 0)
        {
            var lastAction = bikeTrickSequence.Tricks[^1].Action;
            if (lastAction == "L" && action == "R") score *= 2;
            if (lastAction == "T" && action == "S") score *= 3;
        }

        return score;
    }
}