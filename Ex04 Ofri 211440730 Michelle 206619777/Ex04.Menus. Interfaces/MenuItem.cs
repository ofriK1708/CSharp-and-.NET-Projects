using System;
using System.Collections.Generic;

namespace Ex04.Menus.Interfaces
{
    public class MenuItem
    {
        internal string Title { get;}
        private readonly IMenuItem m_Action;
        private readonly List<MenuItem> r_SubMenuItems = new List<MenuItem>();
        private readonly bool r_IsSubMenu;
        
        public MenuItem(string i_Name, IMenuItem i_Action)
        {
            Title = i_Name;
            m_Action = i_Action;
            r_IsSubMenu = false;
        }

        public MenuItem(string i_Name, List<MenuItem> i_Items)
        {
            Title = i_Name;
            r_SubMenuItems = i_Items;
            r_IsSubMenu = true;
        }

        public void Execute()
        {
            if (!r_IsSubMenu)
            {
                m_Action.Invoke();
            }
            else
            {
                Console.Clear();
                int userChoice = ConsoleUtils.ShowMenuAndGetUserChoice(Title, r_SubMenuItems);
                if (userChoice != 0)
                {
                    r_SubMenuItems[userChoice].Execute();
                }
            }
        }
    }
}