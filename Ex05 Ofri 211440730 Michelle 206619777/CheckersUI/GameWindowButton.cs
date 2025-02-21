using System.Drawing;
using System.Windows.Forms;
using CheckersLogic;

namespace CheckersUI
{
	public class GameWindowButton : Button
	{
		public const int k_ButtonHeight = 50;
		public const int k_ButtonWidth = 50;
		private BoardPosition BoardPosition { get; set; }

		public GameWindowButton(BoardPosition i_BoardPosition)
		{
			Name = i_BoardPosition.ToString();
			Text = string.Empty;
			BoardPosition = i_BoardPosition;
			Height = k_ButtonHeight;
			Width = k_ButtonWidth;
			BackColor = Color.White;
			Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Bold);
		}
	}
}