using DG3.PracticalExam.Utilities;
using DG3.PracticeExam.DataAccess;
using DG3.PracticeExam.DataAccess.DataManager;
using DG3.PracticeExam.DataAccess.Repositories;
using DG3.PracticeExam.Domain.Entities;
using DG3.PracticeExam.Domain.Interfaces;
using DG3.PracticeExam.Domain.ValueObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DG3.PracticalExam.Test
{
    [TestClass]
    public class PracticalExamTest
    {
        [TestMethod]
        public void Test_ReadXmlFile_HasCustomers()
        {
            var filePath = @$"{Environment.CurrentDirectory}\cust_import.xml";

            var result = XmlHelper.ReadXmlFile<Customers>(filePath, "customers");

            Assert.AreEqual(result.Customer.Count, 3);
        }

        [TestMethod]
        public void Test_CustomerRepository_CanInsertToDb()
        {
            // Arrange
            IDbManager manager = new SqlDbManager(Constants.ConnectionString);

            IRepository<Customer> repository = new CustomerRepository(manager);

            var filePath = @$"{Environment.CurrentDirectory}\cust_import.xml";

            // Act and assert
            var readFromXml = XmlHelper.ReadXmlFile<Customers>(filePath, "customers");

            foreach (var customer in readFromXml.Customer)
            {
                var customerId = repository.Insert(customer);

                Assert.IsTrue(customerId > 0);
            }
        }
    }
}
