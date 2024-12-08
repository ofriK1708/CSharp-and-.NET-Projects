using System;
using System.Linq;

namespace Ex01_04
{
    class Program
    {
        public static void Main()
        {
            string userInput = getInputFromUserAndValidate();

            bool isPalindrom = isPalindrome(userInput, 0, userInput.Length - 1);
            Console.WriteLine("The string {0} a palindrome", isPalindrom ? "is" : "is not");

            bool isUserInputAnInt = int.TryParse(userInput, out int userInputAsInt);
            if (isUserInputAnInt)
            {
                Console.WriteLine("The number {0} be devided by 4", userInputAsInt % 4 == 0 ? "can" : "can not");
            }

            if (!isUserInputAnInt)
            {
                Console.WriteLine("There are {0} lower case letters in the string", getLowerCaseLettersCount(userInput));
                Console.WriteLine("The string {0} sorted in descending alphabetical order", isLettersInDescendingOrder(userInput) ? "is" : "is not");
            }
        }

        private static string getInputFromUserAndValidate()
        {
            Console.WriteLine("Please insert 10 chracters string that contains only letters in English or numbers");
            string inputFromUser = Console.ReadLine();

            while (!isUserInputValid(inputFromUser))
            {
                Console.WriteLine("The string must contain only letters in English or numbers, please try again");
                inputFromUser = Console.ReadLine();
            }

            return inputFromUser;
        }

        private static bool isUserInputValid(string i_InputFromUser)
        {
            bool isInputValid = (i_InputFromUser.All(char.IsLetter) || i_InputFromUser.All(char.IsNumber)) && i_InputFromUser.Length == 10;

            return isInputValid;
        }

        private static bool isPalindrome(string i_InputFromUser, int i_LeftIndex, int i_RightIndex)
        {
            if (i_LeftIndex >= i_RightIndex)
            {
                return true;
            }

            if (i_InputFromUser[i_LeftIndex] != i_InputFromUser[i_RightIndex])
            {
                return false;
            }

            return isPalindrome(i_InputFromUser, i_LeftIndex + 1, i_RightIndex - 1);
        }

        private static int getLowerCaseLettersCount(string i_InputFromUser)
        {
            int lowerCaseLettersCounter = 0;

            foreach (char c in i_InputFromUser)
            {
                if (char.IsLower(c))
                {
                    lowerCaseLettersCounter++;
                }
            }

            return lowerCaseLettersCounter;
        }

        private static bool isLettersInDescendingOrder(string i_InputFromUser)
        {
            string stringInLowerCase = i_InputFromUser.ToLower();
            int i = 1;
            bool isLettersSorted = true;

            while (i < stringInLowerCase.Length && isLettersSorted)
            {
                isLettersSorted = stringInLowerCase[i] <= stringInLowerCase[i - 1];
                i++;
            }

            return isLettersSorted;
        }
    }
}
