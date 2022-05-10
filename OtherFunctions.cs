using System;
using System.Collections.Generic;
using System.Linq;

namespace TvShowSubtitleRenamer
{
    public class OtherFunctions
    {
        public static void RearrangeList(ref string[] arr, int[] order)
        {
            string[] final = new string[arr.Length];

            for (int i = 0; i < arr.Length; i++)
            {
                final[i] = arr[order[i]];
            }

            arr = final;
        }

        public static int[] AskOrder()
        {
            Console.WriteLine("What is the correct order (eg. \"1,3,2,4,5,6,7,8\")");
            string input = Console.ReadLine();
            
            string[] splitList = input.Split(',');
            int[] intList = new int[splitList.Length];

            for (int i = 0; i < splitList.Length; i++)
            {
                try
                {
                    intList[i] = Int16.Parse(splitList[i]);
                }
                catch
                {
                    Console.WriteLine("Incorrect format, please try again.");
                    AskOrder();
                }
            }
            
            // Verify List
            List<int> arrList = intList.ToList();
            for (int i = 0; i < arrList.Count - 2; i++)
            {
                if (arrList[i + 1] - arrList[i] != 1)
                {
                    Console.WriteLine("A number seems to be missing, please try again.");
                    AskOrder();
                }
            }

            return intList;
        }
    }
}