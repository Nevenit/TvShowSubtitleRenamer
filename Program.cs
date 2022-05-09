using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace TvShowSubtitleRenamer
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            while (true)
            {
                string pathToShow = "";

                bool doLoop = true;
                while (doLoop)
                {
                    Console.Write("Path to the shows directory (containing episodes and subtitles):");
                    pathToShow = Console.ReadLine();
                    if (Directory.Exists(pathToShow))
                        doLoop = false;
                    else
                        Console.WriteLine("This path doesn't exist, try again.");

                }
            
                // Find all mp4 and srt files
                string[] videoPaths = Directory.GetFiles(pathToShow,  "*.*").Where(s => Path.GetExtension(s) == ".mp4").ToArray();
                string[] subtitlePaths = Directory.GetFiles(pathToShow, "*.*").Where(s => Path.GetExtension(s) == ".srt").ToArray();
            
                // Sort arrays to get the right order
                Array.Sort(videoPaths);
                Array.Sort(subtitlePaths);
            
                // List all mp4 files
                for (int i = 0; i < videoPaths.Length; i++)
                    Console.WriteLine((i + 1) + ". " + Path.GetFileName(videoPaths[i]));

                Console.WriteLine("Are these the correct video files, in the right order? (y/n):");
                string answerV = Console.ReadLine();
                if (answerV.ToLower()[0] != 'y')
                    continue;

                // List all srt files
                for (int i = 0; i < subtitlePaths.Length; i++)
                    Console.WriteLine((i + 1) + ". " + Path.GetFileName(subtitlePaths[i]));

                Console.WriteLine("Are these the correct video files, in the right order? (y/n):");
                string answerS = Console.ReadLine();
                if (answerS.ToLower()[0] != 'y')
                    continue;
            
                for (int i = 0; i < subtitlePaths.Length; i++)
                    File.Move(subtitlePaths[i], pathToShow + "\\" + Path.GetFileNameWithoutExtension(videoPaths[i]) + ".en.srt");

                Console.WriteLine("Process complete, press any key to exit.");
                Console.ReadKey();
            }
        }
    }
}