using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Ex02
{
    internal class CheckersUI
    {
        public static void WelcomeMessage()
        {
            Console.WriteLine("Welcome to checkers game!");
        }

        public static string GetPlayerName()
        {
            Console.WriteLine("Please enter your name, without spaces and up to 20 characters");
            string userName = getUserInput();

            while (!isPlayerNameValid(userName))
            {
                Console.WriteLine("Invalid name, please make sure the name is without spaces and up to 20 characters");
                userName = getUserInput();
            }

            return userName;
        }

        public static eCheckersBoardSize GetBoardSize()
        {
            Console.WriteLine("Please enter board size, the options are 6, 8, or 10");
            string boardSize = getUserInput();

            while (!isBoardSizeValid(boardSize))
            {
                Console.WriteLine("Boasr size is not valid, the options are 6, 8, or 10");
                boardSize = getUserInput();
            }

            return (eCheckersBoardSize)Enum.Parse(typeof(eCheckersBoardSize), boardSize);
        }

        private static string getUserInput()
        {
            string userInput = Console.ReadLine();
            while (String.IsNullOrEmpty(userInput))
            {
                Console.WriteLine("Input must not be empty, please try again");
                userInput = Console.ReadLine();
            }

            return userInput;
        }

        private static bool isPlayerNameValid(string i_UserName)
        {
            return !i_UserName.Contains(" ") && i_UserName.Length <= 20; //todo create consts 
        }

        private static bool isBoardSizeValid(string i_BoardSize)
        {
            bool isValidSize = int.TryParse(i_BoardSize,out int size);

            if (isValidSize) {
                isValidSize = Enum.IsDefined(typeof(eCheckersBoardSize), size);
            }

            return isValidSize;
        }
    }
}
