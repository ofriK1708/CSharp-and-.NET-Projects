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
            Console.WriteLine("Please enter player name, without spaces and up to 20 characters");
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

            while (!isInputPartOfIntEnum(boardSize, typeof(eCheckersBoardSize)))
            {
                Console.WriteLine("Boasd size is not valid, the options are 6, 8, or 10");
                boardSize = getUserInput();
            }

            return (eCheckersBoardSize)Enum.Parse(typeof(eCheckersBoardSize), boardSize);
        }

        public static ePlayerType GetSecondPlayerType()
        {
            Console.WriteLine("Choose oponent: " +
                "for Computer press 0" +
                "for second player press 1");

            string playerType = getUserInput();

            while (!isInputPartOfIntEnum(playerType, typeof(ePlayerType)))
            {
                Console.WriteLine("Player type is not valid, the options are:" +
                    "for Computer press 0" +
                    "for second player press 1");
                playerType = getUserInput();
            }

            return (ePlayerType)Enum.Parse (typeof(ePlayerType), playerType);
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

        private static bool isInputPartOfIntEnum(string input, Type enumType)
        {
            bool isValidSize = int.TryParse(input, out int size);

            if (isValidSize) {
                isValidSize = Enum.IsDefined(enumType, size);
            }

            return isValidSize;
        }
    }
}
