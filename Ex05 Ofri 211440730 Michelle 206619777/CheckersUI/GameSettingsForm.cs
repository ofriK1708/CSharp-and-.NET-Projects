﻿using System;
using System.Windows.Forms;

namespace CheckersUI
{
    internal partial class GameSettingsForm : Form
    {
        private const int k_MaxNameLength = 20;
        private const string k_ComputerPlayerName = "[Computer]";
        private const string k_InvalidDataMessage = "Please enter valid data!";
        private const string k_InvalidDataCaption = "Error";
        public int BoardSize { get; private set; }
        public bool IsPlayerTwoActive { get; private set; }
        public string PlayerOneName { get; private set; }
        public string PlayerTwoName { get; private set; }
        public bool IsWindowClosedByDone { get; private set; }

        public GameSettingsForm()
        {
            InitializeComponent();
            ShowDialog();
        }

        private void radioButton_CheckedChanged(object i_Sender, EventArgs i_EventArgs)
        {
            RadioButton radioButton = i_Sender as RadioButton;
            
            if (i_Sender is RadioButton && radioButton.Checked)
            {
                BoardSize = int.Parse(radioButton.Tag.ToString());
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
                textBoxPlayerTwoName.Text = k_ComputerPlayerName;
                textBoxPlayerTwoName.Enabled = false;
            }
        }

        private void buttonDone_Click(object i_Sender, EventArgs i_EventArgs)
        {
            if (validateForm())
            {
                DialogResult = DialogResult.OK;
                PlayerOneName = textBoxPlayerOneName.Text;
                PlayerTwoName = textBoxPlayerTwoName.Text;
                IsPlayerTwoActive = checkBoxPlayerTwo.Checked;
                IsWindowClosedByDone = true;
                Close();
            }
            else
            {
                MessageBox.Show(k_InvalidDataMessage, k_InvalidDataCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void textBoxPlayerName_TextChanged(object i_Sender, EventArgs i_EventArgs)
        {
            validateName(i_Sender as TextBox);
        }

        private bool validateName(TextBox i_PlayerName)
        {
            bool isNameValid =  (!i_PlayerName.Text.Contains(" ") && i_PlayerName.Text.Length <= k_MaxNameLength && !string.IsNullOrEmpty(i_PlayerName.Text));
            bool isNameUnique = textBoxPlayerTwoName.Enabled ? !textBoxPlayerOneName.Text.Equals(textBoxPlayerTwoName.Text) : !(i_PlayerName.Text.Equals(k_ComputerPlayerName));

            if (!isNameValid || !isNameUnique)
            {
                errorProvider.SetError(i_PlayerName, $"Please enter a valid name! no spaces,up to {k_MaxNameLength} characters and player1 and player2 cant have the same name");
            }
            else
            {
                errorProvider.SetError(i_PlayerName, string.Empty);
            }

            return isNameValid&isNameUnique;
        }

        private bool validateForm()
        {
            bool firstPlayerNameValid = validateName(textBoxPlayerOneName);
            bool secondPlayerNameValid = !checkBoxPlayerTwo.Checked || validateName(textBoxPlayerTwoName);
            
            return firstPlayerNameValid && secondPlayerNameValid;
        }

        private void gameSettings_FormClosed(object i_Sender, FormClosedEventArgs i_FormClosedEventArgs)
        {
            if (i_Sender is GameSettingsForm settingsForm)
            {
                if (settingsForm.DialogResult == DialogResult.Cancel)
                {
                    IsWindowClosedByDone = false;
                }
            }
        }
    }
}