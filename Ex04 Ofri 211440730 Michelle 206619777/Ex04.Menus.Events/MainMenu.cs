using System;
using System.Collections;
using System.Collections.Generic;

namespace Ex04.Menus.Events
{
    public class MainMenu
    {
        private readonly List<MenuItem> r_SubMenuItems = new List<MenuItem>();
        private string Title { get; }
        private const bool v_MainMenu = true;

        public MainMenu(string i_Title)
        {
            Title = i_Title;
        }

        public void AddMenuItem(MenuItem i_Item)
        {
            r_SubMenuItems.Add(i_Item);
        }

        public void AddRange(List<MenuItem> i_Items)
        {
            r_SubMenuItems.AddRange(i_Items);
        }

        public void Show()
        {
            int userChoice = ConsoleUtils.ShowMenuAndGetUserChoice(Title, r_SubMenuItems, v_MainMenu);
            while (userChoice != 0)
            {
                Console.Clear();
                userChoice = ConsoleUtils.ShowMenuAndGetUserChoice(Title, r_SubMenuItems, v_MainMenu);
                if (userChoice == 0)
                {
                    Console.WriteLine("Exiting...");
                    break;
                }
                r_SubMenuItems[userChoice - 1].HandleSelection();
            }
        }
    }
}