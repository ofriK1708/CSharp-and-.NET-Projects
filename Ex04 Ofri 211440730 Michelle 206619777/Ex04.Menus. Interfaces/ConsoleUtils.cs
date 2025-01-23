using System;
using System.Collections.Generic;

namespace Ex04.Menus.Interfaces
{
    public class ConsoleUtils
    {

        public static int ShowMenuAndGetUserChoice(string i_Title, List<MenuItem> i_MenuItems, bool i_IsMainMenu)
        {
            Console.WriteLine("** {0} **", i_Title);
            Console.WriteLine("-----------------------------------");
            printMenuItems(i_MenuItems);
            Console.WriteLine("0 - {0}", i_IsMainMenu ? "Exit" : "Back");
            int userChoice = getInputAndValidate(i_MenuItems.Count, i_IsMainMenu);
            
            return userChoice;
        }

        private static void printMenuItems(List<MenuItem> i_Menu)
        {
            for (int i = 0; i < i_Menu.Count; i++)
            {
                Console.WriteLine("{0} - {1}", i + 1, i_Menu[i].Title);
            }
        }
        
        private static int getInputAndValidate(int i_Count, bool i_MainMenu)
        {
            Console.WriteLine("Please enter your choice (1 - {0}) or 0 to {1}:", i_Count, i_MainMenu ? "exit" : "back");
            string userInput = Console.ReadLine();
            bool tryParse = int.TryParse(userInput, out int userChoice);

            while (!tryParse || userChoice < 0 || userChoice > i_Count)
            {
                Console.WriteLine("Invalid input. Please try again.");
                userInput = Console.ReadLine();
                tryParse = int.TryParse(userInput, out userChoice);
            }
            
            return userChoice;
        }
    }
}