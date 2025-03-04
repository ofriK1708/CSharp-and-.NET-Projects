namespace CheckersUI
{
    partial class GameSettingsForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }

            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.buttonDone = new System.Windows.Forms.Button();
            this.textBoxPlayerTwoName = new System.Windows.Forms.TextBox();
            this.checkBoxPlayerTwo = new System.Windows.Forms.CheckBox();
            this.textBoxPlayerOneName = new System.Windows.Forms.TextBox();
            this.labelPlayerOne = new System.Windows.Forms.Label();
            this.labelPlayers = new System.Windows.Forms.Label();
            this.labelBoardSize = new System.Windows.Forms.Label();
            this.radioButton10X10 = new System.Windows.Forms.RadioButton();
            this.radioButton8X8 = new System.Windows.Forms.RadioButton();
            this.radioButton6X6 = new System.Windows.Forms.RadioButton();
            this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonDone
            // 
            this.buttonDone.Location = new System.Drawing.Point(128, 191);
            this.buttonDone.Name = "buttonDone";
            this.buttonDone.Size = new System.Drawing.Size(75, 23);
            this.buttonDone.TabIndex = 19;
            this.buttonDone.Text = "Done";
            this.buttonDone.UseVisualStyleBackColor = true;
            this.buttonDone.Click += new System.EventHandler(this.buttonDone_Click);
            // 
            // textBoxPlayerTwoName
            // 
            this.textBoxPlayerTwoName.Enabled = false;
            this.textBoxPlayerTwoName.Location = new System.Drawing.Point(100, 147);
            this.textBoxPlayerTwoName.MaxLength = 10;
            this.textBoxPlayerTwoName.Name = "textBoxPlayerTwoName";
            this.textBoxPlayerTwoName.Size = new System.Drawing.Size(103, 20);
            this.textBoxPlayerTwoName.TabIndex = 18;
            this.textBoxPlayerTwoName.Text = "[Computer]";
            this.textBoxPlayerTwoName.TextChanged += new System.EventHandler(this.textBoxPlayerName_TextChanged);
            // 
            // checkBoxPlayerTwo
            // 
            this.checkBoxPlayerTwo.AutoSize = true;
            this.checkBoxPlayerTwo.Location = new System.Drawing.Point(21, 147);
            this.checkBoxPlayerTwo.Name = "checkBoxPlayerTwo";
            this.checkBoxPlayerTwo.Size = new System.Drawing.Size(67, 17);
            this.checkBoxPlayerTwo.TabIndex = 17;
            this.checkBoxPlayerTwo.Text = "Player 2:";
            this.checkBoxPlayerTwo.UseVisualStyleBackColor = true;
            this.checkBoxPlayerTwo.CheckedChanged += new System.EventHandler(this.checkBoxPlayerTwo_CheckedChanged);
            // 
            // textBoxPlayerOneName
            // 
            this.textBoxPlayerOneName.Location = new System.Drawing.Point(100, 110);
            this.textBoxPlayerOneName.MaxLength = 10;
            this.textBoxPlayerOneName.Name = "textBoxPlayerOneName";
            this.textBoxPlayerOneName.Size = new System.Drawing.Size(103, 20);
            this.textBoxPlayerOneName.TabIndex = 16;
            this.textBoxPlayerOneName.TextChanged += new System.EventHandler(this.textBoxPlayerName_TextChanged);
            // 
            // labelPlayerOne
            // 
            this.labelPlayerOne.AutoSize = true;
            this.labelPlayerOne.Location = new System.Drawing.Point(18, 110);
            this.labelPlayerOne.Name = "labelPlayerOne";
            this.labelPlayerOne.Size = new System.Drawing.Size(48, 13);
            this.labelPlayerOne.TabIndex = 15;
            this.labelPlayerOne.Text = "Player 1:";
            // 
            // labelPlayers
            // 
            this.labelPlayers.AutoSize = true;
            this.labelPlayers.Location = new System.Drawing.Point(9, 81);
            this.labelPlayers.Name = "labelPlayers";
            this.labelPlayers.Size = new System.Drawing.Size(44, 13);
            this.labelPlayers.TabIndex = 14;
            this.labelPlayers.Text = "Players:";
            // 
            // labelBoardSize
            // 
            this.labelBoardSize.AutoSize = true;
            this.labelBoardSize.Location = new System.Drawing.Point(9, 13);
            this.labelBoardSize.Name = "labelBoardSize";
            this.labelBoardSize.Size = new System.Drawing.Size(61, 13);
            this.labelBoardSize.TabIndex = 13;
            this.labelBoardSize.Text = "Board Size:";
            // 
            // radioButton10X10
            // 
            this.radioButton10X10.AutoSize = true;
            this.radioButton10X10.Location = new System.Drawing.Point(143, 41);
            this.radioButton10X10.Name = "radioButton10X10";
            this.radioButton10X10.Size = new System.Drawing.Size(60, 17);
            this.radioButton10X10.TabIndex = 12;
            this.radioButton10X10.TabStop = true;
            this.radioButton10X10.Tag = "10";
            this.radioButton10X10.Text = "10 x 10";
            this.radioButton10X10.UseVisualStyleBackColor = true;
            this.radioButton10X10.CheckedChanged += new System.EventHandler(this.radioButton_CheckedChanged);
            // 
            // radioButton8X8
            // 
            this.radioButton8X8.AutoSize = true;
            this.radioButton8X8.Location = new System.Drawing.Point(77, 41);
            this.radioButton8X8.Name = "radioButton8X8";
            this.radioButton8X8.Size = new System.Drawing.Size(48, 17);
            this.radioButton8X8.TabIndex = 11;
            this.radioButton8X8.TabStop = true;
            this.radioButton8X8.Tag = "8";
            this.radioButton8X8.Text = "8 x 8";
            this.radioButton8X8.UseVisualStyleBackColor = true;
            this.radioButton8X8.CheckedChanged += new System.EventHandler(this.radioButton_CheckedChanged);
            // 
            // radioButton6X6
            // 
            this.radioButton6X6.AutoSize = true;
            this.radioButton6X6.BackColor = System.Drawing.SystemColors.Control;
            this.radioButton6X6.Location = new System.Drawing.Point(12, 41);
            this.radioButton6X6.Name = "radioButton6X6";
            this.radioButton6X6.Size = new System.Drawing.Size(48, 17);
            this.radioButton6X6.TabIndex = 10;
            this.radioButton6X6.TabStop = true;
            this.radioButton6X6.Tag = "6";
            this.radioButton6X6.Text = "6 x 6";
            this.radioButton6X6.UseVisualStyleBackColor = false;
            this.radioButton6X6.CheckedChanged += new System.EventHandler(this.radioButton_CheckedChanged);
            // 
            // errorProvider
            // 
            this.errorProvider.ContainerControl = this;
            // 
            // GameSettings
            // 
            this.AcceptButton = this.buttonDone;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(221, 234);
            this.Controls.Add(this.buttonDone);
            this.Controls.Add(this.textBoxPlayerTwoName);
            this.Controls.Add(this.checkBoxPlayerTwo);
            this.Controls.Add(this.textBoxPlayerOneName);
            this.Controls.Add(this.labelPlayerOne);
            this.Controls.Add(this.labelPlayers);
            this.Controls.Add(this.labelBoardSize);
            this.Controls.Add(this.radioButton10X10);
            this.Controls.Add(this.radioButton8X8);
            this.Controls.Add(this.radioButton6X6);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "GameSettingsForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Settings";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.gameSettings_FormClosed);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonDone;
        private System.Windows.Forms.TextBox textBoxPlayerTwoName;
        private System.Windows.Forms.CheckBox checkBoxPlayerTwo;
        private System.Windows.Forms.TextBox textBoxPlayerOneName;
        private System.Windows.Forms.Label labelPlayerOne;
        private System.Windows.Forms.Label labelPlayers;
        private System.Windows.Forms.Label labelBoardSize;
        private System.Windows.Forms.RadioButton radioButton10X10;
        private System.Windows.Forms.RadioButton radioButton8X8;
        private System.Windows.Forms.RadioButton radioButton6X6;
        private System.Windows.Forms.ErrorProvider errorProvider;
    }
}