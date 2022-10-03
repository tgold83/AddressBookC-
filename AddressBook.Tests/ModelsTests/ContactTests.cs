using MySql.Data.MySqlClient;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using AddressBook.Models;
using System;

namespace AddressBook.Tests
{
  [TestClass]
  public class ContactTests : IDisposable
  {
    public void Dispose()
    {
      Contact.ClearAll();
    }

    public ContactTests()
    {
      DBConfiguration.ConnectionString = "server=localhost;user id=root;password=epicodus;port=3306;database=address_book_test;";
    }

    [TestMethod]
    public void GetAll_ReturnsEmptyListFromDatabase_ContactList()
    {
      //Arrange
      List<Contact> newList = new List<Contact> { };

      //Act
      List<Contact> result = Contact.GetAll();
      Console.WriteLine(result.Count);

      //Assert
      CollectionAssert.AreEqual(newList, result);
    }

    [TestMethod]
    public void Save_SavesToDatabase_ContactList()
    {
      //Arrange
      string test1 = "test1";
      string test2 = "test2";
      string test3 = "test3";
      string test4 = "test4";
      Contact testContact = new Contact(test1, test2, test3, test4);

      //Act
      testContact.Save();
      List<Contact> result = Contact.GetAll();
      Console.WriteLine(result.Count);
      List<Contact> testList = new List<Contact>{testContact};

      //Assert
      CollectionAssert.AreEqual(testList, result);
    }

    // [TestMethod]
    // public void ContactConstructor_CreatesInstanceOfContact_Contact()
    // {
    //   Contact newContact = new Contact("test");
    //   Assert.AreEqual(typeof(Contact), newContact.GetType());
    // }

    // [TestMethod]
    // public void GetDescription_ReturnsDescription_String()
    // {
    //   //Arrange
    //   string description = "Walk the dog.";

    //   //Act
    //   Contact newContact = new Contact(description);
    //   string result = newContact.Description;

    //   //Assert
    //   Assert.AreEqual(description, result);
    // }

    // [TestMethod]
    // public void SetDescription_SetDescription_String()
    // {
    //   //Arrange
    //   string description = "Walk the dog.";
    //   Contact newContact = new Contact(description);

    //   //Act
    //   string updatedDescription = "Do the dishes";
    //   newContact.Description = updatedDescription;
    //   string result = newContact.Description;

    //   //Assert
    //   Assert.AreEqual(updatedDescription, result);
    // }

    // [TestMethod]
    // public void GetAll_ReturnsEmptyList_ContactList()
    // {
    //   // Arrange
    //   List<Contact> newList = new List<Contact> { };

    //   // Act
    //   List<Contact> result = Contact.GetAll();

    //   // Assert
    //   CollectionAssert.AreEqual(newList, result);
    // }

    [TestMethod]
    public void GetAll_ReturnsContacts_ContactList()
    {
      //Arrange
      string firstName1 = "firstName1";
      string firstName2 = "SecondFirstName";
      Contact newContact1 = new Contact(firstName1, "lname", "pn", "email");
      newContact1.Save(); // New code
      Contact newContact2 = new Contact(firstName2, "lname", "pn", "email");
      newContact2.Save(); // New code
      List<Contact> newList = new List<Contact> { newContact1, newContact2 };

      //Act
      List<Contact> result = Contact.GetAll();

      //Assert
      CollectionAssert.AreEqual(newList, result);
    }

    // [TestMethod]
    // public void GetId_ContactsInstantiateWithAnIdAndGetterReturns_Int()
    // {
    //   //Arrange
    //   string description = "Walk the dog.";
    //   Contact newContact = new Contact(description);

    //   //Act
    //   int result = newContact.Id;

    //   //Assert
    //   Assert.AreEqual(1, result);
    // }

    [TestMethod]
    public void Find_ReturnsCorrectContactFromDatabase_Contact()
    {
      //Arrange
      Contact newContact1 = new Contact("fname", "lname", "pn", "email");
      newContact1.Save();
      Contact newContact2 = new Contact("fname2", "lname2", "pn2", "email2");
      newContact2.Save();

      //Act
      Contact foundContact = Contact.Find(newContact1.Id);
      //Assert
      Assert.AreEqual(newContact1, foundContact);
    }
  }
}

// using Microsoft.VisualStudio.TestTools.UnitTesting;
// using AddressBook.Models;
// using System.Collections.Generic;
// using System;

// namespace AddressBook.Tests
// {
//   [TestClass]
//   public class CategoryTests : IDisposable
//   {

//     public void Dispose()
//     {
//       Category.ClearAll();
//     }

//     [TestMethod]
//     public void CategoryConstructor_CreatesInstanceOfCategory_Category()
//     {
//       Category newCategory = new Category("test category");
//       Assert.AreEqual(typeof(Category), newCategory.GetType());
//     }

//     [TestMethod]
//     public void GetName_ReturnsName_String()
//     {
//       //Arrange
//       string name = "Test Category";
//       Category newCategory = new Category(name);

//       //Act
//       string result = newCategory.Name;

//       //Assert
//       Assert.AreEqual(name, result);
//     }

//     [TestMethod]
//     public void GetId_ReturnsCategoryId_Int()
//     {
//       //Arrange
//       string name = "Test Category";
//       Category newCategory = new Category(name);

//       //Act
//       int result = newCategory.Id;

//       //Assert
//       Assert.AreEqual(1, result);
//     }

//     [TestMethod]
//     public void GetAll_ReturnsAllCategoryObjects_CategoryList()
//     {
//       //Arrange
//       string name01 = "Work";
//       string name02 = "School";
//       Category newCategory1 = new Category(name01);
//       Category newCategory2 = new Category(name02);
//       List<Category> newList = new List<Category> { newCategory1, newCategory2 };

//       //Act
//       List<Category> result = Category.GetAll();

//       //Assert
//       CollectionAssert.AreEqual(newList, result);
//     }

//     [TestMethod]
//     public void Find_ReturnsCorrectCategory_Category()
//     {
//       //Arrange
//       string name01 = "Work";
//       string name02 = "School";
//       Category newCategory1 = new Category(name01);
//       Category newCategory2 = new Category(name02);

//       //Act
//       Category result = Category.Find(2);

//       //Assert
//       Assert.AreEqual(newCategory2, result);
//     }

//     [TestMethod]
//     public void AddContact_AssociatesContactWithCategory_ContactList()
//     {
//       //Arrange
//       string description = "Walk the dog.";
//       Contact newContact = new Contact(description);
//       List<Contact> newList = new List<Contact> { newContact };
//       string name = "Work";
//       Category newCategory = new Category(name);
//       newCategory.AddContact(newContact);

//       //Act
//       List<Contact> result = newCategory.Contacts;

//       //Assert
//       CollectionAssert.AreEqual(newList, result);
//     }
//   }
// }