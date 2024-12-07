using System;
using System.Globalization;

namespace Ex01_05
{
    class Program
    {
        public static void Main()
        {
            int[] numArray = getAndValidateUserInput();

            Console.WriteLine("The number of digits that are bigger then the units digit is : {0}", 
                getNumOfDigitsGreaterThanUnitsDigit(ref numArray));
            Console.WriteLine("The number of digits divided by 4 is : {0}", getNumOfDigitsDividedBy4(ref numArray));
            Console.WriteLine("the ratio between the biggest digit and smallest digit is : {0}", 
                getSmallestDigitBiggestDigitRatio(ref numArray));
            Console.WriteLine("The number of pairs of the same digit is : {0}", getNumOfSameDigitPairs(ref numArray));
        }
        
        private static int[] getAndValidateUserInput()
        {
            Console.WriteLine("Hello! please enter a 9-digit number: ");
            string stringNumber = Console.ReadLine();
            int[] numberInAnArray = new int[9];
            int index = 8;
            int tempNum;
            bool parseTest = int.TryParse(stringNumber, out tempNum);
            //TODO - handle negative numbers
            while (stringNumber.Length != 9 || !int.TryParse(stringNumber,out tempNum))
            {
                Console.WriteLine("Invalid number, please enter a 9-digit number: ");
                stringNumber = Console.ReadLine();
            }

            foreach(char ch in stringNumber)
            {
                numberInAnArray[index] = int.Parse(ch.ToString());
                index--;
            }

            return numberInAnArray;
        }

        private static int getNumOfDigitsGreaterThanUnitsDigit(ref int[] number)
        {
            int numOfBiggerThenUnitsDig = 0;
            int unitDigit = number[0];
            foreach(int digit in number)
            {
                if (digit > unitDigit)
                {
                    numOfBiggerThenUnitsDig++;
                }
            }

            return numOfBiggerThenUnitsDig;
        }

        private static int getNumOfDigitsDividedBy4(ref int[] number)
        {
            int numOfDigitsDividedBy4 = 0;
            foreach(int digit in number)
            {
                if(digit % 4 == 0)
                {
                    numOfDigitsDividedBy4++;
                }
            }

            return numOfDigitsDividedBy4;
        }
        
        private static float getSmallestDigitBiggestDigitRatio(ref int[] number)
        {
            int maxDigit = 0,minDigit = number[0];
            float ratio;
            foreach(int digit in number)
            {
                if(digit > maxDigit)
                {
                    maxDigit = digit;
                }
                
                if(digit > 0 && (digit < minDigit || minDigit == 0))
                {
                    minDigit = digit;
                }
            }

            if(maxDigit == 0)
            {
                ratio = 0;
            }
            else
            {
                ratio = (float)maxDigit / minDigit;
            }

            return ratio;
        }
        private static int getNumOfSameDigitPairs(ref int[] number)
        {
            int numOfPairs = 0;
            int[] digitCounter = new int[10];
            Array.Clear(digitCounter, 0, digitCounter.Length);
            foreach(int num in number)
            {
                digitCounter[num]++;
            }

            foreach(int counter in digitCounter)
            {
                if(counter > 1)
                {
                    numOfPairs += ((counter - 1) * counter) / 2;
                }
            }

            return numOfPairs;
        }
    }
    
}
// TODO - talk with michelle about this 
/*      
        parseTestResult = int.TryParse(stringNumber, out tempNum);
        //TODO - handle negative numbers
        while (!parseTestResult && (tempNum < 0 && stringNumber.Length != 10) || stringNumber.Length != 9)
*/