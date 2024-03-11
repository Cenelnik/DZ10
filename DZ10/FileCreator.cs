using System.IO;
using System.Security.AccessControl;


namespace DZ10
{
    internal class FileCreator
    {

        internal static async void CreateFileAsync(bool debug, string path, string nameFile, CancellationToken t)
        { 
            try
            {
                await Task.Run( () =>
                {
                    if (debug) Console.WriteLine($"CreateFileAsync[{Thread.CurrentThread.ManagedThreadId}]: Start working CreateFileAsync");
                    if (!t.IsCancellationRequested)
                    {
                        if (!File.Exists($"{path}\\{nameFile}"))
                        {
                            if (debug) Console.WriteLine($"CreateFileAsync[{Thread.CurrentThread.ManagedThreadId}]: Creating file {path}\\{nameFile}..");
                            File.WriteAllText($"{path}\\{nameFile}.txt", $"{DateTime.Now}");
                            if (debug) Console.WriteLine($"CreateFileAsync[{Thread.CurrentThread.ManagedThreadId}]: Creating file {path}\\{nameFile} was completed.");

                            if (debug) Console.WriteLine($"CreateFileAsync[{Thread.CurrentThread.ManagedThreadId}]: Write DateTime in the file[{nameFile}].");
                        }
                        else
                        {
                            if (debug) Console.WriteLine($"CreateFileAsync[{Thread.CurrentThread.ManagedThreadId}]: File {nameFile} was created earlier => check access for this file.");
                            FileAttributes attributes = File.GetAttributes($"{path}\\{nameFile}");
                            if (debug) Console.WriteLine($"CreateFileAsync[{Thread.CurrentThread.ManagedThreadId}]: FileAttributes = {attributes}");
                            if (attributes == FileAttributes.ReadOnly)
                            {
                                if (debug) Console.WriteLine($"CreateFileAsync[{Thread.CurrentThread.ManagedThreadId}]: We cant write in this file {nameFile}.");
                                
                            }
                            else
                            {
                                if (debug) Console.WriteLine($"CreateFileAsync[{Thread.CurrentThread.ManagedThreadId}]: We cant write in this file {nameFile}.");

                            }

                        }
                    }
                    else
                    {
                        if (debug) Console.WriteLine($"CreateFileAsync[{Thread.CurrentThread.ManagedThreadId}]: CreateFileAsync was dispoused.");
                    }
                });

            }
            catch (Exception ex)
            {
                Console.WriteLine($"MSG = {ex.Message}; TRACE = {ex.StackTrace}");
            }

        }
    }
}
