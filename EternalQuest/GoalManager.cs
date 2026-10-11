
public class GoalManager
{
    private List<Goal> _goals;
    private int _score;

    public GoalManager()
    {
        _goals = new List<Goal>();
        _score = 0;
    }

    public void AddGoal(Goal goal)
    {
        _goals.Add(goal);
    }

    public void RecordEvent(int goalIndex)
    {
        if (goalIndex < 0 || goalIndex >= _goals.Count)
        {
            Console.WriteLine("Invalid goal index.");
            return;
        }

        _goals[goalIndex].RecordEvent(ref _score);
    }

    public void DisplayGoals()
    {
        for (int i = 0; i < _goals.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {_goals[i].GetDetailsString()}");
        }
    }

    public void DisplayScore()
    {
        Console.WriteLine($"Current Score: {_score}");
    }

    public void SaveGoals(string filePath)
    {
        using (StreamWriter writer = new StreamWriter(filePath))
        {
            writer.WriteLine(_score);
            foreach (var goal in _goals)
            {
                writer.WriteLine(goal.GetSaveString());
            }
        }
    }

    private static Goal CreateGoalFromString(string line)
    {
        if (string.IsNullOrWhiteSpace(line))
        {
            throw new FormatException("Goal data is empty.");
        }

        string[] parts = line.Split(new[] { ':', '|' }, StringSplitOptions.None);

        if (parts.Length < 4)
        {
            throw new FormatException("Invalid goal format.");
        }

        string typeName = parts[0].Trim();
        string name = parts[1].Trim();
        string description = parts[2].Trim();
        int points = int.Parse(parts[3].Trim());

        Type goalType = typeof(Goal).Assembly
            .GetTypes()
            .FirstOrDefault(t => t.IsClass && !t.IsAbstract && t.Name.Equals(typeName, StringComparison.OrdinalIgnoreCase));

        if (goalType == null)
        {
            throw new FormatException($"Unknown goal type: {typeName}");
        }

        object[] args;

        if (typeName.Equals("ChecklistGoal", StringComparison.OrdinalIgnoreCase))
        {
            int target = parts.Length > 4 ? int.Parse(parts[4].Trim()) : 0;
            int amountCompleted = parts.Length > 5 ? int.Parse(parts[5].Trim()) : 0;
            args = new object[] { name, description, points, target, amountCompleted };
        }
        else if (parts.Length > 4 && bool.TryParse(parts[4].Trim(), out bool isComplete))
        {
            args = new object[] { name, description, points, isComplete };
        }
        else
        {
            args = new object[] { name, description, points };
        }

        System.Reflection.ConstructorInfo constructor = goalType
            .GetConstructors()
            .FirstOrDefault(c => c.GetParameters().Length == args.Length);

        if (constructor == null)
        {
            throw new FormatException($"No compatible constructor found for {typeName}.");
        }

        return (Goal)constructor.Invoke(args);
    }

    public void LoadGoals(string filePath)
    {
        if (!File.Exists(filePath))
        {
            Console.WriteLine("Save file not found.");
            return;
        }

        using (StreamReader reader = new StreamReader(filePath))
        {
            if (int.TryParse(reader.ReadLine(), out int score))
            {
                _score = score;
            }
            else
            {
                Console.WriteLine("Invalid score in save file.");
                return;
            }

            _goals.Clear();
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                try
                {
                    Goal goal = CreateGoalFromString(line);
                    _goals.Add(goal);
                }
                catch (FormatException ex)
                {
                    Console.WriteLine($"Error loading goal: {ex.Message}");
                }
            }
        }
    }
}