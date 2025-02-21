
using System;
using System.Drawing;
using System.Windows.Forms;
using CheckersLogic;

namespace CheckersUI
{
    public partial class GameBoardForm : Form
    {
        public CheckersBoard CheckersBoard { get; set; }
        public Player Player1 { get; set; }
        public Player Player2 { get; set; }
        private const int k_ButtonStartX = 3;
        private const int k_ButtonStartY = 50;
        
        public GameBoardForm(CheckersBoard i_CheckersBoard, Player i_Player1, Player i_Player2)
        {
            CheckersBoard = i_CheckersBoard;
            Player1 = i_Player1;
            Player2 = i_Player2;
            InitializeComponent();
            labelPlayerOneName.Text = Player1.Name + ":";
            labelPlayerTwoName.Text = Player2.Name + ":";
            labelPlayerOneScore.Left = labelPlayerOneName.Right + 5;
            labelPlayerTwoScore.Left = labelPlayerTwoName.Right + 5;
            createButtonMatrix();
        }
        
        private void createButtonMatrix()
        {
            for (int row = 0; row < CheckersBoard.Size; row++)
            {
                for (int col = 0; col < CheckersBoard.Size; col++)
                {
                    GameWindowButton currentSquare = new GameWindowButton(new BoardPosition(col, row));
                    if ((row % 2 == 0 && col % 2 == 0) || (row % 2 == 1 && col % 2 == 1))
                    {
                        currentSquare.Enabled = false;
                        currentSquare.BackColor = Color.Gray;
                    }

                    currentSquare.Top = (row  * currentSquare.Height) + k_ButtonStartY;
                    currentSquare.Left = (col * currentSquare.Width)  + k_ButtonStartX;
                    currentSquare.Click += gameWindowButton_ButtonClicked;
                    Controls.Add(currentSquare);
                }
            }
        }
        
        private void gameWindowButton_ButtonClicked(object i_Sender, EventArgs i_E)
        {
        }
    }
}
