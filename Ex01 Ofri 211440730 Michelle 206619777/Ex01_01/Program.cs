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
            Console.WriteLine("The decimal numbers are: {0}, {1}, {2}", decimalNumbers[0], decimalNumbers[1], decimalNumbers[2]);

            double average = decimalNumbers.Average();
            Console.WriteLine("The average is : {0}", average);

            int[] longestBitsSequences = new int[3];
            longestBitsSequences[0] = getLongestBitsSequence(binaryNumber1);
            longestBitsSequences[1] = getLongestBitsSequence(binaryNumber2);
            longestBitsSequences[2] = getLongestBitsSequence(binaryNumber3);
            int maxBitSequence = longestBitsSequences.Max();
            Console.WriteLine("The longest bits sequence is : {0}", maxBitSequence);

            Console.WriteLine("The number of swaps in {0} are : {1}",binaryNumber1, getBitsSwapCount(binaryNumber1));
            Console.WriteLine("The number of swaps in {0} are : {1}", binaryNumber2, getBitsSwapCount(binaryNumber2));
            Console.WriteLine("The number of swaps in {0} are : {1}", binaryNumber3, getBitsSwapCount(binaryNumber3));

            Console.WriteLine("The binary number with the most '0' is : {0}", 
                getBinaryNumberWithMax0(binaryNumber1, binaryNumber2, binaryNumber3));


        }

        private static string getBinaryNumberFromUser(int i_InputIndex)
        {
            Console.WriteLine("Please enter the #{0} binary number with 8 digits (and then press 'enter')", i_InputIndex);
            string binaryNumber = Console.ReadLine();

            while (!validateBinaryNumber(binaryNumber))
            {
                Console.WriteLine("the number you inserted isn't valid, please try again");
                binaryNumber = Console.ReadLine();
            }

            return binaryNumber;
        }

        private static bool validateBinaryNumber(string i_BinaryNumberToCheck)
        {
            bool validation = i_BinaryNumberToCheck.Length == 8 && i_BinaryNumberToCheck.All(c => c == '1' || c == '0');

            return validation;
        }

        private static int convertBinryToDecimal(string i_BinaryNumberToConvert)
        {
            int powerOf2 = 1;
            int decimalNumber = 0;
            for(int i = i_BinaryNumberToConvert.Length - 1; i >= 0; i--) 
            {
                decimalNumber += int.Parse(i_BinaryNumberToConvert[i].ToString()) * powerOf2;
                powerOf2 *= 2;
            }

            return decimalNumber;
        }

        private static int getLongestBitsSequence(string i_BinaryNumber)
        {
            int localMaxSequence = 1,globalMaxSequnce = 1;
            for(int i = 0; i < i_BinaryNumber.Length-1; i++)
            {
                if (i_BinaryNumber[i] == i_BinaryNumber[i + 1]) 
                {
                    localMaxSequence++;
                }
                else
                {
                    if(localMaxSequence > globalMaxSequnce)
                    {
                        globalMaxSequnce = localMaxSequence;
                    }
                    localMaxSequence = 1;
                }
            }

            return Math.Max(localMaxSequence, globalMaxSequnce);
        }

        private static int getBitsSwapCount(string i_BinaryNumber)
        {
            int numOfSwaps = 0;
            for (int i = 0; i < i_BinaryNumber.Length - 1; i++)
            {
                if (i_BinaryNumber[i] != i_BinaryNumber[i + 1])
                {
                    numOfSwaps++;
                }
            }

            return numOfSwaps;
        }

        private static string getBinaryNumberWithMax0(params string[] i_BinaryNumbers)
        {
            string binaryNumberWithMax0 = i_BinaryNumbers[0];
            int numOfZeros;
            int maxNumOfZeros = 0;
            foreach (string binary in i_BinaryNumbers)
            {
                numOfZeros = binary.Count(c => c == '0');
                if (numOfZeros >= maxNumOfZeros)
                {
                    binaryNumberWithMax0 = binary;
                    maxNumOfZeros = numOfZeros;
                }
            }

            return binaryNumberWithMax0;
        }
    }
}
