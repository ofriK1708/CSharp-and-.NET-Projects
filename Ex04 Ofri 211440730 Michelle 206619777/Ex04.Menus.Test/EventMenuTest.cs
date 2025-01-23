using System;
using System.Collections.Generic;
using Ex04.Menus.Events;

namespace Ex04.Menus.Test
{
    public class EventMenuTest
    {
        public MainMenu CreateMenu()
        {
            MainMenu mainMenu = new MainMenu("Delegates Main Menu");
            MenuItem showVersion = new MenuItem("Show Version", this.showVersion_Select);
            MenuItem countLowercaseLetters = new MenuItem("Count Lowercase Letters", this.countLowercaseLetters_Select);
            MenuItem lettersAndVersion = new MenuItem("Letters and Version", new List<MenuItem> { showVersion, countLowercaseLetters });
            MenuItem showCurrentTime = new MenuItem("Show Current Time", this.showCurrentTime_Select);
            MenuItem showCurrentDate = new MenuItem("Show Current Date", this.showCurrentDate_Select);
            MenuItem showCurrentDateOrTime = new MenuItem("Show Current Date/Time", new List<MenuItem> { showCurrentTime, showCurrentDate });
            mainMenu.AddMenuItems(new List<MenuItem> { lettersAndVersion, showCurrentDateOrTime });
            
            return mainMenu;
        }

        private void showVersion_Select()
        {
            Console.WriteLine("App Version: 25.1.4.5480");
        }

        private void countLowercaseLetters_Select()
        {
            uint count = 0;
            
            Console.WriteLine("Insert a sentence:");
            string userInput = Console.ReadLine();
            if(!string.IsNullOrWhiteSpace(userInput))
            {
                foreach(char letter in userInput)
                {
                    if(char.IsLower(letter))
                    {
                        count++;
                    }
                }
            }

            Console.WriteLine($"Lowercase letters: {count}");
        }

        private void showCurrentTime_Select()
        {
            Console.WriteLine("The time now is {0:HH:mm:ss}",DateTime.Now);
        }

        private void showCurrentDate_Select()
        {
            Console.WriteLine("Current date is {0:d}", DateTime.Now);
        }
        
    }
}