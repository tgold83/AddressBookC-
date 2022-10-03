// using System.Collections.Generic;

// namespace AddressBook.Models
// {
//   public class Contact
//   {
//     private static List<Category> _instances = new List<Category> {};
//     public string Name { get; set; }
//     public int Id { get; }
//     public List<Contact> Contacts { get; set; }

//     public Category(string categoryName)
//     {
//       Name = categoryName;
//       _instances.Add(this);
//       Id = _instances.Count;
//       Contacts = new List<Contact>{};
//     }

//     public static void ClearAll()
//     {
//       _instances.Clear();
//     }

//     public static List<Category> GetAll()
//     {
//       return _instances;
//     }

//     public static Category Find(int searchId)
//     {
//       return _instances[searchId-1];
//     }

//     public void AddContact(Contact contact)
//     {
//       Contacts.Add(contact);
//     }
//   }
// }

using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace AddressBook.Models
{
  public class Contact
  {
    //fname, lname, phone number, email
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string PhoneNumber { get; set; }
    public string Email { get; set; }
    public int Id { get; set; }

    public Contact(string firstName, string lastName, string phoneNumber, string email)
    {
      FirstName = firstName;
      LastName = lastName;
      PhoneNumber = phoneNumber;
      Email = email;
    }
    public Contact(string firstName, string lastName, string phoneNumber, string email, int id)
    {
      FirstName = firstName;
      LastName = lastName;
      PhoneNumber = phoneNumber;
      Email = email;
      Id = id;
    }

    public override bool Equals(System.Object otherContact)
    {
      if (!(otherContact is Contact))
      {
        return false;
      }
      else
      {
        Contact newContact = (Contact) otherContact;
        bool idEquality = (this.Id == newContact.Id);
        bool firstNameEquality = (this.FirstName == newContact.FirstName);
        bool lastNameEquality = (this.LastName == newContact.LastName);
        bool phoneNumberEquality = (this.PhoneNumber == newContact.PhoneNumber);
        bool emailEquality = (this.Email == newContact.Email);
        return (idEquality && firstNameEquality && lastNameEquality && phoneNumberEquality && emailEquality);
      }
    }

    public void Save()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;

      cmd.CommandText = "INSERT INTO contacts (firstName, lastName, phoneNumber, email) VALUES (@ContactFirstName, @ContactLastName, @ContactPhoneNumber, @ContactEmail);";
      MySqlParameter paramFirstName = new MySqlParameter();
      paramFirstName.ParameterName = "@ContactFirstName";
      paramFirstName.Value = this.FirstName;
      cmd.Parameters.Add(paramFirstName);
      MySqlParameter paramLastName = new MySqlParameter();
      paramLastName.ParameterName = "@ContactLastName";
      paramLastName.Value = this.LastName;
      cmd.Parameters.Add(paramLastName);
      MySqlParameter paramPhoneNumber = new MySqlParameter();
      paramPhoneNumber.ParameterName = "@ContactPhoneNumber";
      paramPhoneNumber.Value = this.PhoneNumber;
      cmd.Parameters.Add(paramPhoneNumber);    
      MySqlParameter paramEmail = new MySqlParameter();
      paramEmail.ParameterName = "@ContactEmail";
      paramEmail.Value = this.Email;
      cmd.Parameters.Add(paramEmail);    
      cmd.ExecuteNonQuery();
      Id = (int) cmd.LastInsertedId;

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }

    public static List<Contact> GetAll()
    {
      List<Contact> allContacts = new List<Contact> { };
      MySqlConnection conn = DB.Connection();
      conn.Open();
      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = "SELECT * FROM contacts;";
      MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
      
      while (rdr.Read())
      {
          int contactId = rdr.GetInt32(0);
          string contactFirstName = rdr.GetString(1);
          string contactLastName = rdr.GetString(2);
          string ContactPhoneNumber = rdr.GetString(3);
          string contactEmail = rdr.GetString(4);
          Contact newContact = new Contact(contactFirstName, contactLastName, ContactPhoneNumber, contactEmail, contactId);
          allContacts.Add(newContact);
      }
      conn.Close();
      if (conn != null)
      {
          conn.Dispose();
      }
      return allContacts;
    }

    public static void ClearAll()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = "DELETE FROM contacts;";
      cmd.ExecuteNonQuery();
      conn.Close();
      if (conn != null)
      {
      conn.Dispose();
      }
    }
    public static Contact Find(int id)
    {
      // We open a connection.
      MySqlConnection conn = DB.Connection();
      conn.Open();

      // We create MySqlCommand object and add a query to its CommandText property. We always need to do this to make a SQL query.
      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = "SELECT * FROM contacts WHERE id = @ThisId;";

      // We have to use parameter placeholders @ThisId and a `MySqlParameter` object to prevent SQL injection attacks. This is only necessary when we are passing parameters into a query. We also did this with our Save() method.
      MySqlParameter param = new MySqlParameter();
      param.ParameterName = "@ThisId";
      param.Value = id;
      cmd.Parameters.Add(param);

      // We use the ExecuteReader() method because our query will be returning results and we need this method to read these results. This is in contrast to the ExecuteNonQuery() method, which we use for SQL commands that don't return results like our Save() method.
      MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
      int contactId = 0;
      string contactFirstName = "";
      string contactLastName = "";
      string contactPhoneNumber = "";
      string contactEmail = "";
      while (rdr.Read())
      {
        contactId = rdr.GetInt32(0);
        contactFirstName = rdr.GetString(1);
        contactLastName = rdr.GetString(2);
        contactPhoneNumber = rdr.GetString(3);
        contactEmail = rdr.GetString(4);
      }
      Contact foundContact = new Contact(contactFirstName, contactLastName, contactPhoneNumber, contactEmail, contactId);

      // We close the connection.
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return foundContact;
    }
  }
}