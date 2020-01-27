using System.Linq;

namespace SomeCodeTools
{
    public class Tools
    {

        public string GetFirstWord(string someText)
        {
            char[] delimiterChars = { ' ' };
            string[] words = someText.Split(delimiterChars);
            string firstElement = words.First();
            return firstElement;
        }

        public string GetWordsButFirst(string someText)
        {
            char[] delimiterChars = { ' ' };
            string[] words = someText.Split(delimiterChars);
            string restOfArray = string.Join(" ", words.Skip(1));
            return restOfArray;
        }

    }
}
