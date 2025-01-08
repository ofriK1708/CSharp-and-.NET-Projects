using System;
using System.Collections.Generic;
using ex03;

namespace Ex03.ConsoleUI
{
    public class GarageUI
    {
        private readonly ConsoleUtils r_ConsoleUtils = new ConsoleUtils();
        private readonly Garage r_Garage = new Garage();
        private readonly VehicleFactory r_VehicleFactory = new VehicleFactory();
        private bool m_QuitGarage = false;

        internal void Start()
        {
            r_ConsoleUtils.PrintStartMessage();

            while (!m_QuitGarage)
            {
                eMenuOption menuOptionFromUser = r_ConsoleUtils.GetMenuChoiceFromUser();

                Console.Clear();
                if (menuOptionFromUser.Equals(eMenuOption.Quit))
                {
                    exitGarage();
                }
                else
                {
                    handleUserChoice(menuOptionFromUser);
                    Console.Clear();
                    if (!r_ConsoleUtils.getIfUserWantToGoBackToMenu())
                    {
                        exitGarage();
                    }
                    else
                    {
                        Console.Clear();
                    }
                }
            }
        }

        private void exitGarage()
        {
            m_QuitGarage = true;
            Console.WriteLine("Goodbye :)");
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
            string licensePlate = r_ConsoleUtils.GetLicensePlateFromUser();

            if (r_Garage.IsVehicleInTheGarage(licensePlate))
            {
                Console.WriteLine("Vehicle with this license plate already exists, changing status to in repair");
                r_Garage.ChangeVehicleStatusToInRepare(licensePlate);
            }

            else
            {
                addVehicleToGarageWithRetry(licensePlate);
            }
        }

        private void addVehicleToGarageWithRetry(string licensePlate)
        {
            while (true)
            {
                try
                {
                    Vehicle vehicle = createTypedVehicle(licensePlate);
                    r_Garage.AddVehicle(vehicle);
                    Console.WriteLine("Vehicle added successfully");
                    return;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    Console.WriteLine("Vehicle was not added");
                    if (!r_ConsoleUtils.getIfUserWantToTryAgain())
                    {
                        return;
                    }
                    Console.Clear();
                }
            }
        }

        private Vehicle createTypedVehicle(string i_LicensePlate)
        {
            Vehicle vehicle = buildVehicleFromUserData(i_LicensePlate);
            // Wheel[] wheels = r_ConsoleUtils.GetWheelsFromUser();
            // vehicle.Wheels = wheels;
            Dictionary<string, string> fieldsFromUser =
                r_ConsoleUtils.GetAddedFieldsFromUser(vehicle.GetAddedFields());
            vehicle.SetAddedFields(fieldsFromUser);
            return vehicle;
        }

        private Vehicle buildVehicleFromUserData(string i_LicensePlate)
        {
            VehicleFactory.eVehicleType vehicleType = r_ConsoleUtils.GetVehicleTypeFromUser();
            CustomerInfo customerInfo = r_ConsoleUtils.GetCustomerInfoFromUser();
            string model = r_ConsoleUtils.GetModelFromUser();
            eEnergySourceType energySourceType = r_ConsoleUtils.GetEnergySourceTypeFromUser();
            float energyCapacity = r_ConsoleUtils.GetCurrentEnergyCapacityFromUser(energySourceType);

            return r_VehicleFactory.CreateVehicle(vehicleType, customerInfo, model, i_LicensePlate,
                energySourceType,
                energyCapacity);
        }
    }
}