using System;
using System.Collections.Generic;

namespace Ex04.Menus.Events
{
    public class MainMenu
    {
        private readonly List<MenuItem> r_SubMenuItems = new List<MenuItem>();
        private string Title { get; }

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
            Console.Clear();
            int userChoice = ConsoleUtils.ShowMenuAndGetUserChoice(Title, r_SubMenuItems, true);
            if (userChoice != 0)
            {
                r_SubMenuItems[userChoice - 1].HandleSelection();
            }
        }
    }
}