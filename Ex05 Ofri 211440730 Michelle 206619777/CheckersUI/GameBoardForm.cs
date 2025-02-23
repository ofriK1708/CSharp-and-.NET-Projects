using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using CheckersLogic;

namespace CheckersUI
{
    public partial class GameBoardForm : Form
    {
        private const int k_ButtonStartX = 3;
        private const int k_ButtonStartY = 50;
        private const string k_ErrorCaption = "Error";
        private const string k_GamePausedMessage = "This round has finished. Do you wish to quit it and start a new round?";
        private const string k_GamePausedCaption = "Game Paused";
        private const string k_LabelNameSuffix = ":";
        private readonly int r_CheckersBoardSize;
        private readonly bool r_IsPlayerTwoActive;
        private List<BoardPosition> m_NextValidPositions = new List<BoardPosition>();
        private bool m_LastRoundQuitByPlayer;
        private GameSquareButton m_CurrentPressedButton;
        public event Func <BoardPosition,List<BoardPosition>> FirstPositionSelect;
        public event Action<CheckersBoardMove> SecondPositionSelect;
        public event Action NewGame;
        public event Action ComputerTurn;
        private const int k_ButtonClearTime = 300;
        private Timer m_ButtonSkippedTimer;
        private GameSquareButton m_SkippedButton;
        private Timer m_ButtonAddedTimer;
        private GameSquareButton m_AddedButton;
        private Timer m_ButtonRemovedTimer;
        private GameSquareButton m_RemovedButton;
        private readonly Color r_SelectedButtonColor = Color.LightBlue;
        private readonly Color r_UnselectedButtonColor = Color.Gainsboro;
        private readonly Color r_SkippedButtonColor = Color.IndianRed;
        private readonly Color r_ActivePlayerColor = Color.SkyBlue;
        private readonly Color r_MovingButtonColor = Color.Cornsilk;
        private readonly Color r_NextValidPositionsColor = Color.FromArgb(130, 50, 50, 50);
        private bool m_IsComputerTurn;
        private Panel m_PanelBoard;
        public event Action GameRoundQuitByPlayer;

        public GameBoardForm(string i_Player1Name, string i_Player2Name, int i_BoardSize, bool i_IsPlayerTwoActive)
        {
            r_CheckersBoardSize = i_BoardSize;
            InitializeComponent();
            initalisePlayersLabelNamesAndPositions(i_Player1Name, i_Player2Name);
            r_IsPlayerTwoActive = i_IsPlayerTwoActive;
            initializePanelBoard();
            createButtonMatrix();
            initTimersForClearingButtons();
        }

        private void initalisePlayersLabelNamesAndPositions(string i_Player1Name, string i_Player2Name)
        {
            labelPlayerOneName.Text = i_Player1Name + k_LabelNameSuffix;
            labelPlayerTwoName.Text = i_Player2Name + k_LabelNameSuffix;
            labelPlayerOneScore.Left = labelPlayerOneName.Right + 5;
            labelPlayerTwoScore.Left = labelPlayerTwoName.Right + 5;
        }

        private void initTimersForClearingButtons()
        {
            m_ButtonSkippedTimer = new Timer();
            m_ButtonSkippedTimer.Interval = k_ButtonClearTime;
            m_ButtonSkippedTimer.Tick += Timer_ClearSkippedButton;
            m_ButtonAddedTimer = new Timer();
            m_ButtonAddedTimer.Interval = k_ButtonClearTime;
            m_ButtonAddedTimer.Tick += Timer_ClearAddedButtonColor;
            m_ButtonRemovedTimer = new Timer();
            m_ButtonRemovedTimer.Interval = k_ButtonClearTime / 2;
            m_ButtonRemovedTimer.Tick += Timer_ClearRemovedButtonColor;
        }

        private void initializePanelBoard()
        {
            m_PanelBoard = new Panel();
            m_PanelBoard.Location = new Point(k_ButtonStartX, k_ButtonStartY);
            m_PanelBoard.Size = new Size(r_CheckersBoardSize * GameSquareButton.sr_ButtonWidth,
                r_CheckersBoardSize * GameSquareButton.sr_ButtonHeight);
            Controls.Add(m_PanelBoard);
        }

        private void createButtonMatrix()
        {
            for (int row = 0; row < r_CheckersBoardSize; row++)
            {
                for (int col = 0; col < r_CheckersBoardSize; col++)
                {
                    GameSquareButton currentSquare = new GameSquareButton(new BoardPosition(row, col));
                    if (row % 2 == col % 2)
                    {
                        currentSquare.Enabled = false;
                        currentSquare.BackColor = Color.SlateGray;
                    }

                    currentSquare.Top = row * currentSquare.Height;
                    currentSquare.Left = col * currentSquare.Width;
                    currentSquare.Click += gameWindowButton_ButtonClicked;
                    m_PanelBoard.Controls.Add(currentSquare);
                }
            }
        }

        public void Game_BoardReset(List<BoardPosition> i_XPositions, List<BoardPosition> i_OPositions)
        {
            foreach (Control control in m_PanelBoard.Controls)
            {
                control.Text = string.Empty;
            }
            resetBoardForPieceType(i_OPositions, ((char)eCheckersPieceType.OPiece).ToString());
            resetBoardForPieceType(i_XPositions, ((char)eCheckersPieceType.XPiece).ToString());
            labelPlayerOneName.BackColor = r_ActivePlayerColor;
            m_IsComputerTurn = false;
            m_PanelBoard.Enabled = true;
            labelPlayerTwoName.BackColor = Color.Empty;
            
            if (m_CurrentPressedButton != null)
            {
                m_CurrentPressedButton.BackColor = r_UnselectedButtonColor;

                foreach (BoardPosition position in m_NextValidPositions)
                {
                    m_PanelBoard.Controls[position.ToString()].BackColor = r_UnselectedButtonColor;
                }
                m_CurrentPressedButton = null;
                m_NextValidPositions.Clear();
            }
        }

        private void resetBoardForPieceType(List<BoardPosition> i_Positions, string i_PieceText)
        {
            foreach (BoardPosition boardPosition in i_Positions)
            {
                m_PanelBoard.Controls[boardPosition.ToString()].Text = i_PieceText;
            }
        }

        public void Game_ActivePlayerChanged(Player i_ActivePlayer)
        {
            if (labelPlayerOneName.BackColor == r_ActivePlayerColor)
            {
                labelPlayerTwoName.BackColor = r_ActivePlayerColor;
                labelPlayerOneName.BackColor = Color.Empty;
                
                if (!r_IsPlayerTwoActive)
                {
                    m_IsComputerTurn = true;
                    m_PanelBoard.Enabled = false;
                }
            }
            else
            {
                labelPlayerOneName.BackColor = r_ActivePlayerColor;
                labelPlayerTwoName.BackColor = Color.Empty;
                m_IsComputerTurn = false;
                m_PanelBoard.Enabled = true;
            }
        }

        public void Game_PieceRemoved(BoardPosition i_Position, bool i_IsSkipped)
        {
            if (i_IsSkipped)
            {
                m_ButtonSkippedTimer.Start();
                m_PanelBoard.Controls[i_Position.ToString()].BackColor = r_SkippedButtonColor;
                m_SkippedButton = m_PanelBoard.Controls[i_Position.ToString()] as GameSquareButton;
            }
            else
            {
                m_ButtonRemovedTimer.Start();
                m_PanelBoard.Controls[i_Position.ToString()].BackColor = r_MovingButtonColor;
                m_RemovedButton = m_PanelBoard.Controls[i_Position.ToString()] as GameSquareButton;
            }
        }

        private void Timer_ClearSkippedButton(object i_Sender, EventArgs i_EventArgs)
        {
            m_ButtonSkippedTimer.Stop();
            m_PanelBoard.Controls[m_SkippedButton.BoardPosition.ToString()].Text = string.Empty;
            m_PanelBoard.Controls[m_SkippedButton.BoardPosition.ToString()].BackColor = r_UnselectedButtonColor;
            m_SkippedButton = null;
        }

        private void Timer_ClearAddedButtonColor(object i_Sender, EventArgs i_EventArgs)
        {
            m_ButtonAddedTimer.Stop();
            m_PanelBoard.Controls[m_AddedButton.BoardPosition.ToString()].BackColor = r_UnselectedButtonColor;
            m_AddedButton = null;
        }

        private void Timer_ClearRemovedButtonColor(object i_Sender, EventArgs i_EventArgs)
        {
            m_ButtonRemovedTimer.Stop();
            m_PanelBoard.Controls[m_RemovedButton.BoardPosition.ToString()].Text = string.Empty;
            m_PanelBoard.Controls[m_RemovedButton.BoardPosition.ToString()].BackColor = r_UnselectedButtonColor;
            m_RemovedButton = null;
        }

        public void Game_PieceAdded(BoardPosition i_Position, eCheckersPieceType i_PieceType)
        {
            m_ButtonAddedTimer.Start();
            m_PanelBoard.Controls[i_Position.ToString()].Text = ((char)i_PieceType).ToString();
            m_PanelBoard.Controls[i_Position.ToString()].BackColor = r_MovingButtonColor;
            m_AddedButton = m_PanelBoard.Controls[i_Position.ToString()] as GameSquareButton;
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
            else if (button?.Text == string.Empty)
            {
                CheckersBoardMove move = new CheckersBoardMove(m_CurrentPressedButton.BoardPosition, button.BoardPosition);
                OnSecondPositionSelected(move);
                changeButtonColor(m_CurrentPressedButton);
                m_CurrentPressedButton = null;
            }
            else
            {
                reselectFirstPosition(button);
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

        private void reselectFirstPosition(GameSquareButton i_Button)
        {
            undoPositionSelection(m_CurrentPressedButton);
            selectFirstPosition(i_Button);
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
            m_NextValidPositions = FirstPositionSelect?.Invoke(i_Button.BoardPosition);
        }

        private void changeButtonColor(GameSquareButton i_Button)
        {
            if (i_Button != null)
            {
                if (i_Button.BackColor == r_UnselectedButtonColor)
                {

                    foreach (BoardPosition position in m_NextValidPositions)
                    {
                        m_PanelBoard.Controls[position.ToString()].BackColor = r_NextValidPositionsColor;
                    }

                    i_Button.BackColor = r_SelectedButtonColor;
                }
                else
                {
                    if (i_Button.BackColor == r_SelectedButtonColor)
                    {
                        i_Button.BackColor = r_UnselectedButtonColor;
                    }

                    foreach (BoardPosition position in m_NextValidPositions)
                    {
                        m_PanelBoard.Controls[position.ToString()].BackColor = r_UnselectedButtonColor;
                    }
                }
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
            if(m_LastRoundQuitByPlayer || MessageBox.Show(
                   $@"{i_Winner.Name} Won! {Environment.NewLine}Another Round?",
                   @"Game Over",
                   MessageBoxButtons.YesNo,MessageBoxIcon.Question) == DialogResult.Yes)
            {
                m_LastRoundQuitByPlayer = false;
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
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            
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
            if (m_IsComputerTurn)
            {
                labelPlayerTwoName.BackColor = Color.Lavender;
                labelPlayerTwoName.Cursor = Cursors.Hand;
            }
        }

        private void PlayerTwo_MouseLeave(object i_Sender, EventArgs i_EventArgs)
        {
            if (m_IsComputerTurn)
            {
                labelPlayerTwoName.BackColor = r_ActivePlayerColor;
                labelPlayerTwoName.Cursor = Cursors.Default;
            }
        }

        private void PlayerTwo_Clicked(object i_Sender, EventArgs i_EventArgs)
        {
            if (m_IsComputerTurn)
            {
                ComputerTurn?.Invoke();
            }
        }
        
        protected override void OnClosing(CancelEventArgs i_EventArgs)
        {
            DialogResult userChoice = MessageBox.Show(k_GamePausedMessage,k_GamePausedCaption ,MessageBoxButtons.YesNoCancel,MessageBoxIcon.Question); 
            
            switch(userChoice)
            {
                case DialogResult.Yes:
                    m_LastRoundQuitByPlayer = true;
                    i_EventArgs.Cancel = true;
                    GameRoundQuitByPlayer?.Invoke();
                    break;
                case DialogResult.No:
                    break;
                case DialogResult.Cancel:
                    i_EventArgs.Cancel = true;
                    break;
            }
        }
    }
}