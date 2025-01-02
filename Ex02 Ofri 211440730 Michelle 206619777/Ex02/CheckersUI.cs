using System;
using System.Linq;


namespace Ex02
{
    internal class CheckersUI
    {
        private const char k_InvalidCharInName = ' ';
        private const int k_MaxNameLength = 20;
        private const string k_BoardStyle = "====";
        private const char k_MoveSplitChar = '>';
        private const int k_MoveInputSize = 5;
        private const string k_QuitChar = "Q";
        private CheckersGame m_CheckersGame;
        private int m_GameNumber = 1;
        private const string k_ComputerPlayerName = "Computer";
        private bool m_GameQuitedByPlayer = false;
        private bool m_GameFinished = false;

        internal void StartGame()
        {
            initGame();
            playGame();

            bool anotherGame = getFromUserIsContinueToAnotherGame();
            while (anotherGame)
            {
                m_GameNumber++;
                printStartGameMessage(m_GameNumber, m_CheckersGame.Player1.Name, m_CheckersGame.Player2.Name);
                m_CheckersGame.ResetGame();
                printBoard(m_CheckersGame.GameBoard);
                playGame();
                anotherGame = getFromUserIsContinueToAnotherGame();
            }
        }

        private void initGame()
        {
            printWelcomeMessage();
            Player player1 = new Player(getPlayerName(), ePlayerType.Human, eCheckersPieceType.XPiece);
            Player player2 = initSecondPlayer();
            m_CheckersGame = new CheckersGame(player1, player2, getBoardSize());
            printStartGameMessage(m_GameNumber, player1.Name, player2.Name);
            printBoard(m_CheckersGame.GameBoard);
        }

        private void playGame()
        {
            m_GameQuitedByPlayer = false;
            m_GameFinished = false;

            while (!m_GameFinished && !m_GameQuitedByPlayer)
            {
                m_CheckersGame.handleGameStateBeforeNextMove();
                if (m_CheckersGame.IsStalemate)
                {
                    printStalemateMessage(m_CheckersGame.Player1,m_CheckersGame.Player2);
                    m_GameFinished = true;
                    break;
                }

                if (m_CheckersGame.IsActivePlayerWon)
                {
                    printWinMessage(m_CheckersGame.ActivePlayer, m_CheckersGame.Player1,m_CheckersGame.Player2);
                    m_GameFinished = true;
                    break;
                }

                printPlayerTurn(m_CheckersGame.ActivePlayer);
                CheckersBoardMove move = getNextValidMoveOrQuitGame();
                if (m_GameQuitedByPlayer)
                {
                    m_CheckersGame.HandleOpponentWin();
                    printWinMessage(m_CheckersGame.ActivePlayer, m_CheckersGame.Player1, m_CheckersGame.Player2);
                    m_GameFinished = true;
                    break;
                }

                m_CheckersGame.playMove(move);
                printBoard(m_CheckersGame.GameBoard);
                printPlayedMove(move, m_CheckersGame.ActivePlayer);
                m_CheckersGame.handleGameStateAfterMove();
                if (m_CheckersGame.IsActivePlayerWon)
                {
                    printWinMessage(m_CheckersGame.ActivePlayer, m_CheckersGame.Player1, m_CheckersGame.Player2);
                    m_GameFinished = true;     
                }
            }
        }

        private CheckersBoardMove getNextValidMoveOrQuitGame()
        {
            CheckersBoardMove move;

            if (m_CheckersGame.ActivePlayer.PlayerType == ePlayerType.Computer)
            {
                printComputerMessage();
                uint randomIndex = (uint)new Random().Next(m_CheckersGame.ValidMoves.Count);
                move = m_CheckersGame.ValidMoves[(int)randomIndex];
            }
            else
            {
                getMoveOrQuitGameByPlayer(out move);
                
                while (!m_GameQuitedByPlayer && !m_CheckersGame.CheckMovePartOfValidMoves(move))
                {
                    printMoveInvalid();
                    getMoveOrQuitGameByPlayer(out move);
                }
            }

            return move;
        }

        private Player initSecondPlayer()
        {
            ePlayerType secondPlayerType = getSecondPlayerType();
            string secondPlayerName;

            if (secondPlayerType == ePlayerType.Human)
            {
                secondPlayerName = getPlayerName();
            }
            else
            {
                secondPlayerName = k_ComputerPlayerName;
            }

            return new Player(secondPlayerName, secondPlayerType, eCheckersPieceType.OPiece);
        }

        private void printWelcomeMessage()
        {
            Console.WriteLine("Welcome to checkers game!");
        }

        private string getPlayerName()
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

        private eCheckersBoardSize getBoardSize()
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

        private ePlayerType getSecondPlayerType()
        {
            Console.WriteLine("Choose opponent type - for Computer press 0, for second player press 1");
            string playerType = getUserInput();

            while (!isInputPartOfIntEnum(playerType, typeof(ePlayerType)))
            {
                Console.WriteLine("Player type is not valid - for Computer press 0, for second player press 1");
                playerType = getUserInput();
            }

            return (ePlayerType)Enum.Parse(typeof(ePlayerType), playerType);
        }

        private void printBoard(CheckersBoard i_CheckersBoard)
        {
            eCheckersPieceType[,] i_Board = i_CheckersBoard.Board;
            eCheckersBoardSize i_BoardSize = i_CheckersBoard.Size;
            char rowLetter = 'A', colLetter = 'a';

            Ex02.ConsoleUtils.Screen.Clear();
            for (int col = 0; col < (int)i_BoardSize; col++)
            {
                Console.Write("   {0}", colLetter++);
            }

            Console.WriteLine();
            for (int row = 0; row <= (int)i_BoardSize * 2; row++)
            {
                if (row % 2 == 0)
                {
                    Console.Write(" ");
                }

                for (int col = 0; col < (int)i_BoardSize; col++)
                {
                    if (row % 2 == 0)
                    {
                        Console.Write("{0}", k_BoardStyle);
                    }
                    else
                    {
                        if (col == 0)
                        {
                            Console.Write("{0}", rowLetter++);
                        }
                        Console.Write("| {0} ", (char)i_Board[(row - 1) / 2, col]);
                    }
                }

                Console.WriteLine("{0}", row % 2 == 0 ? "=" : "|");
            }
        }

        private void printStartGameMessage(int i_GameNumber, string i_FirstPlayerName, string i_SecondPlayerName)
        {
            Console.WriteLine("Starting game number {0}: {1} against {2}, ", i_GameNumber, i_FirstPlayerName, i_SecondPlayerName);
        }

        private void printPlayerTurn(Player i_Player)
        {
            Console.WriteLine("{0}'s turn ({1}):", i_Player.Name, i_Player.PieceType);
        }

        private void getMoveOrQuitGameByPlayer(out CheckersBoardMove o_Move)
        {
            Console.WriteLine("Enter move");
            string moveInput = getUserInput();
            o_Move = new CheckersBoardMove();
            while (moveInput != k_QuitChar && !isValidMoveInput(moveInput))
            {
                Console.WriteLine("Invalid move input!, move should have the be in the format ROWCol>ROWCol, for example Fc>Fb");
                moveInput = getUserInput();
            }

            if (moveInput == k_QuitChar)
            {
                m_GameQuitedByPlayer = true;
            }
            else
            {
                o_Move.SetMove(moveInput);
            }
        }

        private void printComputerMessage()
        {
            Console.WriteLine("Computer’s Turn (press ‘enter’ to see it’s move)");
            Console.ReadLine();
        }

        private void printPlayedMove(CheckersBoardMove i_Move, Player i_Player)
        {
            Console.WriteLine("{0}'s move was ({1}): {2}{3}>{4}{5}",
                i_Player.Name, i_Player.PieceType, (char)(i_Move.From.Row + 'A'), (char)(i_Move.From.Column + 'a'), (char)(i_Move.To.Row + 'A'), (char)(i_Move.To.Column + 'a'));
        }

        private void printMoveInvalid()
        {
            Console.WriteLine("Move is not valid! please try different move");
        }

        private string getUserInput()
        {
            string userInput = Console.ReadLine();

            while (String.IsNullOrWhiteSpace(userInput))
            {
                Console.WriteLine("Input must not be empty, please try again");
                userInput = Console.ReadLine();
            }

            return userInput.Trim();
        }
        private bool isPlayerNameValid(string i_UserName)
        {
            return !i_UserName.Contains(k_InvalidCharInName) && i_UserName.Length <= k_MaxNameLength;
        }

        private bool isInputPartOfIntEnum(string i_Input, Type i_EnumType)
        {
            bool isValidSize = int.TryParse(i_Input, out int numericValue);

            if (isValidSize)
            {
                isValidSize = Enum.IsDefined(i_EnumType, numericValue);
            }

            return isValidSize;
        }

        private bool isValidMoveInput(string i_MoveInput)
        {
            bool isValidMoveInput = ((i_MoveInput.Length == k_MoveInputSize) && (i_MoveInput[2] == k_MoveSplitChar));

            if (isValidMoveInput)
            {
                isValidMoveInput = char.IsUpper(i_MoveInput[0]) && char.IsLower(i_MoveInput[1]) && char.IsUpper(i_MoveInput[3]) && char.IsLower(i_MoveInput[4]);
            }

            return isValidMoveInput;
        }

        private void printWinMessage(Player i_ActivePlayer, Player i_Player1, Player i_Player2)
        {
            Console.WriteLine("{0} Won!!", i_ActivePlayer.Name);
            printScore(i_Player1, i_Player2);
        }

        private void printScore(Player i_Player1, Player i_Player2)
        {
            Console.WriteLine("{0}'s score is {1}, {2}'s score is {3}", i_Player1.Name, i_Player1.Score, i_Player2.Name, i_Player2.Score);
        }

        private void printStalemateMessage(Player i_Player1, Player i_Player2)
        {
            Console.WriteLine("Stalemate! No one won :(");
            printScore(i_Player1, i_Player2);
        }

        private bool getFromUserIsContinueToAnotherGame()
        {
            Console.WriteLine("Would you like to play another game? yes - press 1, no - press 0");
            string inputFromUser = getUserInput();
            bool anotherGame = false;

            while (inputFromUser != "1" && inputFromUser != "0")
            {
                Console.WriteLine("Invalid input, Would you like to play another game? yes - press 1, no - press 0");
                inputFromUser = getUserInput();
            }

            if (inputFromUser == "1")
            {
                anotherGame = true;
            }

            return anotherGame;
        }
    }
}
