using FileOrganizationAssignment;
using System;

class Program
{
    static void Main(string[] args)
    {
        Random random = new Random();
                                                                    
        Console.Write("Enter the number of random number: ");
        int numberOfKeysToGenerate = Convert.ToInt32(Console.ReadLine());

        int tableSize = FindPrimeFinalSize(numberOfKeysToGenerate);     //calculate the final table size for packing factor to be %65 - %95
        //Console.WriteLine(tableSize);

        int lowerLimit = 0;
        int upperLimit = 1000;

        List<int> randomNumbers = GenerateRandomNumbers(lowerLimit, upperLimit, numberOfKeysToGenerate);


        BEISCH beisch = new BEISCH();

        beisch.ApplyBEISCH(randomNumbers, tableSize);

        Console.WriteLine();
        Console.Write("Enter the searching value: ");   // calculate probe number for given value in BEISCH
        int searchNumber = Convert.ToInt32(Console.ReadLine());

        Console.WriteLine();
        beisch.Search(searchNumber, tableSize);

        Console.WriteLine();

        static List<int> GenerateRandomNumbers(int min, int max, int count)
        {
            Random random = new Random();
            List<int> numbers = new List<int>();

            for (int i = 0; i < count; i++)
            {
                int randomNumber = random.Next(min, max);
                numbers.Add(randomNumber);
            }

            return numbers;
        }

        static int FindPrimeFinalSize(int number)
        {
            int finalSize = 0;
            bool found = false;

            for (int i = number; !found; i++)
            {
                if (IsPrime(i))
                {
                    double packingFactor = ((double)number / i) * 100;
                    if (packingFactor >= 65 && packingFactor <= 95)
                    {
                        finalSize = i;
                        found = true;
                        Console.WriteLine("packing factor: " + packingFactor);
                    }
                }
            }

            return finalSize;
        }

        static bool IsPrime(int number)
        {
            if (number <= 1)
                return false;

            if (number == 2)
                return true;

            if (number % 2 == 0)
                return false;

            for (int i = 3; i <= Math.Sqrt(number); i += 2)
            {
                if (number % i == 0)
                    return false;
            }

            return true;
        }
    }
}
