using System;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
namespace Ex01_02
{
    class Program
    {
        public static void Main()
        {
            printABCTree();
        }

        public static void printABCTree(int i_treeHeight = 7)
        {
            printABCTreeHelper(i_treeHeight, 1, 'A');
        }

        private static void printABCTreeHelper(int i_treeHeight,int i_lineNumber,char i_nextCharInLine)
        {
            if (i_lineNumber > i_treeHeight) 
            {
                return;
            }

            if(i_lineNumber >= i_treeHeight - 1)
            {
                int spacesInLine = i_treeHeight - 2;
                StringBuilder lineToPrint = new StringBuilder();
                lineToPrint.Append(i_lineNumber);
                lineToPrint.Append(' ', spacesInLine);
                lineToPrint.Append(String.Format("|{0}|", i_nextCharInLine));
                Console.WriteLine(lineToPrint);
                printABCTreeHelper(i_treeHeight, i_lineNumber + 1, i_nextCharInLine);
            }
            
            else
            {
                int charsInLine = (i_lineNumber * 2) - 1;
                int spacesInLine = i_treeHeight - i_lineNumber;
                StringBuilder lineToPrint = new StringBuilder();
                lineToPrint.Append(i_lineNumber);
                lineToPrint.Append(' ', spacesInLine);
                for (int i =0; i <charsInLine; i++)
                {
                    if(i_nextCharInLine > 'Z')
                    {
                        i_nextCharInLine = 'A';
                    }
                    lineToPrint.Append(i_nextCharInLine);
                    i_nextCharInLine++;
                }

                Console.WriteLine(lineToPrint);
                printABCTreeHelper(i_treeHeight, i_lineNumber + 1, i_nextCharInLine++);
            }
        }
    }
}
