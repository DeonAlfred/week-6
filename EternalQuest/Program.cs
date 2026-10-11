using System;
public class Program
{
    public static void Main(string[] args)
    {
        GoalManager goalManager = new GoalManager();
        
        // Load goals from file if it exists
        Console.WriteLine("Welcome to The Goal Eternal Quest Program");
        Console.WriteLine("==========================================");
        DateTime currentLocal = DateTime.Now; 
        Console.WriteLine(currentLocal);
        while (true)
        {
            Console.WriteLine("=================Menu:====================");
            Console.WriteLine("1. Create a new goal");
            Console.WriteLine("2. Record an event for a goal");
            Console.WriteLine("3. Display all goals");
            Console.WriteLine("4. Display current score");
            Console.WriteLine("5. Save and exit");
            Console.Write("Choose an option: ");
            string choice = Console.ReadLine();
             string saveFilePath = "goals.txt";
             goalManager.LoadGoals(saveFilePath);

            switch (choice)
            {
                case "1":
                    CreateGoal(goalManager);
                    break;
                case "2":
                    RecordEvent(goalManager);
                    break;
                case "3":
                    goalManager.DisplayGoals();
                    break;
                case "4":
                    goalManager.DisplayScore();
                    break;
                case "5":
                    goalManager.SaveGoals(saveFilePath);
                    return;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }
        }
    }

    private static void CreateGoal(GoalManager goalManager)
    {
        Console.Write("Enter goal type (Simple/Eternal): ");
        string type = Console.ReadLine();
        Console.Write("Enter goal name: ");
        string name = Console.ReadLine();
        Console.Write("Enter goal description: ");
        string description = Console.ReadLine();
        Console.Write("Enter points for the goal: ");
        int points = int.Parse(Console.ReadLine());

        Goal newGoal;
        if (type.Equals("Simple", StringComparison.OrdinalIgnoreCase))
        {
            newGoal = new SimpleGoal(name, description, points);
        }
        else if (type.Equals("Eternal", StringComparison.OrdinalIgnoreCase))
        {
            newGoal = new EternalGoal(name, description, points);
        }
        else
        {
            Console.WriteLine("Invalid goal type.");
            return;
        }

        goalManager.AddGoal(newGoal);
    }

    private static void RecordEvent(GoalManager goalManager)
    {
        goalManager.DisplayGoals();
        Console.Write("Enter the number of the goal to record an event for: ");
        int index = int.Parse(Console.ReadLine()) - 1;
        goalManager.RecordEvent(index);
    }
}