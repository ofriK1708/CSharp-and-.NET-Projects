using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using CheckersLogic;

namespace CheckersUI
{
    public partial class GameBoardForm : Form
    {
        private const int k_ButtonStartX = 3;
        private const int k_ButtonStartY = 50;
        private const String k_ErrorCaption = "Error";
        private int r_CheckersBoardSize;
        private GameSquareButton m_CurrentPressedButton;
        public event Action<BoardPosition> FirstPositionSelect;
        public event Action<CheckersBoardMove> SecondPositionSelect;

        public GameBoardForm(String i_Player1Name, String i_Player2Name, int i_BoardSize)
        {
            r_CheckersBoardSize = i_BoardSize;
            InitializeComponent();
            labelPlayerOneName.Text = i_Player1Name + ":";
            labelPlayerTwoName.Text = i_Player2Name + ":";
            labelPlayerOneScore.Left = labelPlayerOneName.Right + 5;
            labelPlayerTwoScore.Left = labelPlayerTwoName.Right + 5;
            createButtonMatrix();
        }

        private void createButtonMatrix()
        {
            for (int row = 0; row < r_CheckersBoardSize; row++)
            {
                for (int col = 0; col < r_CheckersBoardSize; col++)
                {
                    GameSquareButton currentSquare = new GameSquareButton(new BoardPosition(row, col));
                    if ((row % 2 == 0 && col % 2 == 0) || (row % 2 == 1 && col % 2 == 1))
                    {
                        currentSquare.Enabled = false;
                        currentSquare.BackColor = Color.Gray;
                    }

                    currentSquare.Top = (row * currentSquare.Height) + k_ButtonStartY;
                    currentSquare.Left = (col * currentSquare.Width) + k_ButtonStartX;
                    currentSquare.Click += gameWindowButton_ButtonClicked;
                    Controls.Add(currentSquare);
                }
            }
        }

        public void Game_BoardReset(List<BoardPosition> i_XPositions, List<BoardPosition> i_OPositions)
        {
            resetBoardForPieceType(i_OPositions, ((char)eCheckersPieceType.OPiece).ToString());
            resetBoardForPieceType(i_XPositions, ((char)eCheckersPieceType.XPiece).ToString());
            labelPlayerOneName.BackColor = Color.LightBlue;
            labelPlayerTwoName.BackColor = Color.Empty;
        }

        private void resetBoardForPieceType(List<BoardPosition> i_Positions, string i_PieceText)
        {
            foreach (BoardPosition boardPosition in i_Positions)
            {
                Controls[boardPosition.ToString()].Text = i_PieceText;
            }
        }

        public void Game_ActivePlayerChanged(Player i_ActivePlayer)
        {
            if (i_ActivePlayer.Name == labelPlayerOneName.Text)
            {
                labelPlayerOneName.BackColor = Color.LightBlue;
                labelPlayerTwoName.BackColor = Color.Empty;
            }
            else
            {
                labelPlayerOneName.BackColor = Color.Empty;
                labelPlayerTwoName.BackColor = Color.LightBlue;
            }
        }

        public void Game_PieceRemoved(BoardPosition i_Position)
        {
            Controls[i_Position.ToString()].Text = string.Empty;
        }
        
        public void Game_PieceAdded(BoardPosition i_Position, eCheckersPieceType i_PieceType)
        {
            Controls[i_Position.ToString()].Text = ((char)i_PieceType).ToString();
        }

        private void gameWindowButton_ButtonClicked(object i_Sender, EventArgs i_E)
        {
            GameSquareButton button = i_Sender as GameSquareButton;
            if (m_CurrentPressedButton == null)
            {
                // resetComputerActions();
                selectFirstPosition(button);
            }
            else if (m_CurrentPressedButton == button)
            {
                undoPositionSelection(button);
            }
            else
            {
                playeMove(button);
            }
        }

        private void playeMove(GameSquareButton i_Button)
        {
            try
            {
                CheckersBoardMove move = new CheckersBoardMove(m_CurrentPressedButton.BoardPosition, i_Button.BoardPosition);
                SecondPositionSelect?.Invoke(move);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, k_ErrorCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void undoPositionSelection(GameSquareButton i_Button)
        {
            changeButtonColor(i_Button);
            m_CurrentPressedButton = null;
        }

        private void selectFirstPosition(GameSquareButton i_Button)
        {
            try
            {
                FirstPositionSelect?.Invoke(i_Button.BoardPosition);
                m_CurrentPressedButton = i_Button;
                changeButtonColor(i_Button);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, k_ErrorCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void changeButtonColor(GameSquareButton i_Button)
        {
            if (i_Button.BackColor == Color.White)
            {
                i_Button.BackColor = Color.LightSkyBlue;
            }
            else if (i_Button.BackColor == Color.LightSkyBlue)
            {
                i_Button.BackColor = Color.White;
            }
        }
    }
}