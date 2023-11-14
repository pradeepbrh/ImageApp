using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XSystem.Security.Cryptography;

namespace ImageApp
{
    internal class FileProcess
    {
        internal void FileRead(int option)
        {

            string _mainDirectory = Constant.MainDirectoryImage;
            if (option == 2)
                _mainDirectory = Constant.MainDirectoryVideo;
            List<string> _ImageDirectory = getdir(_mainDirectory);
            if (option == 2)
            {
                string _inputVideoPath = Constant.MainDirectoryImage;
                _ImageDirectory.AddRange(getdir(_inputVideoPath));
            }
            // _ImageDirectory = _ImageDirectory.Where(x =>x.Contains(@"\\INPUT") && !x.Contains("Videos") && !x.Contains("NOT_Processed")).ToList();
            foreach (string dir in _ImageDirectory)
            {
                if (dir.Contains("INPUT") || !dir.Contains("Videos") && !dir.Contains("NOT_Processed"))
                {
                    if (Directory.Exists(dir))
                    {
                        string[] _totalFiles = Directory.GetFiles(dir);

                        string[] _filenames = _totalFiles.Where(x => Path.GetExtension(x).ToLower() != ".json").ToArray();

                        string[] _jsonFilePath = _totalFiles.Where(x => Path.GetExtension(x).ToLower() == ".json").ToArray();
                        if (option == 2)
                            _filenames = _filenames.Where(x => Constant.videoextensionList.Contains(Path.GetExtension(x).ToLower())).ToArray();
                        foreach (string _file in _filenames)
                        {
                            ProcessFile(_file, option);
                            Console.WriteLine(Path.GetFileName(_file) + " Processed ..");
                        }

                        DeleteJsonFiles(_jsonFilePath.ToList());
                    }
                }

            }
            //List<string> _ImageDirectoryoutputpath = getdir(Constant.OUTPUTPATH);

            //foreach (string _dir in _ImageDirectoryoutputpath)
            //{
            //    DeleteDuplicateImages(_dir);
            //}
            //     DuplicateFilesFinder.FindDuplicatePhotos(Constant.OUTPUTPATH);

            Console.WriteLine("Press Key to stop");
            Console.ReadLine();
        }
        private void DeleteJsonFiles(List<string> _fileNames)
        {
            foreach (string file in _fileNames)
            {

                File.Delete(file);
            }
        }
        private void ProcessFile(string FileNameSource, int Option)
        {


            string _path = GetPathForImageMovement(Path.GetFileNameWithoutExtension(FileNameSource), Option);
            if (!string.IsNullOrEmpty(_path))
            {
                _path = Constant.OUTPUTPATH + _path;

            }
            else
            {
                _path = Constant.OUTPUTPATH + "NOT_Processed";
            }
            if (!Directory.Exists(_path))
            {
                Directory.CreateDirectory(_path);
            }
            MoveFile(_path + "\\" + Path.GetFileName(FileNameSource), FileNameSource);
        }
        private string GetPathForImageMovement(string FileName, int option)
        {
            string _filepath = string.Empty;

            char _seperator = '_';
            if (FileName.Contains("-WA"))
                _seperator = '-';
            string[] namearray = FileName.Split(_seperator);
            if (namearray.Length >= 3)
            {
                if (int.TryParse(namearray[1], out int dateFormat) && namearray[1].Length == 8)
                {
                    string _year = namearray[1].Substring(0, 4);
                    string _month = namearray[1].Substring(4, 2);
                    string _date = namearray[1].Substring(6, 2);
                    // Validator validator = new Validator();

                    //if (!Validator.ValidateDate(Convert.ToInt32(_date), Convert.ToInt32(_month), Convert.ToInt32(_year)))
                    //{
                    //    FileDetails(FileName);

                    //}



                    if (option == 2)
                        _filepath = _year + "\\" + "Videos" + "\\" + _month;
                    else
                        _filepath = _year + "\\" + _month;// + "\\" + _date;

                }
                if (FileName.Contains("-WA"))
                {
                    _filepath = "WhatsApp" + "\\" + _filepath;
                }
            }
            if (FileName.ToLower().Contains("screenshot"))
            {
                int indexdash = FileName.IndexOf('_');
                string datefromFile = FileName.Substring(indexdash + 1, 10);
                namearray = datefromFile.Split('-');
                if (namearray.Length == 3)
                {
                    string _year = namearray[0];
                    string _month = namearray[1];
                    string _date = namearray[2];
                    _filepath = "Screenshots" + "\\" + _year + "\\" + _month;
                }

            }
            return _filepath;
        }

        public static void FileDetails(string filePath)
        {
            try
            {
                DateTime creationDate = GetFileCreationDate(filePath);

                Console.WriteLine($"File: {filePath}");
                Console.WriteLine($"Creation Date: {creationDate}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }

        public static DateTime GetFileCreationDate(string filePath)
        {
            FileInfo fileInfo = new FileInfo(filePath);

            // Access the creation time property
            DateTime creationDate = fileInfo.CreationTime;

            return creationDate;
        }


        private void MoveFile(string FilePathTo, string FilePathFrom)
        {
            if (!File.Exists(FilePathTo))
            {
                File.Move(FilePathFrom, FilePathTo);
            }
            else
            {
                string _dirpath = Constant.OUTPUTPATH + "NOT_Processed" + "\\" + "Duplicate" + "\\";
                if (!Directory.Exists(_dirpath))
                {
                    Directory.CreateDirectory(_dirpath);
                }
                if (File.Exists(_dirpath + Path.GetFileName(FilePathTo)))
                {
                    File.Delete(_dirpath + Path.GetFileName(FilePathTo));

                }
                File.Move(FilePathFrom, _dirpath + Path.GetFileName(FilePathTo));
            }
        }
        private static void DuplicateMoveFile(string FilePathTo, string FilePathFrom)
        {
            try
            {
                //if (!File.Exists(FilePathTo))
                //{
                //    File.Move(FilePathFrom, FilePathTo);
                //}
                //else
                //{
                string _dirpath = Constant.OUTPUTPATH + "NOT_Processed" + "\\" + "Duplicate" + "\\" + "ImageDup" + "\\";
                if (!Directory.Exists(_dirpath))
                {
                    Directory.CreateDirectory(_dirpath);
                }
                if (File.Exists(_dirpath + Path.GetFileName(FilePathTo)))
                {
                    File.Delete(_dirpath + Path.GetFileName(FilePathTo));

                }
                File.Move(FilePathFrom, _dirpath + Path.GetFileName(FilePathTo));
                // }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        private void DirList()
        {
            List<string> defaultPath = new List<string> { @"D:\Pradeep\CUSTOM\TestPath" };
            List<string> newPaths = getdir(@"D:\Pradeep\CUSTOM\TestPath");//GetDirectoryList(defaultPath);
            foreach (string s in newPaths)
            {
                Console.WriteLine(s);
                Thread.Sleep(1000);
            }
            Console.ReadLine();
        }
        private List<string> getdir(string path)
        {
            string[] _s = Directory.GetDirectories(path, "*", SearchOption.AllDirectories);
            return _s.ToList();
        }




        static void DeleteDuplicateImages(string directoryPath)
        {
            Dictionary<string, List<string>> hashDictionary = new Dictionary<string, List<string>>();

            string[] imageFiles = Directory.GetFiles(directoryPath); // You can adjust the file extension as needed

            foreach (string imagePath in imageFiles)
            {
                string hash = CalculateSHA256(imagePath);

                if (hashDictionary.ContainsKey(hash))
                {
                    // Duplicate found
                    Console.WriteLine($"Duplicate found: {imagePath}");
                    foreach (string duplicateImagePath in hashDictionary[hash])
                    {
                        // Delete duplicate file
                        //   File.Delete(duplicateImagePath);

                        DuplicateMoveFile(duplicateImagePath, duplicateImagePath);

                        Console.WriteLine($"Deleted: {duplicateImagePath}");
                    }
                }
                else
                {
                    // Add the hash to the dictionary
                    if (!hashDictionary.ContainsKey(hash))
                    {
                        hashDictionary[hash] = new List<string>();
                    }
                    hashDictionary[hash].Add(imagePath);
                }
            }

            Console.WriteLine("Duplicate detection and deletion complete.");
        }

        static string CalculateSHA256(string filePath)
        {
            using (FileStream stream = File.OpenRead(filePath))
            {
                SHA256Managed sha = new SHA256Managed();
                byte[] hash = sha.ComputeHash(stream);
                return BitConverter.ToString(hash).Replace("-", String.Empty);
            }
        }
    }
}
