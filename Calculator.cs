using System;
using System.Collections.Generic;

class Calculator
{
    static void Main()
    {
        List<string> history = new List<string>();

        while (true)
        {
            Console.Clear();
            Console.WriteLine("=== Basic Calculator ===");
            Console.WriteLine("1. Addition");
            Console.WriteLine("2. Subtraction");
            Console.WriteLine("3. Multiplication");
            Console.WriteLine("4. Division");
            Console.WriteLine("5. Square Root");
            Console.WriteLine("6. Memory Functions");
            Console.WriteLine("7. View History");
            Console.WriteLine("8. Clear History");
            Console.WriteLine("9. Exit");
            Console.Write("Enter your choice (1/2/3/4/5/6/7/8/9): ");

            if (!int.TryParse(Console.ReadLine(), out int choice))
            {
                Console.WriteLine("Invalid input. Please enter a valid choice.");
                Console.ReadKey();
                continue;
            }

            if (choice == 9)
            {
                Console.WriteLine("Exiting the calculator.");
                break;
            }

            double result = 0;

            try
            {
                switch (choice)
                {
                    case 1:
                        result = PerformOperation("Addition", (a, b) => a + b);
                        break;
                    case 2:
                        result = PerformOperation("Subtraction", (a, b) => a - b);
                        break;
                    case 3:
                        result = PerformOperation("Multiplication", (a, b) => a * b);
                        break;
                    case 4:
                        result = PerformOperation("Division", (a, b) =>
                        {
                            if (b == 0)
                                throw new DivideByZeroException("Division by zero is not allowed.");
                            return a / b;
                        });
                        break;
                    case 5:
                        result = PerformOperation("Square Root", a => Math.Sqrt(a));
                        break;
                    case 6:
                        result = MemoryFunction(history);
                        break;
                    case 7:
                        ViewHistory(history);
                        break;
                    case 8:
                        ClearHistory(history);
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please enter a valid choice.");
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }

            Console.WriteLine($"Result: {result}");
            history.Add($"Choice: {choice}, Result: {result}");
            Console.ReadKey();
        }
    }

    static double PerformOperation(string operationName, Func<double, double, double> operation)
    {
        Console.Write($"Enter the first number for {operationName}: ");
        if (!double.TryParse(Console.ReadLine(), out double num1))
        {
            throw new ArgumentException("Invalid input. Please enter a valid number.");
        }

        Console.Write($"Enter the second number for {operationName}: ");
        if (!double.TryParse(Console.ReadLine(), out double num2))
        {
            throw new ArgumentException("Invalid input. Please enter a valid number.");
        }

        return operation(num1, num2);
    }

    static double PerformOperation(string operationName, Func<double, double> operation)
    {
        Console.Write($"Enter a number for {operationName}: ");
        if (!double.TryParse(Console.ReadLine(), out double num))
        {
            throw new ArgumentException("Invalid input. Please enter a valid number.");
        }

        return operation(num);
    }

    static double MemoryFunction(List<string> history)
    {
        Console.WriteLine("Memory Functions:");
        Console.WriteLine("1. Memory Store");
        Console.WriteLine("2. Memory Recall");
        Console.WriteLine("3. Memory Clear");
        Console.Write("Enter your choice (1/2/3): ");

        if (!int.TryParse(Console.ReadLine(), out int choice))
        {
            throw new ArgumentException("Invalid input. Please enter a valid choice.");
        }

        switch (choice)
        {
            case 1:
                Console.Write("Enter a value to store in memory: ");
                if (!double.TryParse(Console.ReadLine(), out double value))
                {
                    throw new ArgumentException("Invalid input. Please enter a valid number.");
                }
                history.Add($"Memory Stored: {value}");
                return value;

            case 2:
                if (history.Count == 0)
                {
                    Console.WriteLine("Memory is empty.");
                    return 0;
                }
                Console.WriteLine("Memory Recall:");
                for (int i = 0; i < history.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {history[i]}");
                }
                Console.Write("Enter the number of the memory item to recall: ");
                if (!int.TryParse(Console.ReadLine(), out int recallIndex) || recallIndex < 1 || recallIndex > history.Count)
                {
                    throw new ArgumentException("Invalid input. Please enter a valid memory item number.");
                }
                var memoryItem = history[recallIndex - 1];
                Console.WriteLine($"Recalled: {memoryItem}");
                return double.Parse(memoryItem.Split(':')[1].Trim());

            case 3:
                history.RemoveAll(item => item.StartsWith("Memory Stored"));
                Console.WriteLine("Memory Cleared.");
                return 0;

            default:
                throw new ArgumentException("Invalid choice. Please enter a valid choice.");
        }
    }

    static void ViewHistory(List<string> history)
    {
        Console.WriteLine("Calculation History:");
        if (history.Count == 0)
        {
            Console.WriteLine("No history available.");
        }
        else
        {
            foreach (var item in history)
            {
                Console.WriteLine(item);
            }
        }
    }

    static void ClearHistory(List<string> history)
    {
        history.Clear();
        Console.WriteLine("History Cleared.");
    }
}
