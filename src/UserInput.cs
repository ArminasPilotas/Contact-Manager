using System;

public static class UserInput
{
    //Tuple as return type
    public static (bool success, int input) Number(string message = null, bool clear = false)
    {
        TryWriteMessage(message, clear);
        var input = Console.ReadLine();
        return (int.TryParse(input, out var choice), choice);
    }

    public static (bool success, Contact contact) Contact(string message = null, bool clear = false)
    {
        TryWriteMessage(message, clear);
        try
        {
            var input = Console.ReadLine();
            var values = input.Split(',');

            switch (values.Length)
            {
                case 3:
                    return (true, new Contact(values[0], values[1], values[2]));
                case 4:
                    return (true, new Contact(values[0], values[1], values[2], values[3]));
                default:
                    //user input is not valid return false
                    return (false, null);
            }
        }
        catch
        {
            //returns that file and contact is null
            return (false, null);
        }
    }

    private static void TryWriteMessage(string message, bool clear)
    {
        if (string.IsNullOrWhiteSpace(message)) return;

        if (clear) Console.Clear();
        Console.WriteLine(message);
    }
}