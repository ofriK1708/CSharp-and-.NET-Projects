using ex03;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.ConsoleUI
{
    internal class ConsoleUtils
    {
        internal void PrintStartMessage()
        {
            Console.WriteLine("Welcome to the garage");
        }

        internal eVehicleType GetVehicleTypeFromUser()
        {
            Console.WriteLine("Enter Vehicle type:\n" +
                "for car press 1,\n" +
                "for motorcycle press 2\n" +
                "for  for truck press 3");

            return (eVehicleType)getEnumInputFromUser(typeof(eVehicleType));
        }

        internal eMenuOption GetMenuChoiceFromUser()
        {
            printMainMenu();
            return (eMenuOption)getEnumInputFromUser(typeof(eMenuOption));
        }

        internal string GetLicensePlateFromUser()
        {
            Console.WriteLine("Enter licance plate");
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
            Console.WriteLine("Enter Vehicles energy source type:\n" +
                "for fuel press 1,\n" +
                "for electric press 2");

            return (eEnergySourceType)getEnumInputFromUser(typeof(eEnergySourceType));
        }

        internal float GetCurrentEnergyCapacityFromUser(eEnergySourceType energySourceType)
        {
            switch (energySourceType)
            {
                case eEnergySourceType.Electric:
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
            while (!string.IsNullOrWhiteSpace(userInput))
            {
                Console.WriteLine("Input cant be empty, please try again");
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
            Console.WriteLine("To quit , press 0");
        }

        private object getEnumInputFromUser(Type i_EnumType)
        {
            while (true)
            {
                try
                {
                    string userInput = getStringInputFromUser();
                    return parseInputToEnum(userInput, i_EnumType);
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

            if(numericValue < 0f)
            {
                throw new FormatException("Input can't be negative");
            }

            return numericValue;
        }

        private object parseInputToEnum(string i_UserInput, Type i_EnumType)
        {
            bool isInt = int.TryParse(i_UserInput, out int numericValue);

            if (!isInt)
            {
                throw new FormatException("Input must be and Integer");
            }

            if (!Enum.IsDefined(i_EnumType, numericValue))
            {
                throw new FormatException("Input must be from the specified options");
            }

            return Enum.ToObject(i_EnumType, numericValue);
        }

        internal Wheel[] GetWheelsFromUser()
        {
            //todo implement
            return new Wheel[0];
        }
    }
}
