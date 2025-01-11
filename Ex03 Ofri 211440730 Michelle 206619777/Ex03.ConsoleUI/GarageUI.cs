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
        private bool m_ContinueAction;
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
            string licensePlate = r_ConsoleUtils.GetLicensePlateFromUser();
            
            switch (i_MenuOption)
            {
                case eMenuOption.AddVehicle:
                    handleAddVehicle(licensePlate);
                    break;
                case eMenuOption.LicencePlateList:
                    //
                    break;
                case eMenuOption.FillWheelsAirPressureToMax:
                    fillWheelsAirPressureToMax(licensePlate);
                    break;
                case eMenuOption.ChangeVehicleStatus:
                    //
                    break;
                case eMenuOption.RefuelVehicle:
                    refuelVehicle(licensePlate);
                    break;
                case eMenuOption.ChargeVehicle:
                    //
                    break;
                case eMenuOption.DisplayVehicleDetails:
                    displayVehicleDetails(licensePlate);
                    break;
            }
        }

        private void handleAddVehicle(string i_LicensePlate)
        {
            

            if (r_Garage.IsVehicleInTheGarage(i_LicensePlate))
            {
                Console.WriteLine("Vehicle with this license plate - {0} already exists, changing status to \"in repair\"",
                    i_LicensePlate);
                r_Garage.ChangeVehicleStatus(i_LicensePlate, eVehicleState.InRepair);
            }
            else
            {
                addVehicleToGarageWithRetry(i_LicensePlate);
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
                    return r_VehicleFactory.CreateVehicle(vehicleType, customerInfo, model, i_LicensePlate, energyCapacity);
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
                    Wheel[] wheels = r_ConsoleUtils.GetWheelsFromUser(i_Vehicle.NumOfWheels, i_Vehicle.MaxWheelAirPressure);
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

        private void checkIfCarIsInGarage(string i_LicensePlate)
        {
            m_ContinueAction = true;
            while (m_ContinueAction)
            {
                if (!r_Garage.IsVehicleInTheGarage(i_LicensePlate))
                {
                    handleExceptionAndAskUserIfToTryAgain(new ArgumentException(string.Format("Vehicle {0} not found in garage",i_LicensePlate)));
                }
                else
                {
                    m_ContinueAction = false;
                }
            }
        }

        private void fillWheelsAirPressureToMax(string i_LicensePlate)
        {
            checkIfCarIsInGarage(i_LicensePlate);
            m_ContinueAction = true;
            while (m_ContinueAction)
            {
                try
                {
                    r_Garage.FillWheelsAirPressureToMax(i_LicensePlate);
                    Console.WriteLine("car {0}, wheels air pressure filled to max", i_LicensePlate);
                }
                catch (Exception exception)
                {
                    handleExceptionAndAskUserIfToTryAgain(exception);
                }
            }
        }

        private void refuelVehicle(string i_LicensePlate)
        {
            checkIfCarIsInGarage(i_LicensePlate);
            m_ContinueAction = true;
            while(m_ContinueAction)
            {
                eFuelType fuelType = r_ConsoleUtils.GetFuelTypeFromUser();
                try
                {
                    float amountToFill = r_ConsoleUtils.GetPositivefloatInputFromUser();
                    r_Garage.RefuelVehicle(i_LicensePlate, amountToFill, fuelType);
                    Console.WriteLine("car {0}, refueled successfully", i_LicensePlate);
                    m_ContinueAction = false;
                }
                catch(Exception exception)
                {
                    handleExceptionAndAskUserIfToTryAgain(exception);
                }
            }
        }

        private void displayVehicleDetails(string i_LicensePlate)
        {
            checkIfCarIsInGarage(i_LicensePlate);
            Console.WriteLine(r_Garage.GetFullVehicleDetails(i_LicensePlate));
        }
        private void handleExceptionAndAskUserIfToTryAgain(Exception i_Exception)
        {
            Console.WriteLine(i_Exception.Message);
            if(!r_ConsoleUtils.GetBooleanAnswerFromUser("try again"))
            {
                throw i_Exception;
            }
        }
    }
}