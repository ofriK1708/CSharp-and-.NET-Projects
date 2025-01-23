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
        private const bool v_MainMenu = true;
        
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
                showSubMenu();
            }
        }
        
        private void showSubMenu()
        {
            int userChoice = ConsoleUtils.ShowMenuAndGetUserChoice(Title, r_SubMenuItems, !v_MainMenu);
            while (userChoice != 0)
            {
                userChoice = ConsoleUtils.ShowMenuAndGetUserChoice(Title, r_SubMenuItems, !v_MainMenu);
                if (userChoice == 0)
                {
                    break;
                }
                r_SubMenuItems[userChoice - 1].HandleChoice();
            }
        }
    }
}