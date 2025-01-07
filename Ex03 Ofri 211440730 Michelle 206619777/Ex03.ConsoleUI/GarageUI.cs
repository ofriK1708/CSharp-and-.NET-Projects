using System;
using System.Collections.Generic;
using ex03;

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
            bool continueSameChoice = false;
            m_ConsoleUtils.PrintStartMessage();

            while (!m_QuitGarage)
            {
                eMenuOption menuOptionFromUser = m_ConsoleUtils.GetMenuChoiceFromUser();
                
                Console.Clear();
                if (menuOptionFromUser.Equals(eMenuOption.Quit))
                {
                    m_QuitGarage = true;
                    Console.WriteLine("Goodbye :)");
                    break;
                }

                try
                {
                    handleUserChoice(menuOptionFromUser);
                }
                catch (Exception e)
                {
                    Console.Clear();
                    Console.WriteLine(e.Message);
                    if (!m_ConsoleUtils.GetIfUserWantToTryAgain())
                    {
                        m_QuitGarage = true;
                        Console.WriteLine("Goodbye :)");
                    }
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

            if (m_Garage.IsVehicleInTheGarage(licensePlate))
            {
                m_Garage.ChangeVehicleStatusToInRepare(licensePlate);
            }

            else
            {
                Vehicle vehicle = CreateVehicleFromUserInput(licensePlate);
                Wheel[] wheels = m_ConsoleUtils.GetWheelsFromUser();
                vehicle.Wheels = wheels;
                Dictionary<string, string> fieldsFromUser = m_ConsoleUtils.GetAddedFieldsFromUser(vehicle.GetAddedFields());
                vehicle.SetAddedFields(fieldsFromUser);
            }
        }

        private Vehicle CreateVehicleFromUserInput(string licensePlate)
        {
            VehicleFactory.eVehicleType vehicleType = m_ConsoleUtils.GetVehicleTypeFromUser();
            CustomerInfo customerInfo = m_ConsoleUtils.GetCustomerInfoFromUser();
            string model = m_ConsoleUtils.GetModelFromUser();
            eEnergySourceType energySourceType = m_ConsoleUtils.GetEnergySourceTypeFromUser();
            float energyCapacity = m_ConsoleUtils.GetCurrentEnergyCapacityFromUser(energySourceType);

            return m_VehicleFactory.CreateVehicle(vehicleType, customerInfo, model, licensePlate, energySourceType,
                energyCapacity);
        }
    }
}