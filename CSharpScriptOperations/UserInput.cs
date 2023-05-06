using System.Globalization;
using static CSharpScriptOperations.UserInput;

namespace CSharpScriptOperations;

public static class UserInput
{
    /// <summary>
    /// Pose a question to the user expecting a yes/no answer
    /// </summary>
    /// <param name="question">Question sentence</param>
    /// <param name="defaultAnswer">Default answer if user presses enter without responding. Null makes empty response invalid.</param>
    /// <returns>Bool depending on user response</returns>
    public static bool PoseBoolQuestion(string question, bool? defaultAnswer = true)
    {
        // Prepare question
        string response;
        string answerOptions = "y/n";
        if (defaultAnswer.HasValue)
            if (defaultAnswer == true)
                answerOptions = "Y/n";
            else if (defaultAnswer == false)
                answerOptions = "y/N";

        // Request answer until valid response
        do
        {
            Console.WriteLine($"{question} ({answerOptions})");
            response = Console.ReadLine().ToLower();
            if (response == "yes" || response == "y")
                return true;
            else if (response == "no" || response == "n")
                return false;
            else if (response == "" && defaultAnswer.HasValue)
                return defaultAnswer.Value;
            else
            {
                Console.WriteLine("Invalid response. Please respond only with yes or no.");
                response = "";
            }
        } while (response == "");
        throw new NotImplementedException(); // doesn't happen
    }

    /// <summary>
    /// Prompt the user to provide a datetime in a desired format
    /// </summary>
    /// <param name="requestTest"></param>
    /// <param name="format"></param>
    /// <returns></returns>
    public static DateTime PoseDateTimeQuestion(string requestTest, string format = "yyyy-MM-dd")
    {
        bool goodParse = false;
        DateTime result;
        do
        {
            Console.WriteLine($"{requestTest} (format: {format})");
            string input = Console.ReadLine();
            goodParse = DateTime.TryParseExact(input, format, CultureInfo.InvariantCulture, DateTimeStyles.None, out result);
            if (!goodParse) Console.WriteLine("Invalid Input. Format should be ");
        } while (!goodParse);
        return result;
    }


    /// <summary>
    /// Prompts the user to provide an integer.
    /// </summary>
    /// <param name="requestText">The prompt text displayed to the user.</param>
    /// <returns>The parsed integer value.</returns>
    public static int PoseIntQuestion(string requestText) 
        => PoseParsedQuestion<int>(requestText, int.TryParse);

    /// <summary>
    /// Prompts the user to provide a double.
    /// </summary>
    /// <param name="requestText">The prompt text displayed to the user.</param>
    /// <returns>The parsed double value.</returns>
    public static double PoseDoubleQuestion(string requestText, string format = "0.##")
        => PoseParsedQuestion<double>(requestText, double.TryParse);

    /// <summary>
    /// Prompts the user to provide a float.
    /// </summary>
    /// <param name="requestText">The prompt text displayed to the user.</param>
    /// <returns>The parsed float value.</returns>
    public static float PoseFloatQuestion(string requestText, string format = "0.##") 
        => PoseParsedQuestion<float>(requestText, float.TryParse, format);

    /// <summary>
    /// Prompts the user to provide a long.
    /// </summary>
    /// <param name="requestText">The prompt text displayed to the user.</param>
    /// <returns>The parsed long value.</returns>
    public static long PoseLongQuestion(string requestText) 
        => PoseParsedQuestion<long>(requestText, long.TryParse);

    /// <summary>
    /// Prompts the user to provide an unsigned long.
    /// </summary>
    /// <param name="requestText">The prompt text displayed to the user.</param>
    /// <returns>The parsed unsigned long value.</returns>
    public static ulong PoseULongQuestion(string requestText) 
        => PoseParsedQuestion<ulong>(requestText, ulong.TryParse);

    /// <summary>
    /// Prompts the user to provide a short.
    /// </summary>
    /// <param name="requestText">The prompt text displayed to the user.</param>
    /// <returns>The parsed short value.</returns>
    public static short PoseShortQuestion(string requestText, string format = "0.##") 
        => PoseParsedQuestion<short>(requestText, short.TryParse, format);

    /// <summary>
    /// Prompts the user to provide an unsigned short.
    /// </summary>
    /// <param name="requestText">The prompt text displayed to the user.</param>
    /// <returns>The parsed unsigned short value.</returns>
    public static ushort PoseUShortQuestion(string requestText) 
        => PoseParsedQuestion<ushort>(requestText, ushort.TryParse);

    /// <summary>
    /// Prompts the user to provide a decimal.
    /// </summary>
    /// <param name="requestText">The prompt text displayed to the user.</param>
    /// <returns>The parsed decimal value.</returns>
    public static decimal PoseDecimalQuestion(string requestText, string format = "0.##")
        => PoseParsedQuestion<decimal>(requestText, decimal.TryParse, format);

    /// <summary>
    /// Lower level access for posing a question on a type that can be TryParsed.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="requestText"></param>
    /// <param name="tryParseHandler"></param>
    /// <param name="format"></param>
    /// <returns></returns>
    public static T PoseParsedQuestion<T>(string requestText, TryParseHandler<T> tryParseHandler, string format = "0.##")
        where T : struct
    {
        T result;
        bool goodParse;

        do
        {
            Console.WriteLine(requestText);
            string input = Console.ReadLine();
            goodParse = tryParseHandler(input, NumberStyles.Any, CultureInfo.InvariantCulture, out result);

            if (!goodParse) Console.WriteLine($"Invalid Input. Please enter a valid {typeof(T).Name} value.");
        } while (!goodParse);

        return result;
    }

    public delegate bool TryParseHandler<T>(string input, NumberStyles styles, IFormatProvider provider, out T result)
        where T : struct;
}
