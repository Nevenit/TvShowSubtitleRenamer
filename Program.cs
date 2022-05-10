using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace TvShowSubtitleRenamer
{
    internal class Program
    {
        private static string _showPath;
        private static string[] _videoPaths;
        private static string[] _subtitlePaths;
        private static string language;
        
        public static void Main(string[] args)
        {
            /*
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
                
                Console.Write("Subtitle language (eg. en): ");
                string language = Console.ReadLine();
                
                for (int i = 0; i < subtitlePaths.Length; i++)
                    File.Move(subtitlePaths[i], pathToShow + "\\" + Path.GetFileNameWithoutExtension(videoPaths[i]) + "." + language + ".srt");

                Console.WriteLine("Process complete, press any key to exit.");
                Console.ReadKey();
            }
            */
        }
        
        void GetPath()
        {
            Console.Write("Path to the shows directory (containing episodes and subtitles):");
            _showPath = Console.ReadLine();
            if (Directory.Exists(_showPath))
            {
                // Find all mp4 and srt files
                _videoPaths = Directory.GetFiles(_showPath, "*.*").Where(s => Path.GetExtension(s) == ".mp4").ToArray();
                _subtitlePaths = Directory.GetFiles(_showPath, "*.*").Where(s => Path.GetExtension(s) == ".srt").ToArray();
                
                // Sort arrays to get the right order
                Array.Sort(_videoPaths);
                Array.Sort(_subtitlePaths);

                CheckVideoOrder();
            }
            else
                Console.WriteLine("This path doesn't exist, try again.");
        }
        
        void CheckVideoOrder()
        {
            // List all mp4 files
            for (int i = 0; i < _videoPaths.Length; i++)
                Console.WriteLine((i + 1) + ". " + Path.GetFileName(_videoPaths[i]));

            Console.WriteLine("Are these the correct video files, in the right order? (y/n):");
            string answerV = Console.ReadLine();
            if (answerV.ToLower()[0] != 'y')
                CheckSubtitleOrder();
            else
            {
                int[] correctOrder = OtherFunctions.AskOrder();
                OtherFunctions.RearrangeList(ref _videoPaths, correctOrder);
                CheckVideoOrder();
            }
                
        }

        void CheckSubtitleOrder()
        {
            // List all srt files
            for (int i = 0; i < _subtitlePaths.Length; i++)
                Console.WriteLine((i + 1) + ". " + Path.GetFileName(_subtitlePaths[i]));

            Console.WriteLine("Are these the correct video files, in the right order? (y/n):");
            string answerS = Console.ReadLine();
            if (answerS.ToLower()[0] != 'y')
                ChooseLanguage();
            else
            {
                int[] correctOrder = OtherFunctions.AskOrder();
                OtherFunctions.RearrangeList(ref _subtitlePaths, correctOrder);
                CheckSubtitleOrder();
            }
        }

        void ChooseLanguage()
        {
            Console.Write("Subtitle language (eg. en): ");
            language = Console.ReadLine();
        }

        void FinalFunction()
        {
            for (int i = 0; i < _subtitlePaths.Length; i++)
                File.Move(_subtitlePaths[i], _showPath + "\\" + Path.GetFileNameWithoutExtension(_videoPaths[i]) + "." + language + ".srt");

            Console.WriteLine("Process complete, press any key to exit.");
            Console.ReadKey();
        }
    }
}