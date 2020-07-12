using System.Collections.Generic;
using System.IO;

public class Database
{
    public readonly string Path;

    public Database(string path){
        Path = $"{Directory.GetCurrentDirectory()}{path}";
    }
    public List<Contact> Read(){
         var contacts = new List<Contact>();
        using (var reader = new StreamReader(Path))
        {
            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                var values = line.Split(',');
                var firstName = values[0];
                var lastName = values[1];
                var phoneNumber = values[2];
                var address = values[3];
                contacts.Add(new Contact(firstName, lastName, phoneNumber, address));
            }
        }
        return contacts;
    }
    public void Write(List<Contact> contacts)
    {
        using (var writer = new StreamWriter(Path))
        {
            foreach (var contact in contacts)
            {
                var line = $"{contact.FirstName},{contact.LastName},{contact.PhoneNumber},{contact.Address}";
                writer.WriteLine(line);
            }
        }
    }
}
