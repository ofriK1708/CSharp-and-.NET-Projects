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
                    if (!r_ConsoleUtils.GetBooleanAnswerFromUser("go back to menu"))
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
                Console.WriteLine("Vehicle with this license plate {0} already exists, changing status to in repair",
                    licensePlate);
                r_Garage.ChangeVehicleStatusToInRepare(licensePlate);
            }
            else
            {
                addVehicleToGarageWithRetry(licensePlate);
            }
        }

        private void addVehicleToGarageWithRetry(string licensePlate)
        {
            try
            {
                Vehicle vehicle = createTypedVehicle(licensePlate);
                r_Garage.AddVehicle(vehicle);
                Console.Clear();
                Console.WriteLine("Vehicle {0} added successfully", licensePlate);
            }
            catch (Exception)
            {
                Console.Clear();
                Console.WriteLine("Vehicle {0} was not added to the garage.", licensePlate);
            }
        }

        private Vehicle createTypedVehicle(string i_LicensePlate)
        {
            Vehicle vehicle = buildVehicleFromUserData(i_LicensePlate);
            GetWheelsForVehicle(vehicle);
            SetVehicleAddedFieldsFromUser(vehicle);
            return vehicle;
        }

        private void SetVehicleAddedFieldsFromUser(Vehicle vehicle)
        {
            while (true)
            {
                try
                {
                    Dictionary<string, string> fieldsFromUser =
                        r_ConsoleUtils.GetAddedFieldsFromUser(vehicle.GetAddedFields());
                    vehicle.SetAddedFields(fieldsFromUser);
                    return;
                }
                catch (Exception exception)
                {
                    HandleExceptionAndAskUserIfToTryAgain(exception);
                }
            }
        }

        private void GetWheelsForVehicle(Vehicle vehicle)
        {
            while (true)
            {
                try
                {
                    Wheel[] wheels = r_ConsoleUtils.GetWheelsFromUser(vehicle.NumOfWheels, vehicle.MaxWheelAirPressure);
                    vehicle.Wheels = wheels;
                    return;
                }
                catch (Exception exception)
                {
                    HandleExceptionAndAskUserIfToTryAgain(exception);
                }
            }
           
        }

        private Vehicle buildVehicleFromUserData(string i_LicensePlate)
        {
            VehicleFactory.eVehicleType vehicleType = r_ConsoleUtils.GetVehicleTypeFromUser();
            CustomerInfo customerInfo = r_ConsoleUtils.GetCustomerInfoFromUser();
            string model = r_ConsoleUtils.GetModelFromUser();
            
            while (true)
            {
                try
                {
                    float energyCapacity = r_ConsoleUtils.GetCurrentEnergyCapacityFromUser();
                    return r_VehicleFactory.CreateVehicle(vehicleType, customerInfo, model, i_LicensePlate, energyCapacity);
                }
                catch (Exception exception)
                {
                    HandleExceptionAndAskUserIfToTryAgain(exception);
                }
            }
        }

        private void HandleExceptionAndAskUserIfToTryAgain(Exception exception)
        {
            Console.WriteLine(exception.Message);
            if(!r_ConsoleUtils.GetBooleanAnswerFromUser("try again"))
            {
                throw exception;
            }
        }
    }
}