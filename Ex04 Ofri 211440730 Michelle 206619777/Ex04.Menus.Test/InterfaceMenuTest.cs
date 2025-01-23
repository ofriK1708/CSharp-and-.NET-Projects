using System;
using System.Collections.Generic;
using Ex04.Menus.Interfaces;

namespace Ex04.Menus.Test
{
    public class InterfaceMenuTest
    {
        public MainMenu CreateMenu()
        {
            MainMenu mainMenu = new MainMenu("Interfaces Main Menu");
            MenuItem showVersion = new MenuItem("Show Version", new ShowVersion());
            MenuItem countLowercaseLetters = new MenuItem("Count Lowercase Letters",new CountLowercaseLetters());
            MenuItem lettersAndVersion = new MenuItem("Letters and Version", new List<MenuItem> { showVersion, countLowercaseLetters });
            MenuItem showCurrentTime = new MenuItem("Show Current Time", new ShowCurrentTime());
            MenuItem showCurrentDate = new MenuItem("Show Current Date", new ShowCurrentDate());
            MenuItem showCurrentDateOrTime = new MenuItem("Show Current Date/Time", new List<MenuItem> { showCurrentTime, showCurrentDate });
            mainMenu.AddMenuItems(new List<MenuItem> { lettersAndVersion, showCurrentDateOrTime });
            return mainMenu;
        }

        private class ShowVersion : IMenuItem
        {
            public void Invoke()
            {
                Console.WriteLine("App Version: 25.1.4.5480");
            }
        }

        private class CountLowercaseLetters : IMenuItem
        {
            public void Invoke()
            {
                uint count = 0;
            
                Console.WriteLine("Insert a sentence:");
                string userInput = Console.ReadLine();

                if (!string.IsNullOrWhiteSpace(userInput))
                {
                    foreach (char letter in userInput)
                    {
                        if (char.IsLower(letter))
                        {
                            count++;
                        }
                    }
                }

                Console.WriteLine($"Lowercase letters: {count}");
            }  
        }


        private class ShowCurrentTime : IMenuItem
        {
            public void Invoke()
            {
                Console.WriteLine("The time now is {0:HH:mm:ss}", DateTime.Now);
            }
        }

        private class ShowCurrentDate : IMenuItem
        {
            public void Invoke()
            {
                Console.WriteLine("Current date is {0:d}", DateTime.Now);
            }
        }
    }
}