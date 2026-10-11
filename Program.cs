
using System;
using System.Globalization;

public class Program
{
    static void Main(string[] args)
    {
        bool running = true;

        while (running)
        {
            Console.WriteLine("\n===== ETERNAL QUEST =====");
            GoalManager manager = new GoalManager();
            Console.WriteLine($"Score: {manager.GetScore()}");
            Console.WriteLine($"Rank: {manager.GetRank()}");
            Console.WriteLine("\nMenu:");
            Console.WriteLine("1. Create New Goal");
            Console.WriteLine("2. List Goals");
            Console.WriteLine("3. Record Goal Event");
            Console.WriteLine("4. Save Goals");
            Console.WriteLine("5. Load Goals");
            Console.WriteLine("6. Quit");
            Console.Write("Select an option: ");

            string choice = Console.ReadLine() ?? "";

            switch (choice)
            {
                case "1":
                    CreateGoal(manager);
                    break;

                case "2":
                    manager.DisplayGoals();
                    break;

                case "3":
                    manager.DisplayGoals();
                    Console.Write("Enter goal number: ");

                    if (int.TryParse(Console.ReadLine(), out int number))
                    {
                        manager.RecordGoalEvent(number - 1);
                    }
                    else
                    {
                        Console.WriteLine("Please enter a valid number.");
                    }
                    break;

                case "4":
                    Console.Write("Enter filename: ");
                    string saveFile = Console.ReadLine() ?? "goals.txt";
                    manager.SaveGoals(saveFile);
                    break;

                case "5":
                    Console.Write("Enter filename: ");
                    string loadFile = Console.ReadLine() ?? "goals.txt";
                    manager.LoadGoals(loadFile);
                    break;

                case "6":
                    running = false;
                    Console.WriteLine("Thanks for playing Eternal Quest!");
                    break;

                default:
                    Console.WriteLine("Invalid option. Try again.");
                    break;
            }
        }
    }

    static void CreateGoal(GoalManager manager)
    {
        Console.WriteLine("\nGoal Types:");
        Console.WriteLine("1. Simple Goal");
        Console.WriteLine("2. Eternal Goal");
        Console.WriteLine("3. Checklist Goal");
        Console.Write("Choose a goal type: ");

        string type = Console.ReadLine() ?? "";

        Console.Write("Enter goal name: ");
        string name = Console.ReadLine() ?? "";

        Console.Write("Enter goal description: ");
        string description = Console.ReadLine() ?? "";

        Console.Write("Enter points for each event: ");
        if (!int.TryParse(Console.ReadLine(), out int points) || points <= 0)
        {
            Console.WriteLine("Points must be a positive number.");
            return;
        }

        switch (type)
        {
            case "1":
                manager.AddGoal(new SimpleGoal(name, description, points));
                break;

            case "2":
                manager.AddGoal(new EternalGoal(name, description, points));
                break;

            case "3":
                Console.Write("How many times must you complete it? ");

                if (!int.TryParse(Console.ReadLine(), out int target) || target <= 0)
                {
                    Console.WriteLine("Target must be a positive number.");
                    return;
                }

                Console.Write("Enter bonus points for completing it: ");

                if (!int.TryParse(Console.ReadLine(), out int bonus) || bonus < 0)
                {
                    Console.WriteLine("Bonus cannot be negative.");
                    return;
                }

                manager.AddGoal(
                    new ChecklistGoal(name, description, points, target, bonus));
                break;

            default:
                Console.WriteLine("Invalid goal type.");
                return;
        }

        Console.WriteLine("Goal created successfully!");
    }
}
