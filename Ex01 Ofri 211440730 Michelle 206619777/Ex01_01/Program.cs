using System;
using System.Linq;

namespace Ex01_01
{
    class Program
    {
        public static void Main()
        {
            string binaryNumber1 = GetBinaryNumberFromUser(1);
            string binaryNumber2 = GetBinaryNumberFromUser(2);
            string binaryNumber3 = GetBinaryNumberFromUser(3);

            int[] decimalNumbers = { convertBinryToDecimal(binaryNumber1), convertBinryToDecimal(binaryNumber2), 
                convertBinryToDecimal(binaryNumber3) };

            Array.Sort(decimalNumbers);
            Console.WriteLine(@"The decimal numbers are: {0}, {1}, {2}", decimalNumbers[0], decimalNumbers[1], decimalNumbers[2]);
        }

        public static string GetBinaryNumberFromUser(int i_inputIndex)
        {
            Console.WriteLine("Please enter the #{0} binary number with 7 digits (and then press 'enter')", i_inputIndex);
            string binaryNumber = Console.ReadLine();

            while (!validateBinaryNumber(binaryNumber))
            {
                Console.WriteLine("the number you inserted isn't valid, please try again");
                binaryNumber = Console.ReadLine();
            }

            return binaryNumber;
        }

        private static bool validateBinaryNumber(string i_binaryNumberToCheck)
        {
            bool validation = i_binaryNumberToCheck.Length == 7 && i_binaryNumberToCheck.All(c => c == '1' || c == '0');

            return validation;
        }

        private static int convertBinryToDecimal(string i_binaryNumberToConvert)
        {
            int powerOf2 = 1;
            int decimalNumber = 0;
            for(int i = i_binaryNumberToConvert.Length - 1; i >= 0; i--) 
            {
                decimalNumber += int.Parse(i_binaryNumberToConvert[i].ToString()) * powerOf2;
                powerOf2 *= 2;
            }

            return decimalNumber;
        }
    }
}
