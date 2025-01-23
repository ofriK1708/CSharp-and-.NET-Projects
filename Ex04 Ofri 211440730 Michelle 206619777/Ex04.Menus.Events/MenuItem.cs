using System;
using System.Collections.Generic;

namespace Ex04.Menus.Events
{
    public class MenuItem
    {
        internal string Title { get;}
        public event Action ExcutableMethod;
        private readonly List<MenuItem> r_SubMenuItems;
        private readonly bool r_IsSubMenu;
        
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
            if (!r_IsSubMenu)
            {
              OnExecutableSelect();
            }
            else
            {
                Console.Clear();
                int userChoice = ConsoleUtils.ShowMenuAndGetUserChoice(Title, r_SubMenuItems, false);
                if (userChoice != 0)
                {
                    r_SubMenuItems[userChoice - 1].HandleSelection();
                }
            }
        }

        protected virtual void OnExecutableSelect()
        {
            ExcutableMethod?.Invoke();
        }
    }
}