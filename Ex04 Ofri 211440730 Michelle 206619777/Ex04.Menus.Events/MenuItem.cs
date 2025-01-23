using System;
using System.Collections.Generic;

namespace Ex04.Menus.Events
{
    public class MenuItem
    {
        internal string Title { get; }

        public event Action ExcutableMethod;

        private readonly List<MenuItem> r_SubMenuItems;
        private readonly bool r_IsSubMenu;
        private const string k_LastOptionTitle = "Back";

        public MenuItem(string i_Name, Action i_Action)
        {
            Title = i_Name;
            ExcutableMethod = i_Action;
            r_IsSubMenu = false;
        }

        public MenuItem(string i_Name, List<MenuItem> i_Items)
        {
            Title = i_Name;
            r_SubMenuItems = i_Items;
            r_IsSubMenu = true;
        }

        public void HandleSelection()
        {
            if(!r_IsSubMenu)
            {
                OnExecutableSelect();
            }
            else
            {
                showSubMenu();
            }
        }

        private void showSubMenu()
        {
            int userChoice = ConsoleUtils.ShowMenuAndGetUserChoice(Title, r_SubMenuItems, k_LastOptionTitle);

            while(userChoice != 0)
            {
                r_SubMenuItems[userChoice - 1].HandleSelection();
                Console.WriteLine();
                userChoice = ConsoleUtils.ShowMenuAndGetUserChoice(Title, r_SubMenuItems, k_LastOptionTitle);
            }
        }

        protected virtual void OnExecutableSelect()
        {
            Console.WriteLine();
            ExcutableMethod?.Invoke();
        }
    }
}