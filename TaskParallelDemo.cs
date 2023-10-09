using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace TaskParallelDemo
{
    public class TaskParallelDemo
    {
        public static void Method1()
        {
            Console.WriteLine("Welcome To TPL Programming");

            string[] words = CreateWordArray(@"http://www.gutenberg.org/files/54700/54700-0.txt");

            Parallel.Invoke(() =>
            {
                Console.WriteLine("Begin First Task");
                GetLongestWords(words);
            },
            () =>
            {
                Console.WriteLine("Begin Second Task");
                GetMostCommonWords(words);
            },
            () =>
            {
                Console.WriteLine("Begin Third Task");
                GetCountForWords(words, "sleep");
            });
        }

        private static void GetCountForWords(string[] words, string term)
        {
            var findWord = from word in words
                           where word.ToUpper().Contains(term.ToUpper())
                           select word;
            Console.WriteLine($@"Task 3 -- The Word ""{term}"" occurs {findWord.Count()} times.");
        }

        private static void GetMostCommonWords(string[] words)
        {
            var frequencyOrder = from word in words
                                 where word.Length > 6
                                 group word by word into g
                                 orderby g.Count() descending
                                 select g.Key;

            var commonWords = frequencyOrder.Take(10);
            StringBuilder sb = new StringBuilder();
            sb.Append("Task 2 -- Most common words are : ");
            foreach (var v in commonWords)
            {
                sb.AppendLine(" " + v);
            }
            Console.WriteLine(sb.ToString());
        }

        private static string GetLongestWords(string[] words)
        {
            var longestWord = (from w in words
                               orderby w.Length descending
                               select w).First();
            Console.WriteLine($"Task 1 -- Longest word is {longestWord}");
            return longestWord;
        }

        private static string[] CreateWordArray(string url)
        {
            Console.WriteLine($"Retriving from {url}");
            string s = new WebClient().DownloadString(url);
            return s.Split(new char[] { ' ', ',', '.', ';', ':', '-', '_', '\u000A' }, StringSplitOptions.RemoveEmptyEntries);
        }
    }
}
