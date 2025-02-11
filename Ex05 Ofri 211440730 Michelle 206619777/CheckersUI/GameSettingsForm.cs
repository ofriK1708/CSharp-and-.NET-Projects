using System.Windows.Forms;
using System.Drawing;
using System.Net.NetworkInformation;
namespace CheckersUI
{
    public class GameSettingsForm : Form
    {
        private TextBox m_TextBoxPlayerTwoName = new TextBox();
        private TextBox m_TextBoxPlayerOneName = new TextBox();
        private CheckBox m_CheckBoxPlayerTwo =  new CheckBox();
        private Label m_LablePlayerOne = new Label();
        private Label m_LablePlayers = new Label();
        private Label m_LableBoardSize = new Label();
        private RadioButton m_RadioButton10X10 = new RadioButton();
        private RadioButton m_RadioButton8X8 = new RadioButton();
        private RadioButton m_RadioButton6X6 = new RadioButton();
        private Button m_ButtonDone = new Button();
        

        public GameSettingsForm() 
        {
            initializeForm();
            initializeComponent();
            ShowDialog();
        }

        private void initializeForm()
        {
            Text = "Game Settings";
            Size = new Size(300, 200);
            StartPosition = FormStartPosition.CenterScreen;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            ControlBox = false;
            ShowInTaskbar = false;
            TopMost = true;
            Visible = true;
        }

        private void initializeComponent()
        {
            initializeTextBoxesAndLabels();
            initializeButtonsAndCheckBoxes();
            addControls();
        }
    
        private void initializeTextBoxesAndLabels()
        {
            m_TextBoxPlayerOneName.Location = new System.Drawing.Point(100, 110);
            m_TextBoxPlayerOneName.MaxLength = 10;
            m_TextBoxPlayerOneName.Name = "textBoxPlayerOneName";
            m_TextBoxPlayerOneName.Size = new System.Drawing.Size(103, 20);
            //m_TextBoxPlayerOneName.TextChanged += new System.EventHandler(this.textBoxPlayerName_TextChanged);

            m_TextBoxPlayerTwoName.Enabled = false;
            m_TextBoxPlayerTwoName.Location = new System.Drawing.Point(100, 147);
            m_TextBoxPlayerTwoName.MaxLength = 10;
            m_TextBoxPlayerTwoName.Name = "textBoxPlayerTwoName";
            m_TextBoxPlayerTwoName.Size = new System.Drawing.Size(103, 20);
            m_TextBoxPlayerTwoName.Text = "[Computer]";
            //m_TextBoxPlayerTwoName.TextChanged += new System.EventHandler(this.textBoxPlayerName_TextChanged);

            m_LablePlayerOne.AutoSize = true;
            m_LablePlayerOne.Location = new System.Drawing.Point(18, 110);
            m_LablePlayerOne.Name = "lablePlayerOne";
            m_LablePlayerOne.Size = new System.Drawing.Size(48, 13);
            m_LablePlayerOne.Text = "Player 1:";

            m_LablePlayers.AutoSize = true;
            m_LablePlayers.Location = new System.Drawing.Point(9, 81);
            m_LablePlayers.Name = "lablePlayers";
            m_LablePlayers.Size = new System.Drawing.Size(44, 13);
            m_LablePlayers.Text = "Players:";

            m_LableBoardSize.AutoSize = true;
            m_LableBoardSize.Location = new System.Drawing.Point(9, 13);
            m_LableBoardSize.Name = "lableBoardSize";
            m_LableBoardSize.Size = new System.Drawing.Size(61, 13);
            m_LableBoardSize.Text = "Board Size:";
        }

        private void initializeButtonsAndCheckBoxes()
        {
            m_ButtonDone.Location = new Point(128, 191);
            m_ButtonDone.Name = "buttonDone";
            m_ButtonDone.Size = new Size(75, 23);
            m_ButtonDone.Text = "Done";

            m_CheckBoxPlayerTwo.AutoSize = true;
            m_CheckBoxPlayerTwo.Location = new Point(21, 147);
            m_CheckBoxPlayerTwo.Name = "checkBoxPlayerTwo";
            m_CheckBoxPlayerTwo.Size = new Size(67, 17);
            m_CheckBoxPlayerTwo.Text = "Player 2:";
            //m_CheckBoxPlayerTwo.CheckedChanged += new System.EventHandler(this.checkBoxPlayerTwo_CheckedChanged);

            m_RadioButton10X10.AutoSize = true;
            m_RadioButton10X10.Location = new Point(143, 41);
            m_RadioButton10X10.Name = "radioButton10X10";
            m_RadioButton10X10.Size = new Size(60, 17);
            m_RadioButton10X10.Text = "10 x 10";
            //m_RadioButton10X10.CheckedChanged += new System.EventHandler(this.radioButton_CheckedChanged);

            m_RadioButton8X8.AutoSize = true;
            m_RadioButton8X8.Location = new System.Drawing.Point(77, 41);
            m_RadioButton8X8.Name = "radioButton8X8";
            m_RadioButton8X8.Size = new System.Drawing.Size(48, 17);
            m_RadioButton8X8.Text = "8 x 8";
            //m_RadioButton8X8.CheckedChanged += new System.EventHandler(this.radioButton_CheckedChanged);
            
            m_RadioButton6X6.AutoSize = true;
            m_RadioButton6X6.BackColor = System.Drawing.SystemColors.Control;
            m_RadioButton6X6.Location = new System.Drawing.Point(12, 41);
            m_RadioButton6X6.Name = "radioButton6X6";
            m_RadioButton6X6.Size = new System.Drawing.Size(48, 17);
            m_RadioButton6X6.Text = "6 x 6";
            //m_RadioButton6X6.CheckedChanged += new System.EventHandler(this.radioButton_CheckedChanged);

        }

        private void addControls()
        {
            Controls.AddRange(new Control[] { m_TextBoxPlayerTwoName, m_TextBoxPlayerOneName, m_CheckBoxPlayerTwo, m_LablePlayerOne, m_LablePlayers, m_LableBoardSize, 
                m_RadioButton10X10, m_RadioButton8X8, m_RadioButton6X6, m_ButtonDone });
            AcceptButton = m_ButtonDone;
        }
    }
}