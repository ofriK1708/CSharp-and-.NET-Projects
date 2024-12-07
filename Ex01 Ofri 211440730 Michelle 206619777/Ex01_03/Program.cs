using System;

namespace Ex01_03
{
    class Program
    {
        public static void Main()
        {
            int treeHeight = getAndValidateUserInput();
            Ex01_02.Program.printABCTree(treeHeight);
        }
        private static int getAndValidateUserInput()
        {
            Console.WriteLine("Hello! please enter the tree height (the height should be between 2 and 15)");
            string treeHeightFromUser = Console.ReadLine();
            int treeHeight;
            bool isValidNum = int.TryParse(treeHeightFromUser, out treeHeight);
            while (!isValidNum || treeHeight > 15 || treeHeight < 2)
            {
                Console.WriteLine("Invalid height (the height should be between 2 and 15)");
                treeHeightFromUser = Console.ReadLine();
                isValidNum = int.TryParse(treeHeightFromUser, out treeHeight);
            }

            return treeHeight;
        }
    }
}
