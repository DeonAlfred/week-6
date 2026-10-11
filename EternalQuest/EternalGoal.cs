
using System;

public class EternalGoal : Goal
{
    public EternalGoal(string name, string description, int points)
        : base(name, description, points)
    {
    }

    public override void RecordEvent(ref int score)
    {
        score += _points;
        Console.WriteLine($"You earned {_points} points for your eternal quest!");
    }

    public override string GetDetailsString()
    {
        return $"[∞] {_name} - {_description} ({_points} points per event)";
    }

    public override string GetSaveString()
    {
        return $"Eternal|{Clean(_name)}|{Clean(_description)}|{_points}";
    }
}