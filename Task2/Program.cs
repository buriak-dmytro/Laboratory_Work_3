using System;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using System.Collections.Generic;

namespace Task2
{
    public class Program
    {
        private static string[] extensions =
            { "jpg", "jpeg", "png", "gif", "tiff", "tif", "bmp", "svg", "exif" };

        private static void GetListOfImageFiles(ref List<string> listOfImageFiles)
        {
            Console.Write("Enter directory path: ");
            string directoryPath = Console.ReadLine();

            foreach (string item in extensions)
            {
                try
                {
                    listOfImageFiles.AddRange(
                        Directory.GetFiles(
                            directoryPath, $"*.{item}", SearchOption.TopDirectoryOnly));
                }
                catch (Exception)
                {
                    Console.WriteLine($"Error occured when processing \"{item}\" extension request");
                    throw new Exception($"Error occured when processing \"{item}\" extension request");
                }
            }
        }

        private static void OutputFoundImages(List<string> listOfImageFiles)
        {
            if (listOfImageFiles.Count == 0)
            {
                Console.WriteLine("Images are not found in target directory");
                throw new Exception("Images are not found in target directory");
            }

            foreach (string item in listOfImageFiles)
            {
                Console.WriteLine($"File \"{item}\" was founded");
            }
        }

        private static void CreateMirroredImages(List<string> listOfImageFiles)
        {
            for (int index = 0; index < listOfImageFiles.Count; index++)
            {
                string path = listOfImageFiles[index];

                Bitmap bitmapNormal;
                Bitmap bitmapMirrored;
                int indexLastDot;
                string nameNewImage;

                try
                {
                    bitmapNormal = new Bitmap(path);
                }
                catch (Exception)
                {
                    Console.WriteLine($"Image \"{path}\" cannot be loaded correctly into current program");
                    continue;
                }

                try
                {
                    bitmapMirrored = (Bitmap)bitmapNormal.Clone();
                    bitmapMirrored.RotateFlip(RotateFlipType.RotateNoneFlipX);
                }
                catch (Exception)
                {
                    Console.WriteLine($"Image \"{path}\" cannot be modified correctly");
                    continue;
                }

                try
                {
                    indexLastDot = listOfImageFiles[index].LastIndexOf('.');
                    nameNewImage = listOfImageFiles[index].Substring(0, indexLastDot) + "-mirrored.gif";
                }
                catch (Exception)
                {
                    Console.WriteLine($"Error occured when creating name for modified version of image \"{path}\"");
                    continue;
                }

                try
                {
                    bitmapMirrored.Save(nameNewImage, ImageFormat.Gif);
                    Console.WriteLine($"File \"{nameNewImage}\" was created");
                }
                catch (Exception)
                {
                    Console.WriteLine($"Image \"{path}\" cannot be saved correctly");
                    continue;
                }
            }
        }

        private static void Main()
        {
            List<string> listOfImageFiles = new List<string>();

            GetListOfImageFiles(ref listOfImageFiles);

            OutputFoundImages(listOfImageFiles);

            Console.WriteLine();

            CreateMirroredImages(listOfImageFiles);

            Console.WriteLine();

            Console.WriteLine("Execution is complete");

            Console.ReadLine();
        }
    }
}

// get list of image files from given directory path
// create vertically mirrored images from received image files
