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
                    addVehicleToTheGarage();
                    break;
                case eMenuOption.LicencePlateList:
                    displayLicensePlateList();
                    break;
                case eMenuOption.FillWheelsAirPressureToMax:
                    fillWheelsAirPressureToMax();
                    break;
                case eMenuOption.ChangeVehicleStatus:
                    changeVehicleStatus();
                    break;
                case eMenuOption.RefuelVehicle:
                    refuelVehicle();
                    break;
                case eMenuOption.ChargeVehicle:
                    chargeVehicle();
                    break;
                case eMenuOption.DisplayVehicleDetails:
                    displayVehicleDetails();
                    break;
            }
        }

        private void changeVehicleStatus()
        {
            string licensePlate = r_ConsoleUtils.GetLicensePlateFromUser();
            if (isVehicleInTheGarage(licensePlate))
            {
                eVehicleStatus vehicleStatusFromUser = r_ConsoleUtils.GetVehicleStatusFromUser();
                r_Garage.ChangeVehicleStatus(licensePlate, vehicleStatusFromUser);
                Console.WriteLine("Vehicle {0}, status changed to {1}", licensePlate, vehicleStatusFromUser);
            }
        }

        private void displayLicensePlateList()
        {
            bool showAllVehicles = r_ConsoleUtils.GetBooleanAnswerFromUser("show all vehicles");

            if (showAllVehicles)
            {
                List<string> allLicensePlates = r_Garage.GetAllLicensePlates();
                printLicensePlatesOrMessage(allLicensePlates, "All vehicles in the garage",
                    "No vehicles in the garage");
            }
            else
            {
                eVehicleStatus vehicleStatus = r_ConsoleUtils.GetVehicleStatusFromUser();
                List<string> filteredLicensePlates = r_Garage.GetLicensePlatesByVehicleStatus(vehicleStatus);
                printLicensePlatesOrMessage(filteredLicensePlates,
                    $"All {vehicleStatus} vehicles in the garage:",
                    $"No {vehicleStatus} vehicles in the garage");
            }
        }

        private void printLicensePlatesOrMessage(List<string> i_LicensePlates, string i_SuccessMessage,
            string i_FailureMessage)
        {
            if (i_LicensePlates.Count == 0)
            {
                Console.WriteLine(i_FailureMessage);
            }
            else
            {
                Console.WriteLine(i_SuccessMessage);
                r_ConsoleUtils.PrintLicensePlates(i_LicensePlates);
            }
        }

        private void addVehicleToTheGarage()
        {
            string licensePlate = r_ConsoleUtils.GetLicensePlateFromUser();
            if (r_Garage.IsVehicleInTheGarage(licensePlate))
            {
                Console.WriteLine(
                    "Vehicle with this license plate - {0} already exists, changing status to \"in repair\"",
                    licensePlate);
                r_Garage.ChangeVehicleStatus(licensePlate, eVehicleStatus.InRepair);
            }
            else
            {
                addVehicleToGarageWithRetry(licensePlate);
            }
        }

        private void addVehicleToGarageWithRetry(string i_LicensePlate)
        {
            try
            {
                Vehicle vehicle = createTypedVehicle(i_LicensePlate);
                r_Garage.AddVehicle(vehicle);
                Console.Clear();
                Console.WriteLine("Vehicle {0} added successfully", i_LicensePlate);
            }
            catch (Exception)
            {
                Console.Clear();
                Console.WriteLine("Vehicle {0} was not added to the garage.", i_LicensePlate);
            }
        }

        private Vehicle createTypedVehicle(string i_LicensePlate)
        {
            Vehicle vehicle = buildVehicleFromUserData(i_LicensePlate);
            getWheelsForVehicle(vehicle);
            setVehicleAddedFieldsFromUser(vehicle);
            return vehicle;
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
                    return r_VehicleFactory.CreateVehicle(vehicleType, customerInfo, model, i_LicensePlate,
                        energyCapacity);
                }
                catch (Exception exception)
                {
                    handleExceptionAndAskUserIfToTryAgain(exception);
                }
            }
        }

        private void getWheelsForVehicle(Vehicle i_Vehicle)
        {
            while (true)
            {
                try
                {
                    Wheel[] wheels =
                        r_ConsoleUtils.GetWheelsFromUser(i_Vehicle.NumOfWheels, i_Vehicle.MaxWheelAirPressure);
                    i_Vehicle.Wheels = wheels;
                    return;
                }
                catch (Exception exception)
                {
                    handleExceptionAndAskUserIfToTryAgain(exception);
                }
            }
        }

        private void setVehicleAddedFieldsFromUser(Vehicle i_Vehicle)
        {
            while (true)
            {
                try
                {
                    Dictionary<string, string> fieldsFromUser =
                        r_ConsoleUtils.GetAddedFieldsFromUser(i_Vehicle.GetAddedFields());
                    i_Vehicle.SetAddedFields(fieldsFromUser);
                    return;
                }
                catch (Exception exception)
                {
                    handleExceptionAndAskUserIfToTryAgain(exception);
                }
            }
        }

        private bool isVehicleInTheGarage(string i_LicensePlate)
        {
            bool isVehicleInTheGarage = false;
            bool continueAction = true;

            while (continueAction)
            {
                if (r_Garage.IsVehicleInTheGarage(i_LicensePlate))
                {
                    isVehicleInTheGarage = true;
                    continueAction = false;
                }
                else
                {
                    Console.WriteLine("Vehicle with this license plate - {0} is not in the garage", i_LicensePlate);
                    if (r_ConsoleUtils.GetBooleanAnswerFromUser("insert different license plate"))
                    {
                        i_LicensePlate = r_ConsoleUtils.GetLicensePlateFromUser();
                    }
                    else
                    {
                        continueAction = false;
                    }
                }
            }

            return isVehicleInTheGarage;
        }

        private void fillWheelsAirPressureToMax()
        {
            string licensePlate = r_ConsoleUtils.GetLicensePlateFromUser();
            if (isVehicleInTheGarage(licensePlate))
            {
                r_Garage.FillWheelsAirPressureToMax(licensePlate);
                Console.WriteLine("Vehicles {0}, wheels air pressure filled to max", licensePlate);
            }
        }

        private void refuelVehicle()
        {
            string licensePlate = r_ConsoleUtils.GetLicensePlateFromUser();
            if (isVehicleInTheGarage(licensePlate))
            {
                try
                {
                    refuelVehicleWithRetry(licensePlate);
                    Console.WriteLine("Vehicle {0}, refueled successfully", licensePlate);
                }
                catch (Exception)
                {
                    Console.WriteLine("Vehicle {0}, was not refueled", licensePlate);
                }
            }
        }

        private void refuelVehicleWithRetry(string licensePlate)
        {
            while (true)
            {
                eFuelType fuelType = r_ConsoleUtils.GetFuelTypeFromUser();
                float amountToFill = r_ConsoleUtils.GetLitersToRefuelFromUser();
                try
                {
                    r_Garage.RefuelVehicle(licensePlate, amountToFill, fuelType);
                    return;
                }
                catch (Exception exception)
                {
                    handleExceptionAndAskUserIfToTryAgain(exception);
                }
            }
        }

        private void chargeVehicle()
        {
            string licensePlate = r_ConsoleUtils.GetLicensePlateFromUser();
            if (isVehicleInTheGarage(licensePlate))
            {
                try
                {
                    chargeVehicleWithRetry(licensePlate);
                    Console.WriteLine("Vehicle {0}, charged successfully", licensePlate);
                }
                catch (Exception)
                {
                    Console.WriteLine("Vehicle {0}, was not charged", licensePlate);
                }
            }
        }

        private void chargeVehicleWithRetry(string licensePlate)
        {
            while (true)
            {
                float amountToFill = r_ConsoleUtils.GetMinutesToChargeFromUser();
                try
                {
                    r_Garage.ChargeBattery(licensePlate, amountToFill);
                    return;
                }
                catch (Exception exception)
                {
                    handleExceptionAndAskUserIfToTryAgain(exception);
                }
            }
        }

        private void displayVehicleDetails()
        {
            string licensePlate = r_ConsoleUtils.GetLicensePlateFromUser();
            if (isVehicleInTheGarage(licensePlate))
            {
                Console.WriteLine(r_Garage.GetFullVehicleDetails(licensePlate));
            }
        }

        private void handleExceptionAndAskUserIfToTryAgain(Exception i_Exception)
        {
            Console.WriteLine(i_Exception.Message);
            if (!r_ConsoleUtils.GetBooleanAnswerFromUser("try again"))
            {
                throw i_Exception;
            }
        }
    }
}