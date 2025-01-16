using System;
using System.Collections.Generic;
using ex03;

namespace Ex03.ConsoleUI
{
    internal class ConsoleUtils
    {
        internal static void PrintStartMessage()
        {
            Console.WriteLine("Welcome to the garage\n");
        }

        internal static VehicleFactory.eVehicleType GetVehicleTypeFromUser()
        {
            Console.WriteLine("Please enter Vehicle type:");
            
            return (VehicleFactory.eVehicleType)displayAndGetEnumValueFromUser(typeof(VehicleFactory.eVehicleType));
        }

        internal static eMenuOption GetMenuChoiceFromUser()
        {
            printMainMenu();
            
            return (eMenuOption)getEnumInputFromUser(typeof(eMenuOption));
        }

        internal static string GetLicensePlateFromUser()
        {
            Console.WriteLine("Please enter license plate");
            
            return getStringInputFromUser();
        }

        internal static string GetModelFromUser()
        {
            Console.WriteLine("Please enter vehicle model");
            
            return getStringInputFromUser();
        }

        internal static CustomerInfo GetCustomerInfoFromUser()
        {
            Console.WriteLine("Please enter vehicles owner name");
            string name = getStringInputFromUser();

            Console.WriteLine("Please enter vehicles owner phone number");
            string phoneNumber = getStringInputFromUser();

            return new CustomerInfo(name, phoneNumber);
        }

        internal static float GetCurrentEnergyCapacityFromUser()
        {
            Console.WriteLine("Please enter the current fuel level (in liters) or battery charge (in hours left) of the vehicle:");
            
            return GetPositiveFloatInputFromUser();
        }

        internal static eFuelType GetFuelTypeFromUser()
        {
            Console.WriteLine("Please enter the fuel type you wish");
            
            return (eFuelType)displayAndGetEnumValueFromUser(typeof(eFuelType));
        }

        private static string getStringInputFromUser()
        {
            string userInput = Console.ReadLine();

            while (string.IsNullOrWhiteSpace(userInput))
            {
                Console.WriteLine("Input can't be empty, please try again");
                userInput = Console.ReadLine();
            }

            return userInput;
        }

        private static void printMainMenu()
        {
            Console.WriteLine("Please choose an option from the menu and press enter:");
            Console.WriteLine("1.To add new vehicle to the garage press 1");
            Console.WriteLine("2.To see a list of vehicles (license plates) that in the garage press 2");
            Console.WriteLine("3.To change vehicle's status in the garage press 3");
            Console.WriteLine("4.To fill vehicle's wheels air pressure to maximum press 4");
            Console.WriteLine("5.To refuel a vehicle press 5");
            Console.WriteLine("6.To recharge a vehicle press 6");
            Console.WriteLine("7.To see vehicle's information press 7");
            Console.WriteLine("To quit , press 0\n");
        }

        private static object getEnumInputFromUser(Type i_EnumType)
        {
            while (true)
            {
                try
                {
                    string userInput = getStringInputFromUser();
                    
                    return validateAndParseInputToEnum(userInput, i_EnumType);
                }
                catch (FormatException ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.WriteLine("Please try again");
                }
            }
        }

        internal static float GetPositiveFloatInputFromUser()
        {
            while (true)
            {
                try
                {
                    string userInput = getStringInputFromUser();
                    
                    return parseInputToPositiveFloat(userInput);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.WriteLine("Please try again");
                }
            }
        }

        private static float parseInputToPositiveFloat(string i_UserInput)
        {
            bool isFloat = float.TryParse(i_UserInput, out float numericValue);

            if (!isFloat)
            {
                throw new FormatException("Input must be a number");
            }

            if (numericValue < 0f)
            {
                throw new FormatException("Input can't be negative");
            }

            return numericValue;
        }

        private static object validateAndParseInputToEnum(string i_UserInput, Type i_EnumType)
        {
            if (!int.TryParse(i_UserInput, out int numericInput))
            {
                throw new FormatException("Input must match one of the specified numeric options.");
            }

            if (!Enum.IsDefined(i_EnumType, numericInput))
            {
                throw new FormatException("Input must match one of the specified numeric options.");
            }

            return Enum.ToObject(i_EnumType, numericInput);
        }


        internal static Wheel[] GetWheelsFromUser(int i_NumOfWheels, float i_MaxWheelAirPressure)
        {
            Wheel[] wheels = new Wheel[i_NumOfWheels];
            Console.WriteLine("Would you like to add same data for all wheels? (y/n)");
            bool sameDataToAllWheels = getBooleanInputFromUser();

            if (sameDataToAllWheels)
            {
                Wheel wheelDataFromUser = getWheelDataFromUser(i_MaxWheelAirPressure,1);
                for (int i = 0; i < i_NumOfWheels; i++)
                {
                    wheels[i] = wheelDataFromUser;
                }
            }
            else
            {
                for (int i = 0; i < i_NumOfWheels; i++)
                {
                    wheels[i] = getWheelDataFromUser(i_MaxWheelAirPressure,(uint)i+1);
                }
            }

            return wheels;
        }

        private static Wheel getWheelDataFromUser(float i_MaxWheelAirPressure,uint i_NumOfWheel)
        {
            while (true)
            {
                try
                {
                    Console.WriteLine("Enter #{0} wheel air pressure",i_NumOfWheel);
                    float airPressure = GetPositiveFloatInputFromUser();

                    Console.WriteLine("Enter #{0} wheel manufacturer",i_NumOfWheel);
                    string manufacturer = getStringInputFromUser();

                    return new Wheel(manufacturer, i_MaxWheelAirPressure, airPressure);
                }
                
                catch (Exception ex)
                {
                    HandleExceptionAndAskUserIfToTryAgain(ex);
                }
            }
        }

        internal static bool GetBooleanAnswerFromUser(string i_Question)
        {
            Console.WriteLine("Would you like to {0}? (y/n)", i_Question);

            return getBooleanInputFromUser();
        }

        public static Dictionary<string, string> GetAddedFieldsFromUser(Dictionary<string, Type> i_VehicleAddedFields)
        {
            Dictionary<string, string> fieldsFromUser = new Dictionary<string, string>();

            foreach (string field in i_VehicleAddedFields.Keys)
            {
                Console.WriteLine("Please enter {0}:", field);
                Type fieldType = i_VehicleAddedFields[field];
                fieldsFromUser.Add(field, getGeneralInputFromUser(fieldType));
            }

            return fieldsFromUser;
        }

        private static string getGeneralInputFromUser(Type i_FieldType)
        {
            string userInput;

            if (i_FieldType.IsEnum)
            {
                userInput = displayAndGetEnumValueFromUser(i_FieldType).ToString();
            }
            else if (i_FieldType == typeof(bool))
            {
                userInput = getBooleanInputFromUser().ToString();
            }
            else
            {
                userInput = getStringInputFromUser();

                try
                {
                    object convertedValue = Convert.ChangeType(userInput, i_FieldType);
                    userInput = convertedValue.ToString();
                }
                
                catch (Exception)
                {
                    throw new FormatException("Answer input does not match the specified type");
                }
            }

            return userInput;
        }

        private static bool getBooleanInputFromUser()
        {
            Console.WriteLine("y/n ?");
            string stringInputFromUser = getStringInputFromUser().ToLower();

            while (stringInputFromUser != "y" && stringInputFromUser != "n")
            {
                Console.WriteLine("Please enter 'y' (yes) or 'n' (no)");
                stringInputFromUser = getStringInputFromUser().ToLower();
            }

            return stringInputFromUser == "y";
        }

        private static object displayAndGetEnumValueFromUser(Type i_EnumType)
        {
            foreach (ValueType value in Enum.GetValues(i_EnumType))
            {
                Console.WriteLine("For {0} press {1}", value, (int)value);
            }

            return getEnumInputFromUser(i_EnumType);
        }

        public static void PrintLicensePlates(LinkedList<string> i_LicensePlates)
        {
            foreach (string licensePlate in i_LicensePlates)
            {
                Console.WriteLine(licensePlate);
            }
        }

        public static eVehicleStatus GetVehicleStatusFromUser()
        {
            Console.WriteLine("Enter vehicle status:");

            return (eVehicleStatus)displayAndGetEnumValueFromUser(typeof(eVehicleStatus));
        }

        public static float GetLitersToRefuelFromUser()
        {
            Console.WriteLine("Enter liters to refuel:");

            return GetPositiveFloatInputFromUser();
        }

        public static float GetMinutesToChargeFromUser()
        {
            Console.WriteLine("Enter minutes to charge:");

            return GetPositiveFloatInputFromUser();
        }
        
        internal static void HandleExceptionAndAskUserIfToTryAgain(Exception i_Exception)
        {
            Console.WriteLine(i_Exception.Message);

            if (!GetBooleanAnswerFromUser("try again"))
            {
                throw i_Exception;
            }
        }
    }
}