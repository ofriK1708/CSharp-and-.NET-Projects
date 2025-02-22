using System.Drawing;
using System.Windows.Forms;
using CheckersLogic;

namespace CheckersUI
{
	public class GameSquareButton : Button
	{
		private const int k_ButtonHeight = 50;
		private const int k_ButtonWidth = 50;
		public BoardPosition BoardPosition { get;}

		public GameSquareButton(BoardPosition i_BoardPosition)
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