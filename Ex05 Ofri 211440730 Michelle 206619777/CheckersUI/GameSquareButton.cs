using System.Drawing;
using System.Windows.Forms;
using CheckersLogic;

namespace CheckersUI
{
	public class GameSquareButton : Button
	{
		public static int k_ButtonHeight = 50;
		public static int k_ButtonWidth = 50; // todo- rename static
		public BoardPosition BoardPosition { get;}

		public GameSquareButton(BoardPosition i_BoardPosition)
		{
			Name = i_BoardPosition.ToString();
			Text = string.Empty;
			BoardPosition = i_BoardPosition;
			Height = k_ButtonHeight;
			Width = k_ButtonWidth;
			BackColor = Color.Gainsboro;
			Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Bold);
			FlatStyle = FlatStyle.Flat;
			FlatAppearance.BorderColor = Color.Lavender;
			FlatAppearance.BorderSize = 1;
		}
	}
}