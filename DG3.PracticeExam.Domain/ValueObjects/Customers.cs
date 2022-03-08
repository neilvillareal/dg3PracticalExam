using DG3.PracticeExam.Domain.Entities;
using System.Xml.Serialization;

namespace DG3.PracticeExam.Domain.ValueObjects
{
    [XmlRoot(ElementName = "customers", Namespace = "http://dg3.com/schemas/customers")]
    public class Customers
    {
        [XmlElement(ElementName = "customer", Namespace = "http://dg3.com/schemas/customers")]
        public List<Customer> Customer { get; set; }
        [XmlAttribute(AttributeName = "xmlns")]
        public string Xmlns { get; set; }
    }
}
