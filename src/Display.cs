using System;
using System.Collections.Generic;

public class Display
{
    public void MenuChoices()
    {
        Console.WriteLine("Contact Manager Program");
        Console.WriteLine("1. Add Contact");
        Console.WriteLine("2. Update Contact Information");
        Console.WriteLine("3. Delete Contact");
        Console.WriteLine("4. View All Contacts");
        Console.WriteLine("5. Exit");
        Console.Write("Enter a choice: ");
    }

    public void InvalidChoice()
    {
        Console.Clear();
        Console.WriteLine("Not a valid choice");
    }

    public void OperationSuccessful()
    {
        Console.WriteLine("Operation successful");
    }

    public void Contacts(List<Contact> contacts)
    {
        Console.Clear();
        Console.WriteLine("Name, Last Name, Phone Number, Address");
        for (var contactIndex = 0; contactIndex < contacts.Count; contactIndex++)
        {
            Console.WriteLine($"{contactIndex}) {contacts[contactIndex]}");
        }
    }
    public void WaitForKey(){
        Console.WriteLine("Press any button to continue");
        Console.ReadKey();
    }
}