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
                    GameSquareButton currentSquare = new GameSquareButton(new BoardPosition(col, row));
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
            foreach (BoardPosition boardPosition in i_OPositions)
            {
                Controls[boardPosition.ToString()].Text = eCheckersPieceType.OPiece.ToString();
            }
            
            foreach (BoardPosition boardPosition in i_XPositions)
            {
                Controls[boardPosition.ToString()].Text = eCheckersPieceType.XPiece.ToString();
            }
        }

        private void gameWindowButton_ButtonClicked(object i_Sender, EventArgs i_E)
        {
        }
    }
}