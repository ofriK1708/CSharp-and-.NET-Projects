using System;
using System.Collections.Generic;

namespace Ex04.Menus.Interfaces
{
    public class MenuItem
    {
        internal string Title { get;}
        private readonly IMenuItem r_ExecutableMenuItem;
        private readonly List<MenuItem> r_SubMenuItems;
        private readonly bool r_IsSubMenu;
        
        public MenuItem(string i_Name, IMenuItem i_ExecutableMenuItem)
        {
            Title = i_Name;
            r_ExecutableMenuItem = i_ExecutableMenuItem;
            r_IsSubMenu = false;
        }

        public MenuItem(string i_Name, List<MenuItem> i_Items)
        {
            Title = i_Name;
            r_SubMenuItems = i_Items;
            r_IsSubMenu = true;
        }

        public void HandleChoice()
        {
            if (!r_IsSubMenu)
            {
                r_ExecutableMenuItem.Invoke();
            }
            else
            {
                Console.Clear();
                int userChoice = ConsoleUtils.ShowMenuAndGetUserChoice(Title, r_SubMenuItems, false);
                if (userChoice != 0)
                {
                    r_SubMenuItems[userChoice - 1].HandleChoice();
                }
            }
        }
    }
}