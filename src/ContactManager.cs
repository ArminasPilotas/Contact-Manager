using System;
using System.IO;
using System.Collections.Generic;
using System.Text.RegularExpressions;
public class ContactManager
{
    private List<Contact> contacts;
    private Display display;
    private Database database;

    public ContactManager(){
        display=new Display();
        database = new Database("/outputs/contacts.csv");
        contacts= database.Read();
    }
    public void Run()
    {
        while(true)
        {
            display.MenuChoices();
            var(success,choice)= UserInput.Number();
            if(success)
            {
               MenuOptions(choice);
            }
            else
            {
                display.InvalidChoice();
            }
        }
    }
    private bool MenuOptions (int number)
    {
        switch (number)
        {
            case 1:
                AddContact();
                return false;

            case 2:
                UpdateContact();
                return false;

            case 3:
                DeleteContact();
                return false;
            case 4:

                display.Contacts(contacts);
                display.WaitForKey();
                return false;

            case 5:
            database.Write(contacts);
            return true;

            default:
                display.InvalidChoice();
                display.WaitForKey();
                return false;
        }
    }
    private readonly string EnterContactMessage= "Enter a Name, Last Name, Phone Number, Address(Optional)\n" +
                                                  "Please separate values by comma:";
    private void AddContact()
    {
            while(true)
            {
            var (success,contact) = UserInput.Contact(EnterContactMessage,true);
            if (success && ValidPhoneNumber(contact.PhoneNumber))
            {
                contacts.Add(contact);
                break;
            }
        }
        database.Write(contacts);
        display.OperationSuccessful();

    }

    private void UpdateContact(){
        while(true){
            display.Contacts(contacts);
            var (choiceSuccess, choice) = UserInput.Number("Enter a number which contact to update: ");

            if (choiceSuccess && choice > 0 && choice <= contacts.Count)
            {
                var (contactSuccess, contact) = UserInput.Contact(EnterContactMessage, true);

                if (contactSuccess && ValidPhoneNumber(contact.PhoneNumber))
                {
                   contacts[choice-1]= contact;
                   break;
                }
            }
        }
        database.Write(contacts);
        display.OperationSuccessful();

        
    }
    private void DeleteContact()
    {
        while(true)
        {
            display.Contacts(contacts);
            var (success, choice) = UserInput.Number("Enter a number which contact to delete: ");
            if (success && choice > 0 && choice <= contacts.Count)
            {
                //checks if user enters a integer number
                contacts.RemoveAt(choice-1);
                break;
            }
        }
        database.Write(contacts);
        display.OperationSuccessful();

    }

   private bool ValidPhoneNumber(String phoneNumber)
    {
          if (new Regex(@"^\d{1,}$").Match(phoneNumber).Length == 0)
            {
            return false;
         }  
         foreach (var contact in contacts){
             if (contact.PhoneNumber.Equals(phoneNumber)){
                 return false;
             }
         }
         return true;
    }
}
// Testing notes
// if file is empty and you want to update or delete contact program runs to infinitive loop
// Program craches if needed file and folder not found
// if input is incorrect program craches