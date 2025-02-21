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
        private int r_CheckersBoardSize;

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
            labelPlayerOneName.BackColor = Color.CornflowerBlue;
            labelPlayerTwoName.BackColor = Color.White;
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
                labelPlayerOneName.BackColor = Color.CornflowerBlue;
                labelPlayerTwoName.BackColor = Color.White;
            }
            else
            {
                labelPlayerOneName.BackColor = Color.White;
                labelPlayerTwoName.BackColor = Color.CornflowerBlue;
            }
        }

        private void gameWindowButton_ButtonClicked(object i_Sender, EventArgs i_E)
        {
        }
    }
}