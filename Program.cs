using System.Text.RegularExpressions;
using CalculatorLibrary;

namespace CalculatorProgram
{

    class Program
    {
        static void Main(string[] args)
        {
            bool endApp = false;
            // Display title as the C# console calculator app.
            Console.WriteLine("Console Calculator in C#\r");
            Console.WriteLine("------------------------\n");

            Calculator calculator = new Calculator();
            while (!endApp)
            {
                // Declare variables and set to empty.
                // Use Nullable types (with ?) to match type of System.Console.ReadLine
                string? numInput1 = "";
                string? numInput2 = "";
                double result = 0;

                if (calculator.ResultHistory.Count > 0)
                {
                    // Console.Write("Would you like to use a previously stored result? \n");
                    // Console.WriteLine("\ty - Yes");
                    // Console.WriteLine("\tn - No");
                    // string? decision = Console.ReadLine();
                    // Console.WriteLine();

                    Console.Write("Press 'p' to use a previously stored result, or press any other key and Enter to continue: ");
                    if (Console.ReadLine() == "p")
                    {
                        Console.WriteLine("Select which results you would like to use: \n");

                        for (int i = 0; i < calculator.ResultHistory.Count; i++)
                        {
                            Console.WriteLine($"\t{i + 1} - {calculator.ResultHistory[i]}");
                        }

                        Console.WriteLine();
                        Console.Write("Your option? ");
                        string? selectedResult = Console.ReadLine();
                        while (selectedResult == null && int.Parse(selectedResult) > 0 &&
                        int.Parse(selectedResult) <= calculator.ResultHistory.Count)
                        {
                            Console.Write("This is not valid input. Please select an appropriate option.");
                            selectedResult = Console.ReadLine();
                            // Console.WriteLine();
                        }

                        result = calculator.ResultHistory[int.Parse(selectedResult) - 1];
                    }
                }

                // Ask the user to type the first number.
                Console.WriteLine();
                Console.Write("Type a number, and then press Enter: ");
                numInput1 = Console.ReadLine();
                Console.WriteLine();

                double cleanNum1 = 0;
                while (!double.TryParse(numInput1, out cleanNum1))
                {
                    Console.Write("This is not valid input. Please enter an integer value: ");
                    numInput1 = Console.ReadLine();
                }

                // Ask the user to type the second number.
                Console.Write("Type another number, and then press Enter: ");
                numInput2 = Console.ReadLine();

                double cleanNum2 = 0;
                while (!double.TryParse(numInput2, out cleanNum2))
                {
                    Console.Write("This is not valid input. Please enter an integer value: ");
                    numInput2 = Console.ReadLine();
                }

                // Ask the user to choose an operator.
                Console.WriteLine("Choose an operator from the following list:");
                Console.WriteLine("\ta - Add");
                Console.WriteLine("\ts - Subtract");
                Console.WriteLine("\tm - Multiply");
                Console.WriteLine("\td - Divide\n");

                Console.Write("Your option? ");

                string? op = Console.ReadLine();

                // Validate input is not null, and matches the pattern
                while (op == null || !Regex.IsMatch(op, "[a|s|m|d]"))
                {
                    Console.WriteLine("Error: Unrecognized input. Please try again.");
                }

                try
                {
                    result += calculator.DoOperation(cleanNum1, cleanNum2, op);
                    if (double.IsNaN(result))
                    {
                        Console.WriteLine("This operation will result in a mathematical error.\n");
                    }
                    else
                    {
                        Console.WriteLine();
                        Console.WriteLine("Your result: {0:0.##}\n", result);

                        // Console.WriteLine("Would you like to store this result?");
                        // Console.WriteLine("\ty - Yes");
                        // Console.WriteLine("\tn - No");
                        // string? storeDecisionSingle = Console.ReadLine();
                        // while (storeDecisionSingle == null || !Regex.IsMatch(storeDecisionSingle, "[y | n]"))
                        // {
                        //     Console.WriteLine("Error: Unrecognized input. Please try again.");
                        // }

                        // Console.WriteLine("------------------------\n");
                        // if (storeDecisionSingle == "y")
                        // {
                        //     // calculator.ShouldStoreResults = true;
                        //     Console.WriteLine("Your result has been stored.\n", result);
                        //     calculator.ResultHistory.Add(result);

                        //     if (!calculator.ShouldStoreResults)
                        //     {
                        //         Console.WriteLine("Would you like to keep storing results from now on?");
                        //         Console.WriteLine("\ty - Yes");
                        //         Console.WriteLine("\tn - No");
                        //         string? storeDecision = Console.ReadLine();
                        //         while (storeDecision == null || !Regex.IsMatch(storeDecision, "[y | n]"))
                        //         {
                        //             Console.WriteLine("Error: Unrecognized input. Please try again.");
                        //         }

                        //         Console.WriteLine("------------------------\n");
                        //         if (storeDecision == "y")
                        //         {
                        //             calculator.ShouldStoreResults = true;
                        //             Console.WriteLine("Your results will be stored.\n", result);
                        //             calculator.ResultHistory.Add(result);
                        //         }
                        //         // else
                        //         // {
                        //         //     Console.WriteLine("Your results was NOT stored.\n", result);
                        //         // }
                        //     }
                        // }
                        // else
                        // {
                        //     Console.WriteLine("Your result was NOT stored.\n", result);
                        // }
                        // Wait for the user to respond before closing.

                        Console.WriteLine("------------------------\n");
                        if (calculator.ResultHistory.Count > 0)
                        {
                            // Console.WriteLine("Would you like to delete the previously stored results?");
                            // Console.WriteLine("\ty - Yes");
                            // Console.WriteLine("\tn - No");
                            // string? deleteDecision = Console.ReadLine();
                            // while (deleteDecision == null || !Regex.IsMatch(deleteDecision, "[y | n]"))
                            // {
                            //     Console.WriteLine("Error: Unrecognized input. Please try again.");
                            // }

                            // Console.WriteLine("------------------------\n");
                            // if (deleteDecision == "y")
                            // {
                            //     calculator.ResultHistory.Clear();
                            //     calculator.ShouldStoreResults = false;
                            //     Console.WriteLine("Previous results have been deleted.\n", result);
                            // }
                            // else
                            // {
                            //     Console.WriteLine("Previous results were NOT deleted.\n", result);

                            // }

                            Console.Write("Press 'd' to delete all previously stored results, or press any other key and Enter to continue: ");
                            if (Console.ReadLine() == "d")
                            {
                                Console.WriteLine();
                                calculator.ResultHistory.Clear();
                                Console.WriteLine("Previously stored results have been deleted.");
                            }
                        }

                        Console.Write("Press 's' to store this result, or press any other key and Enter to continue: ");
                        if (Console.ReadLine() == "s")
                        {
                            Console.WriteLine();
                            calculator.ResultHistory.Add(result);
                            Console.WriteLine("Result stored.");
                        }

                        Console.WriteLine();
                        Console.WriteLine($"The calculator was used {calculator.UsageCount} time(s).\n");
                        Console.WriteLine("------------------------\n");
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Oh no! An exception occurred trying to do the math.\n - Details: " + e.Message);
                }
                // Wait for the user to respond before closing.
                Console.Write("Press 'n' and Enter to close the app, or press any other key and Enter to continue: ");
                if (Console.ReadLine() == "n") endApp = true;

                Console.WriteLine("-----------------------------------------------------------------------------------------------------------------------\n");
            }
            calculator.Finish();
            return;
        }
    }
}
