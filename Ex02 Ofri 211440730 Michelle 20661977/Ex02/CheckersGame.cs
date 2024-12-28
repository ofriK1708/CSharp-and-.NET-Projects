
namespace Ex02
{
    internal class CheckersGame
    {
        private CheckersBoard m_GameBoard;
        private Player m_Player1;
        private Player m_Player2;
        bool m_GameFinished = false;
        private const string m_ComputerPlayerName = "Computer";
        private Player m_ActivePlayer;
        private int m_GameNumber = 1;

        internal void StartGame()
        {
           initGame();
           playGame();
        }

        private void initGame()
        {
            CheckersUI.PrintWelcomeMessage();
            m_Player1 = new Player(CheckersUI.GetPlayerName(), ePlayerType.Human, eCheckersBoardPiece.FirstPlayerRegularPiece);
            m_GameBoard = new CheckersBoard(CheckersUI.GetBoardSize());
            initSecondPlayer();
            m_ActivePlayer = m_Player1;
            CheckersUI.PrintStartGameMessage(m_GameNumber, m_Player1.Name, m_Player2.Name);
        }

        private void initSecondPlayer()
        {
            ePlayerType secondPlayerType = CheckersUI.GetSecondPlayerType();

            string secondPlayerName;
            if (secondPlayerType == ePlayerType.Human)
            {
                secondPlayerName = CheckersUI.GetPlayerName();
            }
            else
            {
                secondPlayerName = m_ComputerPlayerName;
            }

            m_Player2 = new Player(secondPlayerName, secondPlayerType, eCheckersBoardPiece.SecondPlayerRegularPiece);
        }

        private void playGame()
        {
            //while (!gameFinished)
            //{
                CheckersUI.PrintBoard(m_GameBoard.Board, m_GameBoard.Size);
                CheckersUI.PrintPlayerTurn(m_ActivePlayer);
                CheckersMove move= CheckersUI.GetMoveFromPlayer();
            //validate turn
            //play turn 
            //decide who is next m_activePlayer =
  
            //}
        }
    }
}
