using System;
using System.IO;
using System.Linq;


namespace Ex02
{
    internal class CheckersUI
    {
        private const char invalidCharInName = ' ';
        private const int maxNameLength = 20;
        private const string boardStyle = "====";
        private const char moveSplitChar = '>';
        private const int moveSize = 5;
        private const char quit = 'Q';

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

        internal static void PrintPlayerTurn(Player i_Player)
        {
            Console.WriteLine("{0}'s turn ({1}):", i_Player.Name, i_Player.CheckersBoardPiece);
        }

        internal static CheckersBoardMove? GetMoveFromPlayer(out bool m_GameQuitedByPlayer)
        {
            Console.WriteLine("Enter move");
            string moveInput = getUserInput().Trim();

            CheckersBoardMove? checkersMove = null;

            if(moveInput.Equals(quit))
            {
                m_GameQuitedByPlayer = true;
            }
            else 
            {
                m_GameQuitedByPlayer= false;
                while (!isValidMoveInput(moveInput))
                {
                    Console.WriteLine("Invalid move input!, move should have the be in the format ROWCol>ROWCol, for example Fc>Fb");
                    moveInput = getUserInput();
                }

                checkersMove = new CheckersBoardMove(moveInput);
            }

            return checkersMove;
        }

        internal static void PrintPlayedMove(CheckersBoardMove move, Player i_Player)
        {
            Console.WriteLine("{0}'s move was ({1}): {2}{3}>{4}{5}",
                i_Player.Name, i_Player.CheckersBoardPiece, move.From.Row, move.From.Column, move.To.Row, move.To.Column);
        }

        internal static void PrintMoveInvalid()
        {
            Console.WriteLine("Move is not valid! please try different move");
        }

        private static string getUserInput()
        {
            string userInput = Console.ReadLine();
            while (String.IsNullOrWhiteSpace(userInput))
            {
                Console.WriteLine("Input must not be empty, please try again");
                userInput = Console.ReadLine();
            }

            return userInput.Trim();
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

        private static bool isValidMoveInput(string i_MoveInput)
        {
            bool isValideMoveInput = i_MoveInput[2] == moveSplitChar && i_MoveInput.Length == moveSize;

            if (isValideMoveInput)
            {
                isValideMoveInput = char.IsUpper(i_MoveInput[0]) && char.IsLower(i_MoveInput[1]) && char.IsUpper(i_MoveInput[3]) && char.IsLower(i_MoveInput[4]);
            }

            return isValideMoveInput;
        }

        internal static void printWinMessage(Player i_ActivePlayer, Player i_Player1, Player i_Player2, uint i_AddedScore)
        {
            Console.WriteLine("{0} Won!! and gained {1} points!", i_ActivePlayer.Name, i_AddedScore);
            printScore(i_Player1, i_Player2);
        }

        private static void printScore(Player i_Player1, Player i_Player2)
        {
            Console.WriteLine("{0}'s score is {1}, {2}'s score is {3}", i_Player1.Name, i_Player1.Score, i_Player2.Name, i_Player2.Score);
        }

        internal static void printStalemateMessage(Player i_Player1, Player i_Player2)
        {
            Console.WriteLine("No one won :(,");
            printScore(i_Player1, i_Player2);
        }

        internal static bool GetFromUserIsContinueToAnotherGame()
        {
            return false;
        }
    }
}
