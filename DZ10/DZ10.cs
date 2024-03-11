namespace DZ10
{
    /// <summary>
    /// 10я домашка по OUTUS. Файлы 
    /// </summary>
    class DZ10
    {
        /// <summary>
        /// Тут есть опиисание метода по созданию файлов.
        /// </summary>
        /// <param name="args"></param>
        public static async Task Main(string[] args)
        {
            CancellationTokenSource cancelTokenSource = new CancellationTokenSource();
            CancellationToken token = cancelTokenSource.Token;
            bool debug = false;
            List<string> listFolders = new List<string>();
            listFolders.Add(@"C:\otus\TestDir1");
            listFolders.Add(@"C:\otus\TestDir3");

            List<string> listFiles = new List<string>();
            for (int i = 1; i<11; i++)
            {
                listFiles.Add($"File_{i}");
            }

            Console.WriteLine($"START WORK WITH DIR.. ");
            FolderWorker fw = new FolderWorker(listFolders);
            var t = Task.Run (() => fw.CreateDirAsync(debug, token), token);
            Console.WriteLine($"CreateDirAsync completed work.. => ");

            Task fileTask = null;
            
            while (true)
            {
                if (t.IsCompleted )
                {
                    if (fileTask == null)
                    {
                        Console.WriteLine($" => START WORK WITH FILES.. ");
                        foreach (string folder in listFolders)
                        {
                            Console.WriteLine($"Create file in folder {folder}");
                            foreach (string name in listFiles)
                            {
                                fileTask = Task.Run(() => FileCreator.CreateFileAsync(debug, folder, name, token));
                            }
                            
                        }
                    }
                    if (fileTask.IsCompleted)
                    {
                        Console.WriteLine($"File were created.");
                        break;
                    }
                }
            }

            Console.WriteLine($"\t\t\t * * * LIST OF FOLDERS * * * ");
            string[] dirs = Directory.GetDirectories($"C:\\otus\\");
            foreach (string fldr in dirs)
            {
                Console.WriteLine(fldr);
                string[] files = Directory.GetFiles(fldr);
                Console.WriteLine($"\tLIST OF FILES: ");
                foreach (string fls in files)
                {
                    Console.WriteLine($"\t\tFILE:{fls}; VALUE = {File.ReadAllText(fls)}");
                }
            }

            Console.ReadLine();
        }
    }
}