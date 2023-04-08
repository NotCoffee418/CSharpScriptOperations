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
}
