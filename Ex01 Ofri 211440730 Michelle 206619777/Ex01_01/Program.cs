using System;
using System.Linq;

namespace Ex01_01
{
    class Program
    {
        public static void Main()
        {
            string binaryNumber1 = getBinaryNumberFromUser(1);
            string binaryNumber2 = getBinaryNumberFromUser(2);
            string binaryNumber3 = getBinaryNumberFromUser(3);

            int[] decimalNumbers = new int[3];
            decimalNumbers[0] = convertBinryToDecimal(binaryNumber1);
            decimalNumbers[1] = convertBinryToDecimal(binaryNumber2);
            decimalNumbers[2] = convertBinryToDecimal(binaryNumber3);
 
            Array.Sort(decimalNumbers);
            Console.WriteLine("The decimal numbers are: {0}, {1}, {2}",decimalNumbers);

            double average = decimalNumbers.Average();
            Console.WriteLine("The average is : {0}", average);

            int[] longestBitsSequences = new int[3];
            longestBitsSequences[0] = getLongestBitsSequence(binaryNumber1);
            longestBitsSequences[1] = getLongestBitsSequence(binaryNumber2);
            longestBitsSequences[2] = getLongestBitsSequence(binaryNumber3);
            int maxBitSequence = longestBitsSequences.Max();
            Console.WriteLine("The longest bit sequence is : {0}", maxBitSequence);


            
        }

        private static string getBinaryNumberFromUser(int i_inputIndex)
        {
            Console.WriteLine("Please enter the #{0} binary number with 8 digits (and then press 'enter')", i_inputIndex);
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
            bool validation = i_binaryNumberToCheck.Length == 8 && i_binaryNumberToCheck.All(c => c == '1' || c == '0');

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

        private static int getLongestBitsSequence(string i_binaryNumber)
        {
            int localMaxSequence = 1,globalMaxSequnce = 1;
            for(int i = 0; i < i_binaryNumber.Length-1; i++)
            {
                if (i_binaryNumber[i] == i_binaryNumber[i + 1]) 
                {
                    localMaxSequence++;
                }
                else
                {
                    if(localMaxSequence > globalMaxSequnce)
                    {
                        globalMaxSequnce = localMaxSequence;
                    }
                    globalMaxSequnce = 1;
                }
            }

            return globalMaxSequnce;
        }

        private static int getBitsSwapCount(string i_binaryNumber)
        {
            int numOfSwaps = 0;
            for (int i = 0; i < i_binaryNumber.Length - 1; i++)
            {
                if (i_binaryNumber[i] != i_binaryNumber[i + 1])
                {
                    numOfSwaps++;
                }
            }

            return numOfSwaps;
        }
    }
}
