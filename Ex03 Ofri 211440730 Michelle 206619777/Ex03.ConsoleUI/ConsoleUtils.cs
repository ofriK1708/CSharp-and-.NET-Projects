using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.ConsoleUI
{
    internal class ConsoleUtils
    {

        internal void PrintMainMenu()
        {
            Console.WriteLine("Welcome to the garage, please choose an option from the menue and press enter:");
            Console.WriteLine("1.To add new vehicle to the garage press 1");
            Console.WriteLine("2.To see a list of vehicles (license plates) that in the garage press 2");
            Console.WriteLine("3.To change vehicl's status in the garage press 3");
            Console.WriteLine("4.To fill vehicl's air pressure to maximum press 4");
            Console.WriteLine("5.To fuel a vehicle press 5");
            Console.WriteLine("6.To charge a vehicle press 6");
            Console.WriteLine("7.To see vehicl's information press 7");
            Console.WriteLine("To quit , press 0");
        }

        internal object GetEnumInputFromUser()
        {
            string userInput = GetInputFromUser();
            return parseInputToIntAndValidateChoice(userInput, typeof(eMenuOption));
           
        }

       internal string GetInputFromUser()
        {
            string userInput = Console.ReadLine();
            while(!string.IsNullOrWhiteSpace(userInput))
            {
                Console.WriteLine("Input cant be empty, please try again");
            }

            return userInput;
        }

        private object parseInputToIntAndValidateChoice(string i_UserInput, Type i_EnumType)
        {
            
            bool isInt = int.TryParse(i_UserInput, out int numericValue);

            if (!isInt)
            {
                throw new FormatException("Input must be and Integer");
            }

            if (!Enum.IsDefined(i_EnumType, numericValue))
            {
                throw new FormatException("Input must be from the specified options");
            }

            return Enum.ToObject(i_EnumType,numericValue);
        }
    }
}
