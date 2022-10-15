using System;
using System.IO;
using System.Collections.Generic;

namespace Task1
{
    public class Program
    {
        private static void CreateAndFillFiles()
        {
            Random randomGenerator = new Random();

            for (int fileIndex = 10; fileIndex < 30; fileIndex++)
            {
                FileStream fileStream =
                        new FileStream($"{fileIndex}.txt", FileMode.OpenOrCreate, FileAccess.Write);
                fileStream.SetLength(0);
                StreamWriter streamWriter = new StreamWriter(fileStream);

                streamWriter.WriteLine(randomGenerator.Next(-10000, 10000));                // ---- min / max
                streamWriter.Write(randomGenerator.Next(-10000, 10000));                    // ---- min / max

                streamWriter.Close();
                fileStream.Close();
            }
        }

        private static void CalculateProducts(ref List<int> listOfProducts)
        {
            try
            {
                FileStream fileStreamNoFile =
                    new FileStream($"no_file.txt", FileMode.OpenOrCreate, FileAccess.Write);
                fileStreamNoFile.SetLength(0);
                fileStreamNoFile.Close();
            }
            catch (Exception)
            {
                Console.WriteLine("Error occured when creating / clearing file no_file.txt");
                throw new Exception("Error occured when creating / clearing file no_file.txt");
            }

            try
            {
                FileStream fileStreamBadData =
                    new FileStream($"bad_data.txt", FileMode.OpenOrCreate, FileAccess.Write);
                fileStreamBadData.SetLength(0);
                fileStreamBadData.Close();
            }
            catch (Exception)
            {
                Console.WriteLine("Error occured when creating / clearing file bad_data.txt");
                throw new Exception("Error occured when creating / clearing file bad_data.txt");
            }

            try
            {
                FileStream fileStreamOverflow =
                    new FileStream($"overflow.txt", FileMode.OpenOrCreate, FileAccess.Write);
                fileStreamOverflow.SetLength(0);
                fileStreamOverflow.Close();
            }
            catch (Exception)
            {
                Console.WriteLine("Error occured when creating / clearing file overflow.txt");
                throw new Exception("Error occured when creating / clearing file overflow.txt");
            }

            for (int fileIndex = 10; fileIndex < 30; fileIndex++)
            {
                FileStream fileStream;
                StreamReader streamReader;

                try
                {
                    fileStream =
                        new FileStream($"{fileIndex}.txt", FileMode.Open, FileAccess.Read);
                    streamReader = new StreamReader(fileStream);
                }
                catch (FileNotFoundException)
                {
                    Console.WriteLine($"File {fileIndex}.txt does not exist");
                    
                    try
                    {
                        FileStream fileStreamNoFile =
                            new FileStream($"no_file.txt", FileMode.Append, FileAccess.Write);
                        StreamWriter streamWriterNoFile = new StreamWriter(fileStreamNoFile);

                        streamWriterNoFile.WriteLine($"{fileIndex}.txt");

                        streamWriterNoFile.Close();
                        fileStreamNoFile.Close();
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("Error occured when opening / writing into file no_file.txt");
                        throw new Exception("Error occured when opening / writing into file no_file.txt");
                    }

                    continue;
                }
                catch (Exception)
                {
                    Console.WriteLine($"General error occured when opening file {fileIndex}.txt");
                    continue;
                }

                int number1;
                int number2;

                try
                {
                    number1 = int.Parse(streamReader.ReadLine());
                    number2 = int.Parse(streamReader.ReadLine());
                }
                catch (Exception)
                {
                    Console.WriteLine($"Error occured when trying to read / parse first two lines from file {fileIndex}.txt");

                    try
                    {
                        FileStream fileStreamBadData =
                            new FileStream($"bad_data.txt", FileMode.Append, FileAccess.Write);
                        StreamWriter streamWriterBadData = new StreamWriter(fileStreamBadData);

                        streamWriterBadData.WriteLine($"{fileIndex}.txt");

                        streamWriterBadData.Close();
                        fileStreamBadData.Close();
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("Error occured when opening / writing into file bad_data.txt");
                        throw new Exception("Error occured when opening / writing into file bad_data.txt");
                    }

                    continue;
                }

                int productCurrent;

                try
                {
                    checked
                    {
                        productCurrent = number1 * number2;
                    }
                }
                catch (Exception)
                {
                    Console.WriteLine("Multiplication of numbers resulted in type overflow");

                    try
                    {
                        FileStream fileStreamOverflow =
                            new FileStream($"overflow.txt", FileMode.Append, FileAccess.Write);
                        StreamWriter streamWriterOverflow = new StreamWriter(fileStreamOverflow);

                        streamWriterOverflow.WriteLine($"{fileIndex}.txt");

                        streamWriterOverflow.Close();
                        fileStreamOverflow.Close();
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("Error occured when opening / writing into file overflow.txt");
                        throw new Exception("Error occured when opening / writing into file overflow.txt");
                    }

                    continue;
                }

                listOfProducts.Add(productCurrent);

                streamReader.Close();
                fileStream.Close();
            }
        }

        private static void SimpleCalculationsAndOutput(List<int> listOfProducts)
        {
            if (listOfProducts.Count == 0)
            {
                Console.WriteLine("Further calculations do not make sense, because there are no products of the numbers in the list");
                throw new Exception("Further calculations do not make sense, because there are no products of the numbers in the list");
            }

            Console.WriteLine("Calculated products:");
            foreach (int item in listOfProducts)
            {
                Console.WriteLine($"{item} ");
            }

            Console.WriteLine();

            Console.WriteLine($"Number of calculated products: {listOfProducts.Count}");

            Console.WriteLine();

            int sumOfProducts = 0;
            try
            {
                checked
                {
                    foreach (int item in listOfProducts)
                    {
                        sumOfProducts += item;
                    }
                }

                Console.Write($"Sum of calculated products: {sumOfProducts} \n");
            }
            catch (Exception)
            {
                Console.WriteLine("Summation of products resulted in type overflow. Further calculations do not make sense");
                throw new Exception("Summation of products resulted in type overflow. Further calculations do not make sense");
            }

            Console.WriteLine();

            double arithmeticMean = sumOfProducts * 1.0 / listOfProducts.Count;
            Console.WriteLine($"Arithmetic mean of calculated products: {arithmeticMean}");
        }

        private void Main()
        {
            CreateAndFillFiles();

            List<int> listOfProducts = new List<int>();
            CalculateProducts(ref listOfProducts);

            Console.WriteLine();

            SimpleCalculationsAndOutput(listOfProducts);

            Console.ReadLine();
        }
    }
}

// create and - / fill withrandom numbers 20 files
// calculate products of each pare of numbers
// calculate sum producs
// calculate arithmetic mean of products
// create and - / fill files: no_file.txt, bad_data.txt. overflow.txt
