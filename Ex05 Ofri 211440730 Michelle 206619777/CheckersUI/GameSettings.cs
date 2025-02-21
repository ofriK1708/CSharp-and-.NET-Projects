using System;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace CheckersUI
{
    internal partial class GameSettings : Form
    {
        private int m_PlayerSelectedWindowSize;

        public GameSettings()
        {
            InitializeComponent();
            ShowDialog();
        }

        public int PlayerSelectedWindowSize
        {
            get { return m_PlayerSelectedWindowSize; }
            private set { m_PlayerSelectedWindowSize = value; }
        }

        public bool IsPlayerTwoActive
        {
            get { return checkBoxPlayerTwo.Checked; }
        }

        public string PlayerOneName
        {
            get { return textBoxPlayerOneName.Text; }
            set { textBoxPlayerOneName.Text = value; }
        }

        public string PlayerTwoName
        {
            get { return textBoxPlayerTwoName.Text; }
            set { textBoxPlayerTwoName.Text = value; }
        }

        private void radioButton_CheckedChanged(object i_Sender, EventArgs i_EventArgs)
        {
            RadioButton radioButtom = i_Sender as RadioButton;
            if (i_Sender is RadioButton && radioButtom.Checked)
            {
                PlayerSelectedWindowSize = int.Parse(radioButtom.Tag.ToString());
            }
        }

        private void checkBoxPlayerTwo_CheckedChanged(object i_Sender, EventArgs i_EventArgs)
        {
            CheckBox checkBox = i_Sender as CheckBox;
            if (i_Sender is CheckBox && checkBox.Checked)
            {
                textBoxPlayerTwoName.Text = string.Empty;
                textBoxPlayerTwoName.Enabled = true;
            }
            else
            {
                textBoxPlayerTwoName.Text = "[Computer]";
                textBoxPlayerTwoName.Enabled = false;
            }
        }

        private void buttonDone_Click(object i_Sender, EventArgs i_EventArgs)
        {
            if (validateForm())
            {
                DialogResult = DialogResult.OK;
                Close();
            }
            else
            {
                MessageBox.Show("Please enter valid data!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void textBoxPlayerName_TextChanged(object i_Sender, EventArgs i_EventArgs)
        {
            validateName(i_Sender as TextBox);
        }

        private bool validateName(TextBox i_PlayerName)
        {
            Regex nameValidation = new Regex("^[A-Za-z]{1,10}$");
            bool isNameValid = nameValidation.IsMatch(i_PlayerName.Text) || i_PlayerName.Text == "[Computer]";
            if (!isNameValid)
            {
                errorProvider.SetError(i_PlayerName, "Please enter a valid name!");
            }
            else
            {
                errorProvider.SetError(i_PlayerName, string.Empty);
            }

            return isNameValid;
        }

        private bool validateForm()
        {
            return validateName(textBoxPlayerOneName) && validateName(textBoxPlayerTwoName);
        }

        private void gameSettings_FormClosed(object i_Sender, FormClosedEventArgs i_FormClosedEventArgs)
        {
            GameSettings settings = i_Sender as GameSettings;
            if (settings.DialogResult == DialogResult.Cancel)
            {
                checkBoxPlayerTwo.Checked = false;
                textBoxPlayerOneName.Text = "Player 1";
                radioButton6X6.Checked = true;
            }
        }
    }
}