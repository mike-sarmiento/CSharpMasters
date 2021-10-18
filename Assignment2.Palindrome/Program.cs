using System;
using System.Text;
using System.Threading.Tasks;

namespace Assignment2.Palindrome
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "C# Display Palindrome String";

            //Instantiate 10 array of words
            var arrayOfWords = new string[10] {"mADAM","RACeCAR","nun","CIVIC","CUBE", "Deed", "BACK", "LEVEl", "FUTURE", "WORD" };

            //Variable to store the reverse word
            var currentReverseWord = new StringBuilder();

            //Iterate through the array of words
            foreach (var word in arrayOfWords)
            {
                //Loop through current word starting from last character and append it to currentReverseWord variable
                for (int index = word.Length - 1; index >= 0; index--)
                {
                    currentReverseWord.Append(word[index]);
                }

                //Compare the reverse word and current word using ToLower()/ToUpper() method in case of casing difference.
                if (currentReverseWord.ToString().ToLower() == word.ToLower())
                {
                    //display current word if equal to reverse word
                    Console.WriteLine(word.ToUpper());
                }
                //Clear the string builder for the next word
                currentReverseWord.Clear();
            }

            Console.ReadLine();
        }
    }
}
