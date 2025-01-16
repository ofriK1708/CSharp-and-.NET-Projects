using System;
using System.Collections.Generic;
using ex03;

namespace Ex03.ConsoleUI
{
    public class GarageUI
    {
        private readonly Garage r_Garage = new Garage();
        private readonly VehicleFactory r_VehicleFactory = new VehicleFactory();
        private bool m_QuitGarage = false;

        internal void Start()
        {
            ConsoleUtils.PrintStartMessage();

            while (!m_QuitGarage)
            {
                eMenuOption menuOptionFromUser = ConsoleUtils.GetMenuChoiceFromUser();
                Console.Clear();
                if (menuOptionFromUser.Equals(eMenuOption.Quit))
                {
                    exitGarage();
                }
                else
                {
                    handleUserChoice(menuOptionFromUser);
                    if (!ConsoleUtils.GetBooleanAnswerFromUser("go back to menu"))
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
            string licensePlate = ConsoleUtils.GetLicensePlateFromUser();

            if (isVehicleInTheGarage(ref licensePlate))
            {
                eVehicleStatus vehicleStatusFromUser = ConsoleUtils.GetVehicleStatusFromUser();
                r_Garage.ChangeVehicleStatus(licensePlate, vehicleStatusFromUser);
                Console.WriteLine("Vehicle {0}, status changed to {1}", licensePlate, vehicleStatusFromUser);
            }
        }

        private void displayLicensePlateList()
        {
            bool showAllVehicles = ConsoleUtils.GetBooleanAnswerFromUser("show all vehicles");

            if (showAllVehicles)
            {
                List<string> allLicensePlates = r_Garage.GetAllLicensePlates();
                printLicensePlatesOrMessage(allLicensePlates, "All vehicles in the garage",
                    "No vehicles in the garage");
            }
            else
            {
                eVehicleStatus vehicleStatus = ConsoleUtils.GetVehicleStatusFromUser();
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
                ConsoleUtils.PrintLicensePlates(i_LicensePlates);
            }
        }

        private void addVehicleToTheGarage()
        {
            string licensePlate = ConsoleUtils.GetLicensePlateFromUser();

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
            VehicleFactory.eVehicleType vehicleType = ConsoleUtils.GetVehicleTypeFromUser();
            CustomerInfo customerInfo = ConsoleUtils.GetCustomerInfoFromUser();
            string model = ConsoleUtils.GetModelFromUser();

            while (true)
            {
                try
                {
                    float energyCapacity = ConsoleUtils.GetCurrentEnergyCapacityFromUser();
                    return r_VehicleFactory.CreateVehicle(vehicleType, customerInfo, model, i_LicensePlate,
                        energyCapacity);
                }
                
                catch (Exception exception)
                {
                    ConsoleUtils.HandleExceptionAndAskUserIfToTryAgain(exception);
                }
            }
        }

        private void getWheelsForVehicle(Vehicle i_Vehicle)
        {
            Wheel[] wheels = ConsoleUtils.GetWheelsFromUser(i_Vehicle.NumOfWheels, i_Vehicle.MaxWheelAirPressure);
            i_Vehicle.Wheels = wheels;
        }

        private void setVehicleAddedFieldsFromUser(Vehicle i_Vehicle)
        {
            while (true)
            {
                try
                {
                    Dictionary<string, string> fieldsFromUser =
                        ConsoleUtils.GetAddedFieldsFromUser(i_Vehicle.GetAddedFields());
                    i_Vehicle.SetAddedFields(fieldsFromUser);
                    break;
                }
                
                catch (Exception exception)
                {
                    ConsoleUtils.HandleExceptionAndAskUserIfToTryAgain(exception);
                }
            }
        }

        private bool isVehicleInTheGarage(ref string io_LicensePlate)
        {
            bool isVehicleInTheGarage = false;
            bool continueAction = true;

            while (continueAction)
            {
                if (r_Garage.IsVehicleInTheGarage(io_LicensePlate))
                {
                    isVehicleInTheGarage = true;
                    continueAction = false;
                }
                else
                {
                    Console.WriteLine("Vehicle with this license plate - {0} is not in the garage", io_LicensePlate);

                    if (ConsoleUtils.GetBooleanAnswerFromUser("insert different license plate"))
                    {
                        io_LicensePlate = ConsoleUtils.GetLicensePlateFromUser();
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
            string licensePlate = ConsoleUtils.GetLicensePlateFromUser();

            if (isVehicleInTheGarage(ref licensePlate))
            {
                r_Garage.FillWheelsAirPressureToMax(licensePlate);
                Console.WriteLine("Vehicles {0}, wheels air pressure filled to max", licensePlate);
            }
        }

        private void refuelVehicle()
        {
            string licensePlate = ConsoleUtils.GetLicensePlateFromUser();

            if (isVehicleInTheGarage(ref licensePlate))
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

        private void refuelVehicleWithRetry(string i_LicensePlate)
        {
            while (true)
            {
                eFuelType fuelType = ConsoleUtils.GetFuelTypeFromUser();
                float amountToFill = ConsoleUtils.GetLitersToRefuelFromUser();
                
                try
                {
                    r_Garage.RefuelVehicle(i_LicensePlate, amountToFill, fuelType);
                    break;
                }
                
                catch (Exception exception)
                {
                    ConsoleUtils.HandleExceptionAndAskUserIfToTryAgain(exception);
                }
            }
        }

        private void chargeVehicle()
        {
            string licensePlate = ConsoleUtils.GetLicensePlateFromUser();

            if (isVehicleInTheGarage(ref licensePlate))
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

        private void chargeVehicleWithRetry(string i_LicensePlate)
        {
            while (true)
            {
                float minutesToCharge = ConsoleUtils.GetMinutesToChargeFromUser();
                
                try
                {
                    r_Garage.ChargeBattery(i_LicensePlate, minutesToCharge);
                    break;
                }
               
                catch (Exception exception)
                {
                    ConsoleUtils.HandleExceptionAndAskUserIfToTryAgain(exception);
                }
            }
        }

        private void displayVehicleDetails()
        {
            string licensePlate = ConsoleUtils.GetLicensePlateFromUser();

            if (isVehicleInTheGarage(ref licensePlate))
            {
                Console.Clear();
                Console.WriteLine(r_Garage.GetFullVehicleDetails(licensePlate));
            }
        }
    }
}