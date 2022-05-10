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
        private static string _language;
        
        public static void Main(string[] args)
        {
            GetPath();
        }
        
        static void GetPath()
        {
            while (true)
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
                    break;
                }
                Console.WriteLine("This path doesn't exist, try again.");
            }
        }
        
        static void CheckVideoOrder()
        {
            while (true)
            {
                // List all mp4 files
                for (int i = 0; i < _videoPaths.Length; i++)
                    Console.WriteLine((i + 1) + ". " + Path.GetFileName(_videoPaths[i]));

                Console.WriteLine("Are these the correct video files, in the right order? (y/n):");
                string answerV = Console.ReadLine();
                if (answerV.ToLower()[0] == 'y')
                {
                    CheckSubtitleOrder();
                    break;
                }

                int[] correctOrder = OtherFunctions.AskOrder();
                OtherFunctions.RearrangeList(ref _videoPaths, correctOrder);
            }
        }

        static void CheckSubtitleOrder()
        {
            while (true)
            {
                // List all srt files
                for (int i = 0; i < _subtitlePaths.Length; i++)
                    Console.WriteLine((i + 1) + ". " + Path.GetFileName(_subtitlePaths[i]));

                Console.WriteLine("Are these the correct video files, in the right order? (y/n):");
                string answerS = Console.ReadLine();
                if (answerS.ToLower()[0] == 'y')
                {
                    ChooseLanguage();
                    break;
                }
                int[] correctOrder = OtherFunctions.AskOrder();
                OtherFunctions.RearrangeList(ref _subtitlePaths, correctOrder);
            }
        }

        static void ChooseLanguage()
        {
            Console.Write("Subtitle language (eg. en): ");
            _language = Console.ReadLine();
            FinalFunction();
        }

        static void FinalFunction()
        {
            for (int i = 0; i < _subtitlePaths.Length; i++)
                File.Move(_subtitlePaths[i], i + ".temp");
            
            for (int i = 0; i < _subtitlePaths.Length; i++)
                File.Move(i + ".temp", _showPath + "\\" + Path.GetFileNameWithoutExtension(_videoPaths[i]) + "." + _language + ".srt");

            Console.WriteLine("Process complete, press any key to exit.");
            Console.ReadKey();
        }
    }
}