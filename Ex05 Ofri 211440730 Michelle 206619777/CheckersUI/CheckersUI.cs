using System;
using CheckersLogic;

namespace CheckersUI
{
    internal class CheckersUI
    {
        private GameSettingsForm m_SettingsForm;
        private GameBoardForm m_GameBoardForm;
        private CheckersGame m_CheckersGame;
        private readonly Random r_RandomGenerator = new Random();

        internal void StartGame()
        {
            m_SettingsForm = new GameSettingsForm();
            if (m_SettingsForm.IsWindowClosedByDone)
            {
                Player player1 = new Player(m_SettingsForm.PlayerOneName, ePlayerType.Human, eCheckersPieceType.XPiece,
                    eCheckersPieceType.XKingPiece);
                Player player2 = initSecondPlayer(m_SettingsForm.IsPlayerTwoActive, m_SettingsForm.PlayerTwoName);

                m_CheckersGame = new CheckersGame(player1, player2, m_SettingsForm.BoardSize);
                m_GameBoardForm = new GameBoardForm(player1.Name, player2.Name, m_SettingsForm.BoardSize, m_SettingsForm.IsPlayerTwoActive);

                initEvents();
                m_CheckersGame.ResetGame();
                m_GameBoardForm.ShowDialog();
            }
        }

        private void initEvents()
        {
            m_CheckersGame.AddBoardActionsListener(m_GameBoardForm.Game_BoardReset, m_GameBoardForm.Game_PieceAdded,m_GameBoardForm.Game_PieceRemoved);
            m_CheckersGame.ActivePlayerChanged += m_GameBoardForm.Game_ActivePlayerChanged;
            m_CheckersGame.PlayerWon += m_GameBoardForm.Game_PlayerWon;
            m_CheckersGame.Stalemate += m_GameBoardForm.Game_Stalemate;
            m_GameBoardForm.FirstPositionSelect += m_CheckersGame.GameForm_FirstPositionSelected;
            m_GameBoardForm.SecondPositionSelect += m_CheckersGame.GameForm_SecondPositionSelected;
            m_GameBoardForm.NewGame += m_CheckersGame.GameForm_NewGame;
            m_GameBoardForm.ComputerTurn += GameForm_PlayComputerMove;
            m_GameBoardForm.GameRoundQuitByPlayer += m_CheckersGame.GameForm_GameRoundQuitByPlayer;
        }

        private Player initSecondPlayer(bool i_SettingsFormIsPlayerTwoActive, string i_SettingsFormPlayerTwoName)
        {
            ePlayerType secondPlayerType = i_SettingsFormIsPlayerTwoActive ? ePlayerType.Human : ePlayerType.Computer;
            return new Player(i_SettingsFormPlayerTwoName, secondPlayerType, eCheckersPieceType.OPiece,
                eCheckersPieceType.OKingPiece);
        }

        private void GameForm_PlayComputerMove()
        {
            CheckersBoardMove checkersBoardMove = getComputerMove();
            m_CheckersGame.playMove(checkersBoardMove);
        }

        private CheckersBoardMove getComputerMove()
        {
            uint randomIndex = (uint)r_RandomGenerator.Next(m_CheckersGame.ValidMoves.Count);
            return m_CheckersGame.ValidMoves[(int)randomIndex];
        }
    }
}