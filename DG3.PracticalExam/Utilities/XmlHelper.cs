using DG3.PracticeExam.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace DG3.PracticalExam.Utilities
{
    public static class XmlHelper
    {
        public static T ReadXmlFile<T>(string path, string root) where T : class, new()
        {
            T data = new T();

            using (FileStream fileStream = new FileStream(path, FileMode.Open))
            {

                XmlSerializer serializer = new XmlSerializer(typeof(T));

                data = serializer.Deserialize(fileStream) as T;
            }

            return data;
        }
    }

    

}
