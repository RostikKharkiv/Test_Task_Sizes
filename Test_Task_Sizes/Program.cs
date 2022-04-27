using System;

namespace Test_Task_Sizes 
{ 
    internal class Program
    {
        private static bool _outputFlag;
        private static string _directoryPath = string.Empty;
        private static string _outputFilePath = string.Empty;
        private static bool _humanreadFlag;
        private static string _info = $"Входная строка должна иметь вид:{Environment.NewLine}" +
                $"[-q | --quite] [-p <DirectoryPath>| --path <DirectoryPath>] " +
                $"[-o <FilePath> | --output <FilePath>] [-h | --humanread]{Environment.NewLine}" +
                $"Параметры можно указывать в любом порядке{Environment.NewLine}" +
                $"Где:{Environment.NewLine}" +
                $"-q (--quite) признак вывода сообщений в стандартный поток вывода " +
                $"(если указана, то не выводить лог в консоль. Только в файл){Environment.NewLine}" +
                $"-p (--path) - путь к папке для обхода (по-умолчанию текущая папка вызова программы);{Environment.NewLine}" +
                $"-o (--output) - путь к тестовому файлу, куда записать результаты выполнения расчёта " +
                $"(по-умолчанию файл sizes-YYYY-MM-DD.txt в текущей папке вызова программы){Environment.NewLine}" +
                $"-h (--humanread) - признак формирования размеров файлов в человекочитаемой форме " +
                $"(размеры до 1Кб указывать в байтах, размеры до 1Мб в килобайтах с 2 знаками после запятой, " +
                $"размеры до 1Гб в мегабайтах с 2 знаками после запятой, размеры до 1Тб - в Гб с 2 знаками после запятой){Environment.NewLine}" +
                $@"Путь к папке, пример: C:\Test\Test2\Test3{Environment.NewLine}";

        private static CalculateFolderSize _calculateFolderSize = new CalculateFolderSize();

        static void Main(string[] args)
        {
            Start();

            _calculateFolderSize.StartCalculate();
        }

        static void Start()
        {
            Console.WriteLine("Введите -help для получения справки");

            string[] args = Console.ReadLine().Trim().Split();

            if (args.Length == 0)
            {
                _calculateFolderSize = new CalculateFolderSize();
                return;
            }

            if (args.Length > 6)
            {
                throw new ArgumentException($"{Environment.NewLine}" +
                    $"Некорректное количество параметров, введите -help для получения справки");
            }

            CheckArguments(args);


        }

        static void CheckArguments(string[] args)
        {
            int qCount = 0;
            int pCount = 0;
            int oCount = 0;
            int hCount = 0;

            for (int i = 0; i < args.Length; i++)
            {
                switch (args[i])
                {
                    case "-q":
                        if (qCount > 0)
                        {
                            throw new ArgumentException($"{Environment.NewLine}" +
                                $"Количество параметров -q (--quite) не может быть больше одного");
                        }
                        _outputFlag = true;
                        qCount++;
                        break;
                    case "--quite":
                        if (qCount > 0)
                        {
                            throw new ArgumentException($"{Environment.NewLine}" +
                                $"Количество параметров -q (--quite) не может быть больше одного");
                        }
                        _outputFlag = true;
                        qCount++;
                        break;
                    case "-p":
                        if (pCount > 0)
                        {
                            throw new ArgumentException($"{Environment.NewLine}" +
                                $"Количество параметров -p (--path) не может быть больше одного");
                        }
                        if (i + 1 < args.Length)
                        {
                            i++;
                            _directoryPath = args[i];
                            pCount++;
                        } 
                        else
                        {
                            throw new ArgumentException($"{Environment.NewLine}Некорректное использование параметра -p (--path)" +
                                @$"{Environment.NewLine}Пример использования: -p C:\Test\Test2 {Environment.NewLine}");
                        }
                        break;
                    case "--path":
                        if (pCount > 0)
                        {
                            throw new ArgumentException($"{Environment.NewLine}" +
                                $"Количество параметров -p (--path) не может быть больше одного");
                        }
                        if (i + 1 < args.Length)
                        {
                            i++;
                            _directoryPath = args[i];
                            pCount++;
                        }
                        else
                        {
                            throw new ArgumentException($"{Environment.NewLine}Некорректное использование параметра -p (--path)" +
                                @$"{Environment.NewLine}Пример использования: -p C:\Test\Test2{Environment.NewLine}");
                        }
                        break;
                    case "-o":
                        if (oCount > 0)
                        {
                            throw new ArgumentException($"{Environment.NewLine}" +
                                $"Количество параметров -o (--output) не может быть больше одного");
                        }
                        if (i + 1 < args.Length)
                        {
                            i++;
                            _outputFilePath = args[i];
                            oCount++;
                        }
                        else
                        {
                            throw new ArgumentException($"{Environment.NewLine}Некорректное использование параметра -o (--output)" +
                                @$"{Environment.NewLine}Пример использования: -p C:\Test\Test2\SizesInfo.txt{Environment.NewLine}");
                        }
                        break;
                    case "--output":
                        if (pCount > 0)
                        {
                            throw new ArgumentException($"{Environment.NewLine}" +
                                $"Количество параметров -o (--output) не может быть больше одного");
                        }
                        if (i + 1 < args.Length)
                        {
                            i++;
                            _outputFilePath = args[i];
                            oCount++;
                        }
                        else
                        {
                            throw new ArgumentException($"{Environment.NewLine}Некорректное использование параметра -o (--output)" +
                                @$"{Environment.NewLine}Пример использования: -p C:\Test\Test2\SizesInfo.txt{Environment.NewLine}");
                        }
                        break;
                    case "-h":
                        if (hCount > 0)
                        {
                            throw new ArgumentException($"{Environment.NewLine}" +
                                $"Количество параметров -h (--humanread) не может быть больше одного");
                        }
                        _humanreadFlag = true;
                        hCount++;
                        break;
                    case "--humanread":
                        if (hCount > 0)
                        {
                            throw new ArgumentException($"{Environment.NewLine}" +
                                $"Количество параметров -h (--humanread) не может быть больше одного");
                        }
                        _humanreadFlag = true;
                        hCount++;
                        break;
                    case "-help":
                        Console.WriteLine(_info);
                        Start();
                        break;
                    default:
                        throw new ArgumentException($"{Environment.NewLine}" +
                            $"Некорректные входные параметры, введите -help для получения справки");
                }
            }

            int argsCount = qCount + pCount + oCount + hCount;

            // Эта табличка для лучшей читабельности

            switch (argsCount)
            {
                case 4:
                    _calculateFolderSize = new CalculateFolderSize(_outputFlag, _directoryPath, _outputFilePath, _humanreadFlag);
                    break;
                case 3 when qCount == 1 && pCount == 1 && oCount == 1 && hCount == 0:
                    _calculateFolderSize = new CalculateFolderSize(_outputFlag, _directoryPath, _outputFilePath);
                    break;
                case 3 when qCount == 1 && pCount == 1 && oCount == 0 && hCount == 1:
                    _calculateFolderSize = new CalculateFolderSize(_outputFlag, _directoryPath, "default", _humanreadFlag);
                    break;
                case 2 when qCount == 1 && pCount == 1 && oCount == 0 && hCount == 0:
                    _calculateFolderSize = new CalculateFolderSize(_outputFlag, _directoryPath);
                    break;
                case 3 when qCount == 1 && pCount == 0 && oCount == 1 && hCount == 1:
                    _calculateFolderSize = new CalculateFolderSize(_outputFlag, "", _outputFilePath, _humanreadFlag);
                    break;
                case 2 when qCount == 1 && pCount == 0 && oCount == 1 && hCount == 0:
                    _calculateFolderSize = new CalculateFolderSize(_outputFlag, "", _outputFilePath);
                    break;
                case 2 when qCount == 1 && pCount == 0 && oCount == 0 && hCount == 1:
                    _calculateFolderSize = new CalculateFolderSize(_outputFlag, "", "default", _humanreadFlag);
                    break;
                case 1 when qCount == 1 && pCount == 0 && oCount == 0 && hCount == 0:
                    _calculateFolderSize = new CalculateFolderSize(_outputFlag);
                    break;
                case 3 when qCount == 0 && pCount == 1 && oCount == 1 && hCount == 1:
                    _calculateFolderSize = new CalculateFolderSize(_outputFlag, _directoryPath, _outputFilePath, _humanreadFlag);
                    break;
                case 2 when qCount == 0 && pCount == 1 && oCount == 1 && hCount == 0:
                    _calculateFolderSize = new CalculateFolderSize(_outputFlag, _directoryPath, _outputFilePath);
                    break;
                case 2 when qCount == 0 && pCount == 1 && oCount == 0 && hCount == 1:
                    _calculateFolderSize = new CalculateFolderSize(_outputFlag, _directoryPath, "default", _humanreadFlag);
                    break;
                case 1 when qCount == 0 && pCount == 1 && oCount == 0 && hCount == 0:
                    _calculateFolderSize = new CalculateFolderSize(_outputFlag, _directoryPath);
                    break;
                case 2 when qCount == 0 && pCount == 0 && oCount == 1 && hCount == 1:
                    _calculateFolderSize = new CalculateFolderSize(_outputFlag, "", _outputFilePath, _humanreadFlag);
                    break;
                case 1 when qCount == 0 && pCount == 0 && oCount == 1 && hCount == 0:
                    _calculateFolderSize = new CalculateFolderSize(_outputFlag, "", _outputFilePath);
                    break;
                case 1 when qCount == 0 && pCount == 0 && oCount == 0 && hCount == 1:
                    _calculateFolderSize = new CalculateFolderSize(_outputFlag, "", _outputFilePath, _humanreadFlag);
                    break;
            }

        }
    }
}