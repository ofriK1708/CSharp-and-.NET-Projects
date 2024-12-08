using System;

namespace Ex01_05
{
    class Program
    {
        public static void Main()
        {
            int[] digitsArray = getAndValidateUserInput();

            Console.WriteLine("The number of digits that are bigger then the units digit is : {0}", 
                getNumOfDigitsGreaterThanUnitsDigit(digitsArray));
            Console.WriteLine("The number of digits divided by 4 is : {0}", getNumOfDigitsDividedBy4(digitsArray));
            Console.WriteLine("the ratio between the biggest digit and smallest digit is : {0}", 
                getSmallestDigitBiggestDigitRatio(digitsArray));
            Console.WriteLine("The number of pairs of the same digit is : {0}", getNumOfSameDigitPairs(digitsArray));
        }
        
        private static int[] getAndValidateUserInput()
        {
            Console.WriteLine("Hello! please enter a 9-digit number: ");
            string digitString = Console.ReadLine();
            int[] digitsArray = new int[9];
            int index = 8;
            int tempNum;
            bool parseTest = int.TryParse(digitString, out tempNum);
            //TODO - handle negative numbers
            while (digitString.Length != 9 || !int.TryParse(digitString,out tempNum))
            {
                Console.WriteLine("Invalid number, please enter a 9-digit number: ");
                digitString = Console.ReadLine();
            }

            foreach(char ch in digitString)
            {
                digitsArray[index] = int.Parse(ch.ToString());
                index--;
            }

            return digitsArray;
        }

        private static int getNumOfDigitsGreaterThanUnitsDigit(int[] i_DigitsArray)
        {
            int numOfBiggerThenUnitsDig = 0;
            int unitDigit = i_DigitsArray[0];
            foreach(int digit in i_DigitsArray)
            {
                if (digit > unitDigit)
                {
                    numOfBiggerThenUnitsDig++;
                }
            }

            return numOfBiggerThenUnitsDig;
        }

        private static int getNumOfDigitsDividedBy4(int[] i_DigitsArray)
        {
            int numOfDigitsDividedBy4 = 0;
            foreach(int digit in i_DigitsArray)
            {
                if(digit % 4 == 0)
                {
                    numOfDigitsDividedBy4++;
                }
            }

            return numOfDigitsDividedBy4;
        }
        
        private static float getSmallestDigitBiggestDigitRatio(int[] i_DigitsArray)
        {
            int maxDigit = 0,minDigit = i_DigitsArray[0];
            float ratio;
            foreach(int digit in i_DigitsArray)
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
        private static int getNumOfSameDigitPairs(int[] i_DigitsArray)
        {
            int numOfPairs = 0;
            int[] digitCounter = new int[10];
            Array.Clear(digitCounter, 0, digitCounter.Length);
            foreach(int num in i_DigitsArray)
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