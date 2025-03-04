
namespace Ex04.Menus.Test
{
    public static class Program
    {
        public static void Main()
        {
            EventMenuTest eventMenuTest = new EventMenuTest();
            eventMenuTest.CreateMenu().Show();
            InterfaceMenuTest interfaceMenuTest = new InterfaceMenuTest();
            interfaceMenuTest.CreateMenu().Show();
        }
    }
}