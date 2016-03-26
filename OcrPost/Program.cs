using System;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using CommandLine;

namespace OcrPost
{
    class Program
    {
        static int Main(string[] args)
        {
            var result = CommandLine.Parser.Default.ParseArguments<Options>(args);
            var exitCode = result
                .MapResult(
                    options =>
                    {
                        // regexes
                        var replaces = Replaces.GetReplaceConfigs(options.Config);
                        
                        // no regexes found
                        if (replaces.Count == 0)
                        {
                            Console.WriteLine($"Found no replaces in {options.Config}");
                        }

                        // Desination found?
                        if (!Directory.Exists(options.DestinationFolder)) Console.WriteLine($"Destination folder {options.DestinationFolder} does not exist");

                        // Loop files of source folder
                        var directory = new DirectoryInfo(options.SourceFolder);
                        var textFiles = directory.GetFiles("*.txt");
                        string text = String.Empty; 

                        // Found no source files
                        if (!textFiles.Any()) Console.WriteLine("Found no .txt-files");

                        // Loop files
                        foreach (var file in textFiles)
                        {
                            text = File.ReadAllText(file.FullName);

                            foreach (var c in replaces)
                            {
                                text = Regex.Replace(text, c.Find, c.Replace);
                            }

                            // save to destination - overwrite if exists
                            var dest = Path.Combine(options.DestinationFolder, file.Name); 
                            File.WriteAllText(dest, text);

                            Console.Write($"Processed: {file.Name}, saved in: {dest}");
                        }

                        Console.Read();
                        return 0;
                    },
                    errors =>
                    {
                        return 1;
                    }
                );
            return exitCode;
        }
    }

}
