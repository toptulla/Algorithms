using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Schema;

/*
 * Для того, чтобы проверить решение на сайте нужно заменить "NumberData" на null и
 * скопировать содержимое в новый файл, затем скопировать класс Number, указать пространства
 * имен, которые в нем используются, этот файл использовать на сайте.
*/

namespace Algorithms.Mfti.Console
{
    class Program
    {
        static void Main()
        {
            //SumNumbers();
            //SqrtNumbers();
            //InsertionSortingWithStats();
            //MergeSort();
        }

        private static void MergeSort()
        {
            var input = ReadInputData(null);
            var numbers = input.Skip(1).First().Split().Select(n => int.Parse(n)).ToArray();

            using (var stream = File.OpenWrite("output.txt"))
            {
                using (var writer  = new StreamWriter(stream))
                {
                    var sorter = new MergeSorter();
                    sorter.SrotWithWrite(numbers, writer);
                }
            }
        }

        private static void InsertionSortingWithStats()
        {
            var input = ReadInputData(null);
            var numbers = input.Skip(1).First().Split().Select(n => double.Parse(n)).ToArray();
            var sorter = new InsertionSorter();
            var stats = sorter.SortWithStats(numbers);
            WriteOutputData(new[] { $"{stats.min} {stats.avg} {stats.max}" }, null);
        }

        private static void InsertionSortingWithLogs()
        {
            var input = ReadInputData(null);
            var numbers = input.Skip(1).First().Split().Select(n => int.Parse(n)).ToArray();
            var sorter = new InsertionSorter();
            var logs = sorter.SortWithLogs(numbers);
            WriteOutputData(logs, null);
        }

        private static void SqrtNumbers()
        {
            string folder = "NumberData";
            var input = ReadInputData(folder);
            var numbers = input.First().Split();
            var a = new Number(numbers[0]);
            var b = new Number(numbers[1]);
            var c = a + b * b;
            WriteOutputData(new[] { c.ToString() }, folder);
        }

        private static void SumNumbers()
        {
            string folder = "NumberData";
            var input = ReadInputData(folder);
            var numbers = input.First().Split();
            var a = new Number(numbers[0]);
            var b = new Number(numbers[1]);
            var c = a + b;
            WriteOutputData(new[] { c.ToString() }, folder);
        }

        private static IEnumerable<string> ReadInputData(string folder)
        {
            string file = GetFullFileName("input.txt", folder);

            using (Stream stream = File.OpenRead(file))
                using (StreamReader reader = new StreamReader(stream))
                    while (!reader.EndOfStream)
                        yield return reader.ReadLine();
        }

        private static void WriteOutputData(IEnumerable<string> output, string folder)
        {
            string file = GetFullFileName("output.txt", folder);

            using (Stream stream = File.OpenWrite(file))
                using (StreamWriter writer = new StreamWriter(stream))
                    foreach (var item in output)
                        writer.WriteLine(item);
        }

        private static string GetFullFileName(string fileName, string folderName)
        {
            if (string.IsNullOrEmpty(folderName))
                return fileName;
            return Path.Combine(folderName, fileName);
        }
    }
}
