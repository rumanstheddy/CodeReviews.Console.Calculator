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
            case "r":
                // Square root of num1
                if (num1 >= 0)
                {
                    result = Math.Sqrt(num1);
                }
                writer.WriteValue("SquareRoot");
                break;
            case "p":
                // Power: num1 ^ num2
                result = Math.Pow(num1, num2);
                writer.WriteValue("Power");
                break;
            case "x":
                result = Math.Pow(10, num1);
                writer.WriteValue("TenPowerX");
                break;
            case "sin":
                result = Math.Sin(num1);
                writer.WriteValue("Sine");
                break;
            case "cos":
                result = Math.Cos(num1);
                writer.WriteValue("Cosine");
                break;
            case "tan":
                result = Math.Tan(num1);
                writer.WriteValue("Tangent");
                break;
            case "sec":
                if (Math.Cos(num1) != 0)
                    result = 1 / Math.Cos(num1);
                writer.WriteValue("Secant");
                break;
            case "cosec":
                if (Math.Sin(num1) != 0)
                    result = 1 / Math.Sin(num1);
                writer.WriteValue("Cosecant");
                break;
            case "cot":
                if (Math.Tan(num1) != 0)
                    result = 1 / Math.Tan(num1);
                writer.WriteValue("Cotangent");
                break;
            default:
                break;
        }
        writer.WritePropertyName("Result");
        writer.WriteValue(result);
        writer.WriteEndObject();

        UsageCount += 1;
        return result;
    }
    public void Finish()
    {
        writer.WriteEndArray();
        writer.WriteEndObject();
        writer.Close();
    }
}