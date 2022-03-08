using DG3.PracticalExam;
using DG3.PracticalExam.Utilities;
using DG3.PracticeExam.DataAccess;
using DG3.PracticeExam.DataAccess.DataManager;
using DG3.PracticeExam.DataAccess.Repositories;
using DG3.PracticeExam.Domain.Entities;
using DG3.PracticeExam.Domain.Interfaces;
using DG3.PracticeExam.Domain.ValueObjects;
using System;

public class Program
{
    public static void Main()
    {
        IDbManager manager = new SqlDbManager(Constants.ConnectionString);

        IRepository<Customer> repository = new CustomerRepository(manager);

        var filePath = @$"{Environment.CurrentDirectory}\cust_import.xml";

        var readFromXml = XmlHelper.ReadXmlFile<Customers>(filePath, "customers");

        Console.WriteLine("Inserting...");
        foreach (var customer in readFromXml.Customer)
        {
            Console.Write($"FirstName: {customer.Firstname} LastName: {customer.Lastname} ");
            int customerId = repository.Insert(customer);
            Console.Write($"Customer Id: {customerId}\n");

        }

        Console.ReadKey();
    }
}