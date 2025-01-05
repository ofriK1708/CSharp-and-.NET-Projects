using ex03;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.ConsoleUI
{
    internal class GarageUI
    {

        private ConsoleUtils m_ConsoleUtils = new ConsoleUtils();
        private bool m_QuitGarage = false;
        internal void Start()
        {
            m_ConsoleUtils.PrintMainMenu();
            while (!m_QuitGarage)
            {
                try
                {
                    eMenuOption menuOptionFromUser = (eMenuOption)m_ConsoleUtils.GetEnumInputFromUser();
                    if (menuOptionFromUser.Equals(eMenuOption.Quit))
                    {
                        m_QuitGarage = true;
                        Console.WriteLine("Goodby :)");
                    }
                    else
                    {
                        handleUserChoice(menuOptionFromUser);
                    }
                }
                catch (FormatException ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.WriteLine("Please try again");
                }
            }

        }

        private void handleUserChoice(eMenuOption i_MenuOption)
        {
            switch (i_MenuOption)
            {
                case eMenuOption.AddVehicle:
                    handleAddVehicle();
                    break;
                case eMenuOption.LicencePlateList:
                    //
                    break;
                case eMenuOption.FillAirPressure:
                    //
                    break;
                case eMenuOption.ChangeVehicleStatus:
                    //
                    break;
                case eMenuOption.RefuelVehicle:
                    //
                    break;
                case eMenuOption.ChargeVehicle:
                    //
                    break;
                case eMenuOption.ShowFullVehicleStatus:
                    //
                    break;
            }
        }

        private void handleAddVehicle()
        {
            Console.WriteLine("Please enter licance plate");
            string licansePlate = m_ConsoleUtils.GetInputFromUser();

        }
    }
}
