using System;
using System.Collections.Generic;
using ex03;

namespace Ex03.ConsoleUI
{
    internal class ConsoleUtils
    {
        internal void PrintStartMessage()
        {
            Console.WriteLine("Welcome to the garage\n");
        }

        internal VehicleFactory.eVehicleType GetVehicleTypeFromUser()
        {
            Console.WriteLine("Enter Vehicle type:");
            return (VehicleFactory.eVehicleType)displayAndGetEnumValueFromUser(typeof(VehicleFactory.eVehicleType));
        }

        internal eMenuOption GetMenuChoiceFromUser()
        {
            printMainMenu();
            return (eMenuOption)getEnumInputFromUser(typeof(eMenuOption));
        }

        internal string GetLicensePlateFromUser()
        {
            Console.WriteLine("Enter license plate");
            return getStringInputFromUser();
        }

        internal string GetModelFromUser()
        {
            Console.WriteLine("Enter vehicle model");
            return getStringInputFromUser();
        }

        internal CustomerInfo GetCustomerInfoFromUser()
        {
            Console.WriteLine("Enter vehicles owner name");
            string name = getStringInputFromUser();

            Console.WriteLine("Enter vehicles owner phone number");
            string phoneNumber = getStringInputFromUser();

            return new CustomerInfo(name, phoneNumber);
        }

        internal eEnergySourceType GetEnergySourceTypeFromUser()
        {
            Console.WriteLine("Enter Vehicles energy source type");
            return (eEnergySourceType)displayAndGetEnumValueFromUser(typeof(eEnergySourceType));
        }

        internal float GetCurrentEnergyCapacityFromUser(eEnergySourceType i_EnergySourceType)
        {
            switch (i_EnergySourceType)
            {
                case eEnergySourceType.Battery:
                    Console.WriteLine("Enter the current battery reserve in the vehicle in minutes");
                    break;
                case eEnergySourceType.Fuel:
                    Console.WriteLine("Enter the current fuel reserve in the vehicle in liters");
                    break;
            }

            return getPositivefloatInputFromUser();
        }

        private string getStringInputFromUser()
        {
            string userInput = Console.ReadLine();
            while (string.IsNullOrWhiteSpace(userInput))
            {
                Console.WriteLine("Input cant be empty, please try again");
                userInput = Console.ReadLine();
            }

            return userInput;
        }

        private void printMainMenu()
        {
            Console.WriteLine("Please choose an option from the menue and press enter:");
            Console.WriteLine("1.To add new vehicle to the garage press 1");
            Console.WriteLine("2.To see a list of vehicles (license plates) that in the garage press 2");
            Console.WriteLine("3.To change vehicl's status in the garage press 3");
            Console.WriteLine("4.To fill vehicl's air pressure to maximum press 4");
            Console.WriteLine("5.To fuel a vehicle press 5");
            Console.WriteLine("6.To charge a vehicle press 6");
            Console.WriteLine("7.To see vehicl's information press 7");
            Console.WriteLine("To quit , press 0\n");
        }

        private object getEnumInputFromUser(Type i_EnumType)
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

        private float getPositivefloatInputFromUser()
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

        private float parseInputToPositiveFloat(string i_UserInput)
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

        private object validateAndParseInputToEnum(string i_UserInput, Type i_EnumType)
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


        internal Wheel[] GetWheelsFromUser()
        {
            //todo implement
            return new Wheel[0];
        }

        internal bool getIfUserWantToGoBackToMenu()
        {
            Console.WriteLine("Would you like to go back to menu? (y/n)");
            return Convert.ToBoolean(getBooleanInputFromUser());
        }
        
        internal bool getIfUserWantToTryAgain()
        {
            Console.WriteLine("Would you like to try again? (y/n)");
            return Convert.ToBoolean(getBooleanInputFromUser());
        }

        public Dictionary<string, string> GetAddedFieldsFromUser(Dictionary<string, Type> i_VehicleAddedFields)
        {
            Dictionary<string, string> fieldsFromUser = new Dictionary<string, string>();

            foreach (string field in i_VehicleAddedFields.Keys)
            {
                Console.WriteLine("Enter {0}:", field);
                Type fieldType = i_VehicleAddedFields[field];
                fieldsFromUser.Add(field, getGeneralInputFromUser(fieldType));
            }

            return fieldsFromUser;
        }

        private string getGeneralInputFromUser(Type i_FieldType)
        {
            while (true)
            {
                try
                {
                    if (i_FieldType.IsEnum)
                    {
                        return displayAndGetEnumValueFromUser(i_FieldType).ToString();
                    }

                    if (i_FieldType == typeof(bool))
                    {
                        return getBooleanInputFromUser();
                    }
                    
                    string input = getStringInputFromUser();
                    object convertedValue = Convert.ChangeType(input, i_FieldType);
                    return convertedValue.ToString();
                }
                catch (Exception)
                {
                    Console.WriteLine("Invalid input. Please try again");
                }
            }
        }

        private string getBooleanInputFromUser()
        {
            Console.WriteLine("y/n ?");
            string stringInputFromUser = getStringInputFromUser().ToLower();
            while (stringInputFromUser != "y" && stringInputFromUser != "n")
            {
                Console.WriteLine("Please enter y or n");
                stringInputFromUser = getStringInputFromUser();
            }

            return stringInputFromUser == "y" ? bool.TrueString: bool.FalseString;
        }

        private object displayAndGetEnumValueFromUser(Type i_EnumType)
        {
            foreach (ValueType value in Enum.GetValues(i_EnumType))
            {
                Console.WriteLine("For {0} press {1}", value, (int)value);
            }

            return getEnumInputFromUser(i_EnumType);
        }
    }
}