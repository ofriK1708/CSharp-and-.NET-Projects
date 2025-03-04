using System;
using System.Collections.Generic;

namespace Ex04.Menus.Events
{
    public class MainMenu
    {
        private readonly List<MenuItem> r_SubMenuItems = new List<MenuItem>();
        private string Title { get; }
        private const string k_LastOptionTitle = "Exit";

        public MainMenu(string i_Title)
        {
            Title = i_Title;
        }

        public void AddMenuItem(MenuItem i_Item)
        {
            r_SubMenuItems.Add(i_Item);
        }

        public void AddMenuItems(List<MenuItem> i_Items)
        {
            r_SubMenuItems.AddRange(i_Items);
        }

        public void Show()
        {
            int userChoice = ConsoleUtils.ShowMenuAndGetUserChoice(Title, r_SubMenuItems, k_LastOptionTitle);

            while (userChoice != 0)
            {
                Console.Clear();
                r_SubMenuItems[userChoice - 1].HandleSelection();
                Console.Clear();
                userChoice = ConsoleUtils.ShowMenuAndGetUserChoice(Title, r_SubMenuItems, k_LastOptionTitle);
            }

            Console.Clear();
        }
    }
}