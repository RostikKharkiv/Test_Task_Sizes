using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test_Task_Sizes
{
    internal class CalculateFolderSize
    {
        private char _symbol = '-';
        private int _symbolCount = 0;

        private bool _outputFlag;
        private string _directoryPath;
        private string _outputFilePath;
        private bool _humanReadFlag;

        private Format _format;
        private OutputTo _outputTo;

        private string[]? _directories;
        private string[]? _files;

        public IEnumerable<string>? directories => _directories;
        public IEnumerable<string>? files => _files;

        StringBuilder sizeInfoMain = new StringBuilder();
        StringBuilder sizeInfoParent = new StringBuilder();
        StringBuilder sizeInfoChild = new StringBuilder();

        public CalculateFolderSize(bool outputFlag = false, string directoryPath = "", string outputFilePath = $"default", bool humanReadFlag = false)
        {
            if (directoryPath.Equals(""))
            {
                _directoryPath = Directory.GetCurrentDirectory();
            }
            else
            {
                _directoryPath = directoryPath;
            }
            if (outputFilePath.Equals($"default"))
            {
                _outputFilePath = $"Sizes-{DateTime.Now.Year}-{DateTime.Now.Month}-{DateTime.Now.Day}.txt";
            }
            else
            {
                _outputFilePath = outputFilePath;
            }

            _outputFlag = outputFlag;

            _humanReadFlag = humanReadFlag;

            OutputFormatInitialize();

            OutputInitialize();
        }

        private void OutputFormatInitialize()
        {
            if (_humanReadFlag)
            {
                _format = new HumanFormat();
            }
            else
            {
                _format = new DefaultFormat();
            }
        }

        private void OutputInitialize()
        {
            if (_outputFlag)
            {
                _outputTo = new WhithoutConsoleOutput();
            }
            else
            {
                _outputTo = new DefaultOutput();
            }
        }

        public void StartCalculate()
        {
            CheckMainDirectory();

            sizeInfoMain.Append($"{_symbol} {_directoryPath} ({_format.OutputFormat(CheckDirectories(_symbolCount, _directoryPath))}){Environment.NewLine}");

            sizeInfoMain.Append(sizeInfoParent);

            foreach (var file in files)
            {
                FileInfo fileInfo = new FileInfo(file);
                sizeInfoMain.AppendLine($"{new string(_symbol, 2)} {fileInfo.Name} ({_format.OutputFormat(fileInfo.Length)})");
            }

            _outputTo.Output(_outputFilePath, sizeInfoMain.ToString());
        }

        private void CheckMainDirectory()
        {
            if (!Directory.Exists(_directoryPath))
            {
                Console.WriteLine("Папка по указанному пути не существует");
            }
            else
            {
                _directories = Directory.GetDirectories(_directoryPath);
                _files = Directory.GetFiles(_directoryPath);
            }
        }

        private long CheckDirectories(int symbolCount, string currentDirectory)
        {
            long currentDirectorySize = 0;

            int indexInsert = sizeInfoParent.Length;

            DirectoryInfo currentDirectoryInfo = new DirectoryInfo(currentDirectory);

            symbolCount += 4;

            IEnumerable<string> filesInCurrentDirectory = Directory.GetFiles(currentDirectory).Reverse();

            foreach (var file in filesInCurrentDirectory)
            {
                FileInfo fileInfo = new FileInfo(file);
                //sizeInfoChild.AppendLine($"{new string(_symbol, symbolCount)} {fileInfo.Name} ({fileInfo.Length} bytes)");
                currentDirectorySize += fileInfo.Length;
            }

            symbolCount -= 2;


            IEnumerable<string> directoriesInCurrentDirectory = Directory.GetDirectories(currentDirectory);

            foreach (var dir in directoriesInCurrentDirectory)
            {
                currentDirectorySize += CheckDirectories(symbolCount, dir);
            }

            if (currentDirectory == _directoryPath)
            {
                return currentDirectorySize;
            }

            symbolCount -= 2;

            string insertInfo = $"{new string(_symbol, symbolCount)} {currentDirectoryInfo.Name} " +
                $"({_format.OutputFormat(currentDirectorySize)}){Environment.NewLine}";

            sizeInfoParent.Insert(indexInsert, insertInfo);

            symbolCount += 2;

            foreach (var file in filesInCurrentDirectory)
            {
                FileInfo fileInfo = new FileInfo(file);
                sizeInfoChild.AppendLine($"{new string(_symbol, symbolCount)} {fileInfo.Name} ({_format.OutputFormat(fileInfo.Length)} bytes)");
            }

            sizeInfoParent.Append(sizeInfoChild);

            sizeInfoChild.Clear();

            symbolCount -= 2;

            return currentDirectorySize;
        }
    }
}
