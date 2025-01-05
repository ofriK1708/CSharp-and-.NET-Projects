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
        private Garage m_Garage = new Garage();
        private VehicleFactory m_VehicleFactory = new VehicleFactory();
       
        internal void Start()
        {
            m_ConsoleUtils.PrintStartMessage();
            while (!m_QuitGarage)
            {
                eMenuOption menuOptionFromUser = m_ConsoleUtils.GetMenuChoiceFromUser();
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
            string licensePlate = m_ConsoleUtils.GetLicensePlateFromUser();
            if (m_Garage.IsVehicleInTheGarage(licensePlate)){
                m_Garage.ChangeVehicleStatusToInRepare(licensePlate);
            }
            else
            {
                eVehicleType vehicleType = m_ConsoleUtils.GetVehicleTypeFromUser();
                CustomerInfo customerInfo = m_ConsoleUtils.GetCustomerInfoFromUser();
                string model = m_ConsoleUtils.GetModelFromUser();
                eEnergySourceType energySourceType = m_ConsoleUtils.GetEnergySourceTypeFromUser();
                Wheel[] wheels = m_ConsoleUtils.GetWheelsFromUser();
                float energyCapacity = m_ConsoleUtils.GetCurrentEnergyCapacityFromUser(energySourceType);

                m_Garage.AddVehicle(m_VehicleFactory.createCar());
            }
        }
    }
}
