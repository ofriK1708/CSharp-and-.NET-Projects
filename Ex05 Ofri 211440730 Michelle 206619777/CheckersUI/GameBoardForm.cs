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
        private readonly bool r_IsPlayerTwoActive;
        private GameSquareButton m_CurrentPressedButton;
        public event Action<BoardPosition> FirstPositionSelect;
        public event Action<CheckersBoardMove> SecondPositionSelect;
        public event Action NewGame;
        public event Action ComputerTurn;
        private const int k_ButtonClearTime = 200;
        private Timer r_ButtonSkippedTimer;
        private GameSquareButton m_SkippedButton;
        private Timer r_ButtonAddedTimer;
        private GameSquareButton m_AddedButton;
        private Timer r_ButtonRemovedTimer;
        private GameSquareButton m_RemovedButton;
        private readonly Color r_SelectedButtonColor = Color.LightSkyBlue;
        private readonly Color r_UnselectedButtonColor = Color.White;
        private readonly Color r_SkippedButtonColor = Color.IndianRed;
        private readonly Color r_ActivePlayerColor = Color.LightBlue;
        private readonly Color r_MovingButtonColor = Color.PaleTurquoise;

        public GameBoardForm(string i_Player1Name, string i_Player2Name, int i_BoardSize, bool i_IsPlayerTwoActive)
        {
            r_CheckersBoardSize = i_BoardSize;
            InitializeComponent();
            labelPlayerOneName.Text = i_Player1Name + ":";
            labelPlayerTwoName.Text = i_Player2Name + ":";
            labelPlayerOneScore.Left = labelPlayerOneName.Right + 5;
            labelPlayerTwoScore.Left = labelPlayerTwoName.Right + 5;
            r_IsPlayerTwoActive = i_IsPlayerTwoActive;
            createButtonMatrix();
            initTimersForClearingButtons();
        }

        private void initTimersForClearingButtons()
        {
            r_ButtonSkippedTimer = new Timer();
            r_ButtonSkippedTimer.Interval = k_ButtonClearTime;
            r_ButtonSkippedTimer.Tick += Timer_ClearSkippedButton;
            r_ButtonAddedTimer = new Timer();
            r_ButtonAddedTimer.Interval = k_ButtonClearTime;
            r_ButtonAddedTimer.Tick += Timer_ClearAddedButtonColor;
            r_ButtonRemovedTimer = new Timer();
            r_ButtonRemovedTimer.Interval = k_ButtonClearTime;
            r_ButtonRemovedTimer.Tick += Timer_ClearRemovedButtonColor;
        }

        private void createButtonMatrix()
        {
            for (int row = 0; row < r_CheckersBoardSize; row++)
            {
                for (int col = 0; col < r_CheckersBoardSize; col++)
                {
                    GameSquareButton currentSquare = new GameSquareButton(new BoardPosition(row, col));
                    if (row % 2 == col % 2) // (row % 2 == 0 && col % 2 == 0) || (row % 2 == 1 && col % 2 == 1)
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
            labelPlayerOneName.BackColor = r_ActivePlayerColor;
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
            if (labelPlayerOneName.BackColor == r_ActivePlayerColor)
            {
                labelPlayerTwoName.BackColor = r_ActivePlayerColor;
                labelPlayerOneName.BackColor = Color.Empty;
            }
            else
            {
                labelPlayerOneName.BackColor = r_ActivePlayerColor;
                labelPlayerTwoName.BackColor = Color.Empty;
            }
        }

        public void Game_PieceRemoved(BoardPosition i_Position, bool i_IsSkipped)
        {
            if (i_IsSkipped)
            {
                r_ButtonSkippedTimer.Start();
                Controls[i_Position.ToString()].BackColor = r_SkippedButtonColor;
                m_SkippedButton = Controls[i_Position.ToString()] as GameSquareButton;
            }
            else
            {
                r_ButtonRemovedTimer.Start();
                Controls[i_Position.ToString()].BackColor = r_MovingButtonColor;
                m_RemovedButton = Controls[i_Position.ToString()] as GameSquareButton;
            }
        }

        private void Timer_ClearSkippedButton(object i_Sender, EventArgs i_EventArgs)
        {
            r_ButtonSkippedTimer.Stop();
            Controls[m_SkippedButton.BoardPosition.ToString()].Text = string.Empty;
            Controls[m_SkippedButton.BoardPosition.ToString()].BackColor = r_UnselectedButtonColor;
            m_SkippedButton = null;
        }

        private void Timer_ClearAddedButtonColor(object i_Sender, EventArgs i_EventArgs)
        {
            r_ButtonAddedTimer.Stop();
            Controls[m_AddedButton.BoardPosition.ToString()].BackColor = r_UnselectedButtonColor;
            m_AddedButton = null;
        }

        private void Timer_ClearRemovedButtonColor(object i_Sender, EventArgs i_EventArgs)
        {
            r_ButtonRemovedTimer.Stop();
            Controls[m_RemovedButton.BoardPosition.ToString()].Text = string.Empty;
            Controls[m_RemovedButton.BoardPosition.ToString()].BackColor = r_UnselectedButtonColor;
            m_RemovedButton = null;
        }

        public void Game_PieceAdded(BoardPosition i_Position, eCheckersPieceType i_PieceType)
        {
            r_ButtonAddedTimer.Start();
            Controls[i_Position.ToString()].Text = ((char)i_PieceType).ToString();
            Controls[i_Position.ToString()].BackColor = r_MovingButtonColor;
            m_AddedButton = Controls[i_Position.ToString()] as GameSquareButton;
        }

        private void gameWindowButton_ButtonClicked(object i_Sender, EventArgs i_E)
        {
            GameSquareButton button = i_Sender as GameSquareButton;
            if (m_CurrentPressedButton == null)
            {
                selectFirstPosition(button);
            }
            else if (m_CurrentPressedButton == button)
            {
                undoPositionSelection(button);
            }
            else
            {
                CheckersBoardMove move =
                    new CheckersBoardMove(m_CurrentPressedButton.BoardPosition, button.BoardPosition);
                OnSecondPositionSelected(move);
                // changeButtonColor(m_CurrentPressedButton);
                m_CurrentPressedButton = null;
            }
        }

        private void OnSecondPositionSelected(CheckersBoardMove i_Move)
        {
            try
            {
                SecondPositionSelect?.Invoke(i_Move);
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
                OnFirstPositionSelected(i_Button);
                m_CurrentPressedButton = i_Button;
                changeButtonColor(i_Button);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, k_ErrorCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void OnFirstPositionSelected(GameSquareButton i_Button)
        {
            FirstPositionSelect?.Invoke(i_Button.BoardPosition);
        }

        private void changeButtonColor(GameSquareButton i_Button)
        {
            if (i_Button.BackColor == r_UnselectedButtonColor)
            {
                i_Button.BackColor = r_SelectedButtonColor;
            }
            else if (i_Button.BackColor == r_SelectedButtonColor)
            {
                i_Button.BackColor = r_UnselectedButtonColor;
            }
        }

        private void changeScore(Player i_Player)
        {
            if (i_Player.PieceType == eCheckersPieceType.XPiece) // first player
            {
                labelPlayerOneScore.Text = i_Player.Score.ToString();
            }
            else
            {
                labelPlayerTwoScore.Text = i_Player.Score.ToString();
            }
        }

        public void Game_PlayerWon(Player i_Winner)
        {
            DialogResult result = MessageBox.Show($@"{i_Winner.Name} Won! {Environment.NewLine}Another Round?",
                @"Game Over", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                changeScore(i_Winner);
                OnNewGame();
            }
            else
            {
                Close();
            }
        }

        public void Game_Stalemate()
        {
            DialogResult result = MessageBox.Show($@"Tie! {Environment.NewLine}Another Round?", @"Game Over",
                MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                OnNewGame();
            }
            else
            {
                Close();
            }
        }

        private void OnNewGame()
        {
            NewGame?.Invoke();
        }

        private void PlayerTwo_MouseHover(object i_Sender, EventArgs i_EventArgs)
        {
            if (!r_IsPlayerTwoActive && labelPlayerTwoName.BackColor == r_ActivePlayerColor)
            {
                labelPlayerTwoName.BackColor = Color.CadetBlue;
            }
        }

        private void PlayerTwo_MouseLeave(object i_Sender, EventArgs i_EventArgs)
        {
            if (!r_IsPlayerTwoActive && labelPlayerOneName.BackColor == r_ActivePlayerColor)
            {
                labelPlayerTwoName.BackColor = Color.Empty;
            }
            else
            {
                labelPlayerTwoName.BackColor = r_ActivePlayerColor;
            }
        }

        private void PlayerTwo_Clicked(object i_Sender, EventArgs i_EventArgs)
        {
            if (!r_IsPlayerTwoActive && labelPlayerTwoName.BackColor != Color.Empty)
            {
                ComputerTurn?.Invoke();
            }
        }
    }
}