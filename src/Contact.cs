using System;
class Contact
{
    private string firstName;
    private string lastName;
    private string phoneNumber;
    private string address;

    public Contact(string firstName, string lastName, string phoneNumber, string address)
    {
        this.firstName = firstName;
        this.lastName = lastName;
        this.phoneNumber = phoneNumber;
        this.address = address;
    }
    public Contact(string firstName, string lastName, string phoneNumber) : this(firstName, lastName, phoneNumber, String.Empty)
    {
    }
    public string getFirstName()
    {
        return this.firstName;
    }
    public string getLastName()
    {
        return this.lastName;
    }
    public string getPhoneNumber()
    {
        return this.phoneNumber;
    }
    public string getAddress()
    {
        return this.address;
    }

}