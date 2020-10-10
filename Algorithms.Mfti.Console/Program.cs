using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
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

            //-------------

            //var numbers = new[] { 1, 8, 2, 1, 4, 7, 3, 2, 3, 6 };
            //var sorter = new MergeSorter();

            //int count = sorter.GetInversionCount(numbers);

            //System.Console.WriteLine(count);

            //-------------

            //int n = 7;
            //var arr = new int[n];

            //for (int i = 0; i < n; i++)
            //    arr[i] = i + 1;

            //for (int i = 2; i < n; i++)
            //{
            //    int buf = arr[i];
            //    arr[i] = arr[i/2];
            //    arr[i/2] = buf;
            //}

            //for (int i = 0; i < n; i++)
            //    System.Console.Write($"{arr[i]} ");

            //-------------

            //int n;
            //using (var file = File.OpenRead("input.txt"))
            //using (StreamReader reader = new StreamReader(file))
            //    n = int.Parse(reader.ReadLine());

            //var arr = new int[n];
            //for (int i = 0; i < n; i++)
            //    arr[i] = i + 1;

            //for (int i = 2; i < n; i++)
            //{
            //    int buf = arr[i];
            //    arr[i] = arr[i / 2];
            //    arr[i / 2] = buf;
            //}

            //var sb = new StringBuilder();
            //for (int i = 0; i < n - 1; i++)
            //    sb.Append($"{arr[i]} ");
            //sb.Append($"{arr[n - 1]}");

            //using (Stream stream = File.OpenWrite("output.txt"))
            //using (StreamWriter writer = new StreamWriter(stream))
            //    writer.WriteLine(sb.ToString());

            //-------------

            //int[] firstLine = null;
            //int[] secondLine = null;
            //using (var file = File.OpenRead("input.txt"))
            //using (StreamReader reader = new StreamReader(file))
            //{
            //    firstLine = reader.ReadLine().Split().Select(n => int.Parse(n)).ToArray();
            //    secondLine = reader.ReadLine().Split().Select(n => int.Parse(n)).ToArray();
            //}

            //-------------

            //int[] firstLine = new[] { 5, 3, 4 };
            //int[] secondLine = new[] { 200000, 300000, 5, 1, 2 };

            //int n = firstLine[0];
            //int k1 = firstLine[1];
            //int k2 = firstLine[2];

            //int a = secondLine[0];
            //int b = secondLine[1];
            //int c = secondLine[2];
            //int a1 = secondLine[3];
            //int a2 = secondLine[4];

            //var numbers = new int[n];
            //numbers[0] = a1;
            //numbers[1] = a2;
            //for (int i = 2; i < n; i++)
            //    numbers[i] = unchecked(a * numbers[i - 2] + b * numbers[i - 1] + c);

            //PartitionK(numbers, 0, numbers.Length - 1, k1 - 1, k2 - 1);

            //System.Console.WriteLine(numbers[k1 - 1]);
            //System.Console.WriteLine(numbers[k2 - 1]);

            //var sb = new StringBuilder();
            //for (int i = k1 - 1; i < k2; i++)
            //    sb.Append($"{numbers[i]} ");

            //-------------

            //int n = 5;
            //int range = 1;
            //int[] arr = new[] { 1, 5, 3, 4, 1 };

            //var needConttinue = false;
            //do
            //{
            //    needConttinue = false;
            //    for (int i = 0; i + range < n; i++)
            //    {
            //        if (arr[i] > arr[i + range])
            //        {
            //            int tmp = arr[i + range];
            //            arr[i + range] = arr[i];
            //            arr[i] = tmp;
            //            needConttinue = true;
            //        }
            //    }
            //} while (needConttinue);

            //string result = "YES";
            //for (int i = 0; i + 1 < n; i++)
            //{
            //    if (arr[i] > arr[i + 1])
            //    {
            //        result = "NO";
            //        break;
            //    }
            //}

            //System.Console.WriteLine(result);

            //-------------

            //for (int i = 1; i < 10; i++)
            //    System.Console.WriteLine($"{1 << i} pow {i}");

            System.Console.WriteLine(40000 * 40000);
        }

        private static void PartitionK(int[] input, int left, int right, int k1, int k2)
        {
            int key = input[(left + right) / 2];
            int i = left;
            int j = right;
            do
            {
                // Находим первый элемент, который больше или равен key, идем слева
                while (input[i] < key)
                    i++;

                // Находим первый элемент, который меньше или равен key, идем справа
                while (key < input[j])
                    j--;

                if (i == j)
                {
                    i++;
                    j--;
                }

                if (i < j)
                {
                    int buf = input[i];
                    input[i] = input[j];
                    input[j] = buf;
                    i++;
                    j--;
                }
            } while (i < j); // Обмен происходит до тех пор пока индексы не пересекутся

            if (left < j && (left <= k1 || k2 <= j))
                PartitionK(input, left, j, k1, k2);

            if (i < right && (i <= k1 || k2 <= right))
                PartitionK(input, i, right, k1, k2);
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
                    sorter.SortWithWrite(numbers, writer);
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
