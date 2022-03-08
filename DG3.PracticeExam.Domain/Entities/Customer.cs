using System;
using System.Xml.Serialization;
using System.Collections.Generic;

namespace DG3.PracticeExam.Domain.Entities
{

	[XmlRoot(ElementName = "customer", Namespace = "http://dg3.com/schemas/customers")]
	public class Customer : BaseEntity
	{
		[XmlElement(ElementName = "firstname", Namespace = "http://dg3.com/schemas/customers")]
		public string Firstname { get; set; }
		[XmlElement(ElementName = "lastname", Namespace = "http://dg3.com/schemas/customers")]
		public string Lastname { get; set; }
		[XmlElement(ElementName = "address1", Namespace = "http://dg3.com/schemas/customers")]
		public string Address1 { get; set; }
		[XmlElement(ElementName = "address2", Namespace = "http://dg3.com/schemas/customers")]
		public string Address2 { get; set; }
		[XmlElement(ElementName = "city", Namespace = "http://dg3.com/schemas/customers")]
		public string City { get; set; }
		[XmlElement(ElementName = "province", Namespace = "http://dg3.com/schemas/customers")]
		public string Province { get; set; }
		[XmlElement(ElementName = "postcode", Namespace = "http://dg3.com/schemas/customers")]
		public string Postcode { get; set; }
		[XmlElement(ElementName = "country", Namespace = "http://dg3.com/schemas/customers")]
		public string Country { get; set; }
		[XmlElement(ElementName = "email", Namespace = "http://dg3.com/schemas/customers")]
		public string Email { get; set; }

		[XmlElement(ElementName = "regdate", Namespace = "http://dg3.com/schemas/customers")]
		public string _Regdate { get; set; }

		[XmlIgnore]
		public DateTime? Regdate
		{
			get
			{
				if (!string.IsNullOrWhiteSpace(_Regdate))
				{
					DateTime.TryParse(_Regdate, out DateTime dateTime);
					return dateTime;
				}

				return DateTime.Now;
			}
		}
	}
}
