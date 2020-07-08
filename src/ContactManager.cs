using System;
using System.IO;
using System.Collections.Generic;
public class ContactManager
{
    private List<Contact> contacts = new List<Contact>();
    public void showWelcomeWindow()
    {
        Console.WriteLine("Contact Manager Program");
        Console.WriteLine("1. Add Contact");
        Console.WriteLine("2. Update Contact Information");
        Console.WriteLine("3. Delete Contact");
        Console.WriteLine("4. View All Contacts");
        Console.Write("Enter a choice: ");
        String input = Console.ReadLine();
        int choice;
        if (int.TryParse(input, out choice))
        { //checks if user enters a integer number
            choice = Convert.ToInt32(input);
            validateChoice(choice);
        }
        else
        {
            showErrorMessage();
        }
    }
    private void showErrorMessage()
    { //if user enters wrong number or not a number use this method
        Console.Clear();
        Console.WriteLine("Not a valid choice try again");
        showWelcomeWindow();
    }
    private void validateChoice(int number)
    {
        switch (number)
        {
            case 1:
                addContact();
                break;

            case 2:
                updateContact();
                break;

            case 3:
                deleteContact();
                break;

            case 4:
                showContacts();
                break;

            default:
                showErrorMessage();
                break;
        }
    }
    private void addContact()
    {
        readFile();
        Console.Clear();
        Console.WriteLine("Enter a Name, Last Name, Phone Number, Address(Optional)");
        Console.WriteLine("Please separate values by comma:");
        String input = Console.ReadLine();
        var values = input.Split(',');
        string firstName = values[0];
        string lastName = values[1];
        string phoneNumber = values[2];
        if (isPhoneNumberExits(phoneNumber) == true)
        { //number exists can't create a contact
            addContact(); //return to add contact
        }
        else
        { //number don't exits
            if (values.Length == 3)
            { //adress is not entered
                contacts.Add(new Contact(firstName, lastName, phoneNumber));
            }

            else if (values.Length == 4)
            { //address is entered
                string address = values[3];
                contacts.Add(new Contact(firstName, lastName, phoneNumber, address));
            }

            else
            { //not valid input
                addContact();
            }
        }
        showSuccessfulMessage();

    }
    private void updateContact()
    {
        Console.Clear();
        printContacts();
        Console.Write("Enter a number which contact to update: ");
        String input = Console.ReadLine();
        int choice;
        if (int.TryParse(input, out choice) && Convert.ToInt32(input) > 0 && Convert.ToInt32(input) <= contacts.Count)
        { //checks if user enters a integer number
            choice = Convert.ToInt32(input) - 1;
            Console.Clear();
            Contact contact = contacts[choice]; //take the contact object which is choosen
            Console.WriteLine("{0} {1} {2} {3}", contact.getFirstName(), contact.getLastName(), contact.getPhoneNumber(), contact.getAddress());
            Console.WriteLine("Enter a Name, Last Name, Phone Number, Address(Optional)");
            Console.WriteLine("Please separate values by comma:");
            String temporary = Console.ReadLine();
            var values = temporary.Split(',');
            string firstName = values[0];
            string lastName = values[1];
            string phoneNumber = values[2];
            if (isPhoneNumberExits(phoneNumber) == true)
            { //number exists can't create a contact
                updateContact(); //returning to updateContact
            }
            else
            {
                if (values.Length == 3)
                { //adress is not entered
                    Contact contact1 = new Contact(firstName, lastName, phoneNumber);
                    contacts[choice] = contact1;
                }

                else if (values.Length == 4)
                { //address is entered
                    string address = values[3];
                    Contact contact1 = new Contact(firstName, lastName, phoneNumber, address);
                    contacts[choice] = contact1;
                }

                else
                { //not valid input
                    updateContact();
                }
            }
        }
        else
        {
            updateContact();
        }
        showSuccessfulMessage();

    }
    private void deleteContact()
    {
        Console.Clear();
        printContacts();
        Console.Write("Enter a number which contact to delete: ");
        String input = Console.ReadLine();
        int choice;
        if (int.TryParse(input, out choice) && Convert.ToInt32(input) > 0 && Convert.ToInt32(input) <= contacts.Count)
        { //checks if user enters a integer number
            choice = Convert.ToInt32(input) - 1;
            contacts.RemoveAt(choice);
        }
        else
        {
            deleteContact();
        }
        showSuccessfulMessage();

    }
    private void showContacts()
    {
        printContacts();
        Console.WriteLine("Press any button to continue");
        Console.ReadKey();
        showWelcomeWindow();

    }
    private void printContacts()
    {
        Console.Clear();
        readFile();
        int contactNumber = 1;
        Console.WriteLine("Name, Last Name, Phone Number, Address");
        foreach (Contact contact in contacts)
        {
            Console.WriteLine("{0}) {1} {2} {3} {4}", contactNumber, contact.getFirstName(), contact.getLastName(), contact.getPhoneNumber(), contact.getAddress());
            contactNumber++;
        }
    }
    private void readFile()
    {
        contacts.Clear();
        string path = Directory.GetCurrentDirectory() + "/outputs/contacts.csv";
        using (var reader = new StreamReader(path))
        {
            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                var values = line.Split(',');
                string firstName = values[0];
                string lastName = values[1];
                string phoneNumber = values[2];
                string address = values[3];
                contacts.Add(new Contact(firstName, lastName, phoneNumber, address));
            }
        }
    }
    private void writeToFile()
    {
        string path = Directory.GetCurrentDirectory() + "/outputs/contacts.csv";
        using (var writter = new StreamWriter(path))
        {
            foreach (Contact contact in contacts)
            {
                var line = string.Format("{0},{1},{2},{3}", contact.getFirstName(), contact.getLastName(), contact.getPhoneNumber(), contact.getAddress());
                writter.WriteLine(line);
                writter.Flush();
            }
        }
    }
    private bool isPhoneNumberExits(String phoneNumber)
    { //checks if number is not exist in other contacts
        foreach (char c in phoneNumber)
        {
            if (c < '0' || c > '9')
                return true;
        }
        foreach (Contact contact in contacts)
        {
            if (contact.getPhoneNumber().Equals(phoneNumber))
            {
                return true;
            }
        }
        return false;
    }
    private void showSuccessfulMessage()
    { //use this method if contact is added, updated or deleted
        writeToFile();
        Console.WriteLine("Operation successful press any button to continue");
        Console.ReadKey();
        showWelcomeWindow();
    }
}