
public class SimpleGoal : Goal
{
    private bool _isComplete;

    public SimpleGoal(string name, string description, int points)
        : base(name, description, points)
    {
        _isComplete = false;
    }

    public override void RecordEvent(ref int score)
    {
        if (_isComplete)
        {
            Console.WriteLine("This goal is already complete.");
            return;
        }

        _isComplete = true;
        score += _points;
        Console.WriteLine($"You earned {_points} points.");
    }

    public override string GetDetailsString()
    {
        string status = _isComplete ? "X" : " ";
        return $"[{status}] {_name} - {_description} ({_points} points)";
    }

    public override string GetSaveString()
    {
        return $"Simple|{Clean(_name)}|{Clean(_description)}|{_points}|{_isComplete}";
    }
}