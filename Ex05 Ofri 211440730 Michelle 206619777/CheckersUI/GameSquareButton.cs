using System.Drawing;
using System.Windows.Forms;
using CheckersLogic;

namespace CheckersUI
{
	public sealed class GameSquareButton : Button
	{
		public static readonly int sr_ButtonHeight = 50;
		public static readonly int sr_ButtonWidth = 50;
		public BoardPosition BoardPosition { get;}

		public GameSquareButton(BoardPosition i_BoardPosition)
		{
			Name = i_BoardPosition.ToString();
			Text = string.Empty;
			BoardPosition = i_BoardPosition;
			Height = sr_ButtonHeight;
			Width = sr_ButtonWidth;
			BackColor = Color.Gainsboro;
			Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Bold);
			FlatStyle = FlatStyle.Flat;
			FlatAppearance.BorderColor = Color.Lavender;
			FlatAppearance.BorderSize = 1;
		}
	}
}