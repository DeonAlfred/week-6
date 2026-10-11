
public abstract class Goal
{
    protected string _name;
    protected string _description;
    protected int _points;

    public Goal(string name, string description, int points)
    {
        _name = name;
        _description = description;
        _points = points;
    }

    public abstract void RecordEvent(ref int score);
    public abstract string GetDetailsString();
    public abstract string GetSaveString();

    protected string Clean(string input)
    {
        return input.Replace("|", " ");
    }
}