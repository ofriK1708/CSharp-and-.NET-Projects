using System.Collections.Generic;

namespace Ex04.Menus.Interfaces
{
    public class MainMenu
    {
        private readonly List<MenuItem> r_SubMenuItems = new List<MenuItem>();
        private string Title { get; }

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
            int userChoice = ConsoleUtils.ShowMenuAndGetUserChoice(Title, r_SubMenuItems);
            if (userChoice != 0)
            {
                r_SubMenuItems[userChoice].Execute();
            }
        }
    }
}