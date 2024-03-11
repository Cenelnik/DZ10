
using System.Threading.Tasks;

namespace DZ10
{
    internal class FolderWorker
    {
        private List<string> _foldersList;


        internal FolderWorker(List<string> folders)
        {
            _foldersList = folders;
        }

        internal async void CreateDirAsync(bool debug, CancellationToken t)
        {
            try
            {
                if(debug) Console.WriteLine($"CreateDirAsync[{Thread.CurrentThread.ManagedThreadId}]: Start working CreateDirAsync");
                await Task.Run(() =>
                {
                    foreach (string folder in _foldersList)
                    {
                        if (!t.IsCancellationRequested)
                        {
                            if (!Directory.Exists(folder))
                            {
                                if (debug) Console.WriteLine($"CreateDirAsync[{Thread.CurrentThread.ManagedThreadId}]: Create Directory {folder}.");
                                DirectoryInfo df = new DirectoryInfo(folder);
                                df.Create();
                            }
                            else
                            {
                                if (debug) Console.WriteLine($"CreateDirAsync[{Thread.CurrentThread.ManagedThreadId}]: Directory {folder} was created earlier ");
                                
                            }
                        }
                    }
                }, t);
            }
            catch (Exception ex)
            {
                if (debug) Console.WriteLine($"MSG = {ex.Message}, TRACE = {ex.StackTrace}");
            }
        }
    }
}
