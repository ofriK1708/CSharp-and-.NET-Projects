using System;
using System.Collections.Generic;

namespace CheckersLogic
{
    public class CheckersGame
    {
        private CheckersBoard GameBoard { get; }
        private Player Player1 { get;}
        private Player Player2 { get;}
        private Player ActivePlayer { get; set; }
        public List<CheckersBoardMove> ValidMoves { get; private set; } = new List<CheckersBoardMove>();
        private bool m_ContinueTurnForCurrentPlayer;
        public event Action<Player> ActivePlayerChanged;
        public event Action<Player> PlayerWon;
        public event Action Stalemate;

        public CheckersGame(Player i_Player1, Player i_Player2, int i_CheckersBoardSize)
        {
            Player1 = i_Player1;
            Player2 = i_Player2;
            GameBoard = new CheckersBoard(i_CheckersBoardSize);
        }

        public void AddBoardActionsListener(Action<List<BoardPosition>, List<BoardPosition>> i_BoardResetAction,
            Action<BoardPosition, eCheckersPieceType> i_PieceAddedAction,
            Action<BoardPosition> i_PieceRemovedAction)
        {
            GameBoard.BoardReset += i_BoardResetAction;
            GameBoard.PieceAdded += i_PieceAddedAction;
            GameBoard.PieceRemoved += i_PieceRemovedAction;
        }

        public void ResetGame()
        {
            GameBoard.resetBoard(GameBoard.Size);
            ActivePlayer = Player1;
            ValidMoves = getAllValidMoves(ActivePlayer);
            m_ContinueTurnForCurrentPlayer = false;
        }

        public void GameForm_NewGame()
        {
           ResetGame();
        }

        public void GameForm_FirstPositionSelected(BoardPosition i_SelectedPosition)
        {
            eCheckersPieceType eCheckersPieceType = GameBoard.GetPieceType(i_SelectedPosition);

            if (eCheckersPieceType != ActivePlayer.PieceType && eCheckersPieceType != ActivePlayer.KingPieceType)
            {
                throw new ArgumentException("Illegal selection!, please choose a valid position!");
            }
        }

        public void GameForm_SecondPositionSelected(CheckersBoardMove i_BoardMove)
        {
            eCheckersPieceType eCheckersPieceType = GameBoard.GetPieceType(i_BoardMove.To);
            if (eCheckersPieceType != eCheckersPieceType.EmptyPlace || !CheckMovePartOfValidMoves(i_BoardMove))
            {
                throw new ArgumentException("Illegal move!");
            }

            playMove(i_BoardMove);
        }

        private void playMove(CheckersBoardMove i_ValidMove)
        {
            bool skippedOpponentsPiece = GameBoard.executeMove(i_ValidMove);

            if (skippedOpponentsPiece)
            {
                ValidMoves = getValidSkipsFromPosition(i_ValidMove.To, ActivePlayer);
                m_ContinueTurnForCurrentPlayer = ValidMoves.Count > 0;
            }
            else
            {
                m_ContinueTurnForCurrentPlayer = false;
            }

            if (!m_ContinueTurnForCurrentPlayer)
            {
                if (checkIfPlayerWon(ActivePlayer))
                {
                    OnPlayerWon(ActivePlayer);
                }
                else
                {
                    handleGameStateBeforeNextMove();
                }
            }
        }

        private void handleGameStateBeforeNextMove()
        {
            switchActivePlayer();
            ValidMoves = getAllValidMoves(ActivePlayer);

            if (ValidMoves.Count > 0)
            {
                OnActivePlayerChanged();
            }
            else
            {
                Player opponent = getOpponent(ActivePlayer);
                List<CheckersBoardMove> opponentsValidMoves = getAllValidMoves(opponent);

                if (opponentsValidMoves.Count <= 0)
                {
                    OnStalemate();
                }
                else
                {
                    OnPlayerWon(opponent);
                }
            }
        }

        private void OnStalemate()
        {
           Stalemate?.Invoke();
        }

        private void switchActivePlayer()
        {
            if (ActivePlayer.Equals(Player1))
            {
                ActivePlayer = Player2;
            }
            else
            {
                ActivePlayer = Player1;
            }
        }

        private Player getOpponent(Player i_ActivePlayer)
        {
            Player opponent;

            if (i_ActivePlayer.Equals(Player1))
            {
                opponent = Player2;
            }
            else
            {
                opponent = Player1;
            }

            return opponent;
        }

        private void OnPlayerWon(Player i_Player)
        {
            uint addedScore = calcScore(i_Player.PieceType);
            i_Player.addToScore(addedScore);
            PlayerWon?.Invoke(i_Player);
        }
        public void GameForm_GameRoundQuitByPlayer()
        {
            switchActivePlayer();
            OnPlayerWon(ActivePlayer);
        }

        private bool checkIfPlayerWon(Player i_ActivePlayer)
        {
            bool isPlayerWon;
            eCheckersPieceType pieceType = i_ActivePlayer.PieceType;

            if (pieceType.Equals(eCheckersPieceType.XPiece) || pieceType.Equals(eCheckersPieceType.XKingPiece))
            {
                isPlayerWon = GameBoard.isAllPiecesRemoved(eCheckersPieceType.OPiece);
            }
            else
            {
                isPlayerWon = GameBoard.isAllPiecesRemoved(eCheckersPieceType.XPiece);
            }

            return isPlayerWon;
        }

        private void OnActivePlayerChanged()
        {
            ActivePlayerChanged?.Invoke(ActivePlayer);
        }

        public bool CheckMovePartOfValidMoves(CheckersBoardMove i_Move)
        {
            return ValidMoves.Contains(i_Move);
        }

        private uint calcScore(eCheckersPieceType i_CheckersBoardPiece)
        {
            List<BoardPosition> winningPositions;
            List<BoardPosition> loosingPositions;
            uint winnerScore = 0;
            uint looserScore = 0;

            if (i_CheckersBoardPiece.Equals(eCheckersPieceType.XPiece))
            {
                winningPositions = GameBoard.XPositions;
                loosingPositions = GameBoard.OPositions;
            }
            else
            {
                winningPositions = GameBoard.OPositions;
                loosingPositions = GameBoard.XPositions;
            }

            foreach (BoardPosition position in winningPositions)
            {
                if (GameBoard.IsPieceKing(position.Row, position.Column))
                {
                    winnerScore += 4;
                }
                else
                {
                    winnerScore++;
                }
            }

            foreach (BoardPosition position in loosingPositions)
            {
                if (GameBoard.IsPieceKing(position.Row, position.Column))
                {
                    looserScore += 4;
                }
                else
                {
                    looserScore++;
                }
            }

            return winnerScore >= looserScore ? winnerScore - looserScore : looserScore - winnerScore;
        }

        private List<CheckersBoardMove> getAllValidMoves(Player i_ActivePlayer)
        {
            List<CheckersBoardMove> validBoardMoves = new List<CheckersBoardMove>();
            List<BoardPosition> positionsToCheck = i_ActivePlayer.PieceType == eCheckersPieceType.XPiece
                ? GameBoard.XPositions
                : GameBoard.OPositions;
            eCheckersPieceType opponentPiece = i_ActivePlayer.PieceType == eCheckersPieceType.XPiece
                ? eCheckersPieceType.OPiece
                : eCheckersPieceType.XPiece;
            int directionToMoveInRow = i_ActivePlayer.PieceType == eCheckersPieceType.XPiece ? -1 : 1;

            foreach (BoardPosition position in positionsToCheck)
            {
                validBoardMoves.AddRange(getValidSkipsFromPosition(position, i_ActivePlayer));
            }

            if (validBoardMoves.Count == 0)
            {
                foreach (BoardPosition position in positionsToCheck)
                {
                    int newRow = position.Row + directionToMoveInRow;
                    int newColRight = position.Column + 1;
                    int newColLeft = position.Column - 1;
                    int newKingRow = position.Row - directionToMoveInRow;

                    if (GameBoard.IsCellInRange(newRow, newColRight) && GameBoard.IsCellEmpty(newRow, newColRight))
                    {
                        validBoardMoves.Add(new CheckersBoardMove(position, new BoardPosition(newRow, newColRight)));
                    }

                    if (GameBoard.IsCellInRange(newRow, newColLeft) && GameBoard.IsCellEmpty(newRow, newColLeft))
                    {
                        validBoardMoves.Add(new CheckersBoardMove(position, new BoardPosition(newRow, newColLeft)));
                    }

                    if (GameBoard.IsPieceKing(position.Row, position.Column))
                    {
                        if (GameBoard.IsCellInRange(newKingRow, newColRight) &&
                            GameBoard.IsCellEmpty(newKingRow, newColRight))
                        {
                            validBoardMoves.Add(new CheckersBoardMove(position,
                                new BoardPosition(newKingRow, newColRight)));
                        }

                        if (GameBoard.IsCellInRange(newKingRow, newColLeft) &&
                            GameBoard.IsCellEmpty(newKingRow, newColLeft))
                        {
                            validBoardMoves.Add(new CheckersBoardMove(position,
                                new BoardPosition(newKingRow, newColLeft)));
                        }
                    }
                }
            }

            return validBoardMoves;
        }

        private List<CheckersBoardMove> getValidSkipsFromPosition(BoardPosition i_Position, Player i_ActivePlayer)
        {
            List<CheckersBoardMove> validBoardPositions = new List<CheckersBoardMove>();
            eCheckersPieceType opponentPiece = i_ActivePlayer.PieceType == eCheckersPieceType.XPiece
                ? eCheckersPieceType.OPiece
                : eCheckersPieceType.XPiece;
            int directionToMoveInRow = i_ActivePlayer.PieceType == eCheckersPieceType.XPiece ? -1 : 1;
            int newRow = i_Position.Row + directionToMoveInRow;
            int newRowDouble = i_Position.Row + directionToMoveInRow * 2;
            int newColRight = i_Position.Column + 1;
            int newColLeft = i_Position.Column - 1;
            int newColRightDouble = i_Position.Column + 2;
            int newColLeftDouble = i_Position.Column - 2;
            int newKingRow = i_Position.Row - directionToMoveInRow;
            int newKingRowDouble = i_Position.Row - directionToMoveInRow * 2;

            if (GameBoard.IsCellInRange(newRowDouble, newColRightDouble) &&
                GameBoard.IsCellEmpty(newRowDouble, newColRightDouble) &&
                GameBoard.IsOpponentPiece(opponentPiece, newRow, newColRight))
            {
                validBoardPositions.Add(new CheckersBoardMove(i_Position,
                    new BoardPosition(newRowDouble, newColRightDouble)));
            }

            if (GameBoard.IsCellInRange(newRowDouble, newColLeftDouble) &&
                GameBoard.IsCellEmpty(newRowDouble, newColLeftDouble) &&
                GameBoard.IsOpponentPiece(opponentPiece, newRow, newColLeft))
            {
                validBoardPositions.Add(new CheckersBoardMove(i_Position,
                    new BoardPosition(newRowDouble, newColLeftDouble)));
            }

            if (GameBoard.IsPieceKing(i_Position.Row, i_Position.Column))
            {
                if (GameBoard.IsCellInRange(newKingRowDouble, newColRightDouble) &&
                    GameBoard.IsCellEmpty(newKingRowDouble, newColRightDouble) &&
                    GameBoard.IsOpponentPiece(opponentPiece, newKingRow, newColRight))
                {
                    validBoardPositions.Add(new CheckersBoardMove(i_Position,
                        new BoardPosition(newKingRowDouble, newColRightDouble)));
                }

                if (GameBoard.IsCellInRange(newKingRowDouble, newColLeftDouble) &&
                    GameBoard.IsCellEmpty(newKingRowDouble, newColLeftDouble) &&
                    GameBoard.IsOpponentPiece(opponentPiece, newKingRow, newColLeft))
                {
                    validBoardPositions.Add(new CheckersBoardMove(i_Position,
                        new BoardPosition(newKingRowDouble, newColLeftDouble)));
                }
            }

            return validBoardPositions;
        }
    }
}