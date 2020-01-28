using CsvHelper;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using System.Xml;
using System.Linq;

namespace SomeCodeTools
{
    public class CsvFileProcessor
    {

        Tools tools = new Tools();

        public void WriteCsvFile(string filename)
        {
            List<string> someTexts = new List<string>();
            someTexts.Add("This is a first line");            
            someTexts.Add("This is a second line");
            someTexts.Add("This is a third line");
            someTexts.Add("This is a fourth line");

            using (var writer = new StreamWriter(filename))
            {
                using (var csvWriter = new CsvWriter(writer, CultureInfo.CurrentCulture))
                {
                    csvWriter.Configuration.Delimiter = ";";
                    csvWriter.Configuration.HasHeaderRecord = true;
                    csvWriter.WriteHeader(typeof(Person));

                    csvWriter.WriteHeader<Person>();
                    csvWriter.NextRecord();

                    foreach (var item in someTexts)
                    {
                        csvWriter.WriteField(item);
                        csvWriter.NextRecord();
                    }

                    writer.Flush();
                }
            }

        }

        public void CreateCSVFile(string filename)
        {
            using (var writer = new StreamWriter(filename))
            {
                using (var csvWriter = new CsvWriter(writer, CultureInfo.CurrentCulture))
                {
                    //List<Person> listOfNewDocuments = new List<Person>();
                    //List<Person> listOfUpdatedDocuments = new List<Person>();
                    csvWriter.Configuration.Delimiter = ";";
                    csvWriter.Configuration.HasHeaderRecord = true;
                    csvWriter.WriteHeader(typeof(Person));
                    csvWriter.WriteHeader<Person>();
                    csvWriter.NextRecord();
                    var listOfNewDocuments = ExtractData("/readership/new/document");
                    var listOfUpdatedDocuments = ExtractData("/readership/updated/document");
                    listOfNewDocuments.AddRange(listOfUpdatedDocuments);
                    csvWriter.WriteRecords(listOfNewDocuments);
                }
            }
        }

        public List<Person> ExtractData(string  path)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(@"D:\Test\ReaderShip.xml");

            XmlNodeList nodeList = doc.DocumentElement.SelectNodes(path);
            var lstPersons = new List<Person>();
            
            foreach (XmlNode node in nodeList)
            {
                var hitNode = node.SelectNodes("hit");
                foreach (XmlNode tinyNode in hitNode)
                {
                    var person = new Person();
                    person.Name = GetNameFromXML(tinyNode);
                    person.LastName = GetLastNameFromXML(tinyNode);
                    person.Firm = GetNodeValueFromXML(tinyNode, "reader/firm");
                    person.Email = GetNodeValueFromXML(tinyNode, "reader/email");
                    person.Channel = GetNodeValueFromXML(tinyNode, "channel");
                    person.DocumentId = GetAttributeValueFromNode(node, "id");
                    person.Title = GetAttributeValueFromNode(node, "title");
                    person.Security = GetNodeValueFromXML(node, "security[@primary='true']");
                    person.Analyst = GetNodeValueFromXML(node, "analyst[@primary='true']");
                    person.Sector = GetNodeValueFromXML(node, "sector[@primary='true']");
                    person.Read = GetNodeValueAsDateTimeFromXML(tinyNode, "read");
                    person.Engaged = GetNodeValueAsDateTimeFromXML(tinyNode, "engaged");
                    person.UpdateTime = GetNodeValueAsDateTimeFromXML(tinyNode, "updateTime");
                    lstPersons.Add(person);
                }
            }
            return lstPersons;
        }

        private string GetNameFromXML(XmlNode xmlNode)
        {
            var node = xmlNode.SelectSingleNode("reader/name");
            if(node != null)
            {
                var value = node.InnerText;
                var name = tools.GetFirstWord(value);
                return name;
            }            
            return "";
        }

        private string GetLastNameFromXML(XmlNode xmlNode)
        {
            var node = xmlNode.SelectSingleNode("reader/name");
            if(node != null)
            {
                var value = node.InnerText;
                var lastname = tools.GetWordsButFirst(value);
                return lastname;
            }            
            return "";
        }

        private string GetNodeValueFromXML(XmlNode xmlNode, string path)
        {
            var node = xmlNode.SelectSingleNode(path);
            if(node != null)
            {
                var value = node.InnerText;
                return value;
            }
            return "";
        }

        private string GetAttributeValueFromNode(XmlNode xmlNode, string attribute)
        {
            var attributeValue = xmlNode.Attributes[attribute].Value;
            return attributeValue;
        }

        private string GetNodeValueAsDateTimeFromXML(XmlNode xmlNode, string path)
        {
            var node = xmlNode.SelectSingleNode(path);
            if(node != null)
            {
                var value = node.InnerText;
                DateTime date = Convert.ToDateTime(value);
                return date.ToString();
            }
            return "";
        }
    }
}
