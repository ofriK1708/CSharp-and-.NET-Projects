using System;
using System.Linq;


namespace Ex02
{
    internal class CheckersUI
    {
        private const char invalidCharInName = ' ';
        private const int maxNameLength = 20;
        private const string boardStyle = "====";

        internal static void PrintWelcomeMessage()
        {
            Console.WriteLine("Welcome to checkers game!");
        }

        internal static string GetPlayerName()
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

        internal static eCheckersBoardSize GetBoardSize()
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

       internal static ePlayerType GetSecondPlayerType()
        {
            Console.WriteLine("Choose opponent type - for Computer press 0, for second player press 1");

            string playerType = getUserInput();

            while (!isInputPartOfIntEnum(playerType, typeof(ePlayerType)))
            {
                Console.WriteLine("Player type is not valid - for Computer press 0, for second player press 1");
                playerType = getUserInput();
            }

            return (ePlayerType)Enum.Parse (typeof(ePlayerType), playerType);
        }

        internal static void PrintBoard(eCheckersBoardPiece[,] i_Board, eCheckersBoardSize i_BoardSize)
        {
            Ex02.ConsoleUtils.Screen.Clear();
            char rowLetter = 'A', colLetter = 'A';
            for(int col= 0; col < (int)i_BoardSize; col++)
            {
                Console.Write("   {0}", colLetter++);
            }

            Console.WriteLine();
            for (int row = 0; row <= (int)i_BoardSize * 2; row++)
            {
                if(row % 2 == 0)
                {
                    Console.Write(" ");
                }
                for (int col = 0; col < (int)i_BoardSize; col++)
                {
                    if (row % 2 == 0)
                    {
                        Console.Write("{0}", boardStyle);
                    }
                    else
                    {
                        if (col == 0)
                        {
                            Console.Write("{0}", rowLetter++);
                        }
                        Console.Write("| {0} ", (char)i_Board[col, (row-1) / 2]);
                    }
                }
                Console.WriteLine("{0}",row % 2 == 0 ? "=" : "|");
            }
        }

        internal static void PrintStartGameMessage(int i_GameNumber, string i_FirstPlayerName, string i_SecondPlayerName)
        {
            Console.WriteLine("Starting game number {0}: {1} against {2}, " , i_GameNumber, i_FirstPlayerName, i_SecondPlayerName);
        }

        internal static void PrintPlayerTurn(string i_PlayerName)
        {
            Console.WriteLine("{}'s turn:", i_PlayerName);
        }

        private static string getUserInput()
        {
            string userInput = Console.ReadLine();
            while (String.IsNullOrWhiteSpace(userInput))
            {
                Console.WriteLine("Input must not be empty, please try again");
                userInput = Console.ReadLine();
            }

            return userInput;
        }

        private static bool isPlayerNameValid(string i_UserName)
        {
            return !i_UserName.Contains(invalidCharInName) && i_UserName.Length <= maxNameLength;
        }

        private static bool isInputPartOfIntEnum(string i_Input, Type i_EnumType)
        {
            bool isValidSize = int.TryParse(i_Input, out int numericValue);

            if (isValidSize) 
            {
                isValidSize = Enum.IsDefined(i_EnumType, numericValue);
            }

            return isValidSize;
        }
    }
}
