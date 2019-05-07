using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace SimpleGames
{
    /// <summary>
    /// Hangman words load
    /// </summary>
    public class XML
    {
        public int CategoryCount { get { return XmlDocument.DocumentElement.ChildNodes.Count; } }
        private XmlDocument XmlDocument;      

        public XML()
        {
            XmlDocument = new XmlDocument();
            XmlDocument.Load(@"..\Hangman.xml");
        }

        private int RandomNumber(int max)
        {
            Random random = new Random();
            return random.Next(0, max);
        }

        public string ReadWord(int point)
        {
            string name = XmlDocument.DocumentElement.ChildNodes.Item(point).Name;
            int wordsInCategory = XmlDocument.DocumentElement.ChildNodes.Item(point).ChildNodes.Count;           
            string result = XmlDocument.DocumentElement[name].ChildNodes.Item(RandomNumber(wordsInCategory)).InnerText;         
            return result.ToUpper();
        }
    }
}
