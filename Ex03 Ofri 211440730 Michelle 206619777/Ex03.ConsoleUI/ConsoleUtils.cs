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

        internal float GetCurrentEnergyCapacityFromUser()
        {
            Console.WriteLine("Enter the current reserve in the vehicle");
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


        internal Wheel[] GetWheelsFromUser(int i_NumOfWheels, float i_MaxWheelAirPressure)
        {
            Wheel[] wheels = new Wheel[i_NumOfWheels];

            Console.WriteLine("Would you like to add same data for all wheels? (y/n)");
            bool sameDataToAllWheels = bool.Parse(getBooleanInputFromUser());

            if (sameDataToAllWheels)
            {
                Wheel wheelDataFromUser = getWheelDataFromUser(i_MaxWheelAirPressure);
                for (int i = 0; i < i_NumOfWheels; i++)
                {
                    wheels[i] = wheelDataFromUser;
                }
            }
            else
            {
                for (int i = 0; i < i_NumOfWheels; i++)
                {
                    wheels[i] = getWheelDataFromUser(i_MaxWheelAirPressure);
                }
            }
            
            return wheels;
        }

        private Wheel getWheelDataFromUser(float i_MaxWheelAirPressure)
        {
            Console.WriteLine("Enter wheel air pressure");
            float airPressure = getPositivefloatInputFromUser();

            Console.WriteLine("Enter wheel manufacturer");
            string manufacturer = getStringInputFromUser();

            return new Wheel(manufacturer, i_MaxWheelAirPressure, airPressure);
        }

        internal bool GetBooleanAnswerFromUser(string i_Question)
        {
            Console.WriteLine("Would you like to {0}? (y/n)", i_Question);
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
            if (i_FieldType.IsEnum)
            {
                return displayAndGetEnumValueFromUser(i_FieldType).ToString();
            }

            if (i_FieldType == typeof(bool))
            {
                return getBooleanInputFromUser();
            }

            string input = getStringInputFromUser();

            try
            {
                object convertedValue = Convert.ChangeType(input, i_FieldType);
                return convertedValue.ToString();
            }
            catch (Exception e)
            {
                throw new FormatException("Answer input does not match the specified type");
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

            return stringInputFromUser == "y" ? bool.TrueString : bool.FalseString;
        }

        private object displayAndGetEnumValueFromUser(Type i_EnumType)
        {
            foreach (ValueType value in Enum.GetValues(i_EnumType))
            {
                Console.WriteLine("For {0} press {1}", value, (int)value);
            }

            return getEnumInputFromUser(i_EnumType);
        }

        public void PrintLicensePlates(List<string> i_LicensePlates)
        {
            foreach (string licensePlate in i_LicensePlates)
            {
                Console.WriteLine(licensePlate);  
            }
        }

        public eVehicleState GetVehicleStateToShowFromUser()
        {
            Console.WriteLine("Enter vehicle state to show:");
            return (eVehicleState)displayAndGetEnumValueFromUser(typeof(eVehicleState));
        }
    }
}