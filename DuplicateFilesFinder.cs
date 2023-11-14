using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ImageApp
{
    internal class DuplicateFilesFinder
    {
      internal static void FindDuplicatePhotos(string directoryPath)
        {
            Dictionary<string, List<string>> hashDictionary = new Dictionary<string, List<string>>();

            ProcessDirectory(directoryPath, hashDictionary);

            foreach (var kvp in hashDictionary)
            {
                if (kvp.Value.Count > 1)
                {
                    Console.WriteLine($"Duplicate photos with hash {kvp.Key}:");
                    foreach (string imagePath in kvp.Value)
                    {
                        Console.WriteLine($"  {imagePath}");
                    }
                    Console.WriteLine();
                }
            }
        }

        static void ProcessDirectory(string directory, Dictionary<string, List<string>> hashDictionary)
        {
            string[] imageFiles = Directory.GetFiles(directory);

            foreach (string imagePath in imageFiles)
            {
                string hash = GetFileHash(imagePath);

                if (hashDictionary.ContainsKey(hash))
                {
                    hashDictionary[hash].Add(imagePath);
                }
                else
                {
                    hashDictionary.Add(hash, new List<string> { imagePath });
                }
            }

            string[] subDirectories = Directory.GetDirectories(directory);
            foreach (string subDirectory in subDirectories)
            {
                ProcessDirectory(subDirectory, hashDictionary);
            }
        }

        static string GetFileHash(string filePath)
        {
            using (var md5 = MD5.Create())
            {
                using (var stream = File.OpenRead(filePath))
                {
                    byte[] hashBytes = md5.ComputeHash(stream);
                    return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
                }
            }
        }
    }
}
