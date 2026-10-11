
public class ChecklistGoal : Goal
{
    private int _amountCompleted;
    private int _target;
    private int _bonus;

    public ChecklistGoal(
        string name,
        string description,
        int points,
        int target,
        int bonus,
        int amountCompleted = 0)
        : base(name, description, points)
    {
        if (target <= 0)
            throw new ArgumentOutOfRangeException(nameof(target));

        if (points < 0 || bonus < 0)
            throw new ArgumentOutOfRangeException(
                "Points and bonus cannot be negative.");

        _target = target;
        _bonus = bonus;
        _amountCompleted = Math.Clamp(amountCompleted, 0, target);
    }

    public override void RecordEvent(ref int score)
    {
        if (_amountCompleted >= _target)
        {
            Console.WriteLine("This checklist goal is already complete.");
            return;
        }

        _amountCompleted++;
        score += _points;

        Console.WriteLine($"You earned {_points} points.");

        if (_amountCompleted == _target)
        {
            score += _bonus;
            Console.WriteLine(
                $"Goal completed! Bonus: {_bonus} points.");
        }
    }

    public override string GetDetailsString()
    {
        string status = _amountCompleted >= _target ? "X" : " ";

        return $"[{status}] {_name} - {_description} " +
               $"({_amountCompleted}/{_target} completed; " +
               $"{_points} points each; {_bonus} bonus points)";
    }

    public override string GetSaveString()
    {
        return $"Checklist|{Clean(_name)}|{Clean(_description)}|" +
               $"{_points}|{_target}|{_bonus}|{_amountCompleted}";
    }
}