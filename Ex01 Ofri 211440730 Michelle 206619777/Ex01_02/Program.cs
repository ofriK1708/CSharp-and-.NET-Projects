using System;
using System.Text;
namespace Ex01_02
{
    public class Program
    {
        public static void Main()
        {
            printABCTree(14);
        }

        public static void printABCTree(int i_TreeHeight = 7)
        {
            printABCTreeHelper(i_TreeHeight, 1, 'A');
        }

        private static void printABCTreeHelper(int i_TreeHeight, int i_LineNumber, char i_NextCharInLine)
        {
            if (i_LineNumber > i_TreeHeight) 
            {
                return;
            }

            if(i_LineNumber >= i_TreeHeight - 1)
            {
                int spacesInLine = i_TreeHeight >= 10 ? i_TreeHeight - 3 : i_TreeHeight  - 2;
                StringBuilder lineToPrint = new StringBuilder();

                lineToPrint.Append(i_LineNumber);
                lineToPrint.Append(' ', spacesInLine);
                lineToPrint.Append(String.Format("|{0}|", i_NextCharInLine));
                Console.WriteLine(lineToPrint);
                printABCTreeHelper(i_TreeHeight, i_LineNumber + 1, i_NextCharInLine);
            }
            
            else
            {
                int charsInLine = (i_LineNumber * 2) - 1;
                int spacesInLine = i_TreeHeight - i_LineNumber;
                spacesInLine -= i_LineNumber >= 10 ? 1 : 0;
                StringBuilder lineToPrint = new StringBuilder();

                lineToPrint.Append(i_LineNumber);
                lineToPrint.Append(' ', spacesInLine);
                for (int i =0; i <charsInLine; i++)
                {
                    if(i_NextCharInLine > 'Z')
                    {
                        i_NextCharInLine = 'A';
                    }
                    lineToPrint.Append(i_NextCharInLine);
                    i_NextCharInLine++;
                }

                Console.WriteLine(lineToPrint);
                printABCTreeHelper(i_TreeHeight, i_LineNumber + 1, i_NextCharInLine++);
            }
        }
    }
}
