// CalculatorLibrary.cs
using System.Diagnostics;
using Newtonsoft.Json;
namespace CalculatorLibrary;

class Calculator
{
    JsonWriter writer;
    public int UsageCount { get; set; }
    public List<double> ResultHistory { get; set; }
    public Boolean ShouldStoreResults { get; set; }
    public Calculator()
    {
        UsageCount = 0;
        ResultHistory = [];
        ShouldStoreResults = false;

        StreamWriter logFile = File.CreateText("calculatorlog.json");
        logFile.AutoFlush = true;
        writer = new JsonTextWriter(logFile);
        writer.Formatting = Formatting.Indented;
        writer.WriteStartObject();
        writer.WritePropertyName("Operations");
        writer.WriteStartArray();
    }

    // TODO: Create a functionality that will count the amount of times the calculator was used.
    // TODO: Store a list with the latest calculations. And give the users the ability to delete that list.
    // TODO: Allow the users to use the results in the list above to perform new calculations.
    // TODO: Add extra calculations: Square Root, Taking the Power, 10x, Trigonometry functions.

    // CalculatorLibrary.cs
    public double DoOperation(double num1, double num2, string op)
    {
        double result = double.NaN; // Default value is "not-a-number" if an operation, such as division, could result in an error.
        writer.WriteStartObject();
        writer.WritePropertyName("Operand1");
        writer.WriteValue(num1);
        writer.WritePropertyName("Operand2");
        writer.WriteValue(num2);
        writer.WritePropertyName("Operation");
        // Use a switch statement to do the math.
        switch (op)
        {
            case "a":
                result = num1 + num2;
                writer.WriteValue("Add");
                break;
            case "s":
                result = num1 - num2;
                writer.WriteValue("Subtract");
                break;
            case "m":
                result = num1 * num2;
                writer.WriteValue("Multiply");
                break;
            case "d":
                // Ask the user to enter a non-zero divisor.
                if (num2 != 0)
                {
                    result = num1 / num2;
                }
                writer.WriteValue("Divide");
                break;
            // Return text for an incorrect option entry.
            default:
                break;
        }
        writer.WritePropertyName("Result");
        writer.WriteValue(result);
        writer.WriteEndObject();

        UsageCount += 1;
        return result;
    }

    // CalculatorLibrary.cs
    public void Finish()
    {
        writer.WriteEndArray();
        writer.WriteEndObject();
        writer.Close();
    }
}