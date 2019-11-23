using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rotator
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("0: Rotate 0");
            Console.WriteLine("1: Rotate 90");
            Console.WriteLine("2: Rotate 180");
            Console.WriteLine("3: Rotate 270");
            Console.WriteLine("Press key");
            var key = Console.ReadKey();
            var rotate = "";
            if (key.Key == ConsoleKey.D0)
                rotate = "0";
            else if (key.Key == ConsoleKey.D1)
                rotate = "90";
            else if (key.Key == ConsoleKey.D2)
                rotate = "180";
            else if (key.Key == ConsoleKey.D3)
                rotate = "270";
            if (rotate == "" || args.Length == 0)
                return; // return if no file was dragged onto exe)
            foreach (var arg in args)
            {
                //Log(arg);
                try
                {
                    Console.WriteLine(arg);
                    var filenamenoext = Path.GetFileNameWithoutExtension(arg);
                    var filename = Path.GetFileName(arg);
                    var fullnewpathfile = arg.Replace(filename, filenamenoext);
                    //var filepath = @"H:\AfterSSDDownloads\asd";
                    var proc = new Process();
                    proc.StartInfo.FileName = "C:\\Program Files (x86)\\ffmpeg.exe";
                    proc.StartInfo.Arguments = $"-i \"{arg}\" -metadata:s:v rotate={rotate} -codec copy \"{fullnewpathfile}-fv.mp4\"";
                    proc.Start();
                    proc.WaitForExit();
                    var exitCode = proc.ExitCode;
                    proc.Close();

                }
                catch (Exception exc)
                {
                    Log(exc.ToString());
                    Console.ReadKey();
                }
            }
            return;
            //string text = File.ReadAllText(args[0]);


        }
        public static void Log(string message)
        {
            StreamWriter sw = null;
            try
            {
                //sw = new StreamWriter("logfile.txt", true);
                sw = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + "\\logfile.txt", true);
                //sw = new StreamWriter("f:\\logfile.txt", true);
                sw.WriteLine(DateTime.Now.ToString() + ": " + message);
                sw.Flush();
                sw.Close();
            }
            catch (Exception exc)
            {

            }
        }
    }
}
