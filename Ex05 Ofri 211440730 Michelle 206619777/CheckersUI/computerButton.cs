using System.Drawing;
using System.Windows.Forms;
using CheckersLogic;

namespace CheckersUI
{
    public class ComputerButton : Button
    {
        public ComputerButton(int i_Height, int i_Width,int i_Left,int i_Top) 
        {
            Height = i_Height;
            Width = i_Width;
            Left = i_Left;
            Top = i_Top;
            Text = "testing1123";
            Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Bold);
            BackColor = Color.Teal;
            //FlatStyle = FlatStyle.Flat;
            //FlatAppearance.BorderSize = 0;
        }
    }
}

/*
 * public class GameSquareButton : Button
   {
   	public const int k_ButtonHeight = 50;
   	public const int k_ButtonWidth = 50;
   	public BoardPosition BoardPosition { get; set; }
   
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
 */