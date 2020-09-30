using System.Collections.Generic;
using System.IO;

namespace Algorithms.Mfti
{
    public class InsertionSorter
    {
        public void Sort(int[] input)
        {
            for (int i = 0; i < input.Length - 1; i++)
            {
                int j = i + 1;
                while (j > 0 && input[j] < input[j - 1])
                {
                    int temp = input[j];
                    input[j] = input[j - 1];
                    input[j - 1] = temp;
                    j--;
                }
            }
        }

        public int[] SortWithPositions(int[] input)
        {
            var positions = new int[input.Length];
            positions[0] = 1;

            for (int i = 0; i < input.Length - 1; i++)
            {
                int j = i + 1;
                int tmpJ = j;
                while (j > 0 && input[j] < input[j - 1])
                {
                    int temp = input[j];
                    input[j] = input[j - 1];
                    input[j - 1] = temp;
                    j--;
                }
                positions[tmpJ] = j + 1;
            }

            return positions;
        }

        public (int min, int avg, int max) SortWithStats(double[] input)
        {
            var inputCopy = new double[input.Length];
            for (int i = 0; i < input.Length; i++)
                inputCopy[i] = input[i];

            for (int i = 1; i < input.Length; i++)
            {
                int j = i - 1;
                while (j >= 0 && input[j] > input[j + 1])
                {
                    double tmp = input[j];
                    input[j] = input[j + 1];
                    input[j + 1] = tmp;
                    j--;
                }
            }

            var minValue = input[0];
            var avgValue = input[input.Length / 2];
            var maxValue = input[input.Length - 1];

            int min = 0;
            int avg = 0;
            int max = 0;

            for (int i = 0; i < inputCopy.Length; i++)
            {
                if (inputCopy[i] == minValue)
                    min = i + 1;

                if (inputCopy[i] == avgValue)
                    avg = i + 1;

                if (inputCopy[i] == maxValue)
                    max = i + 1;
            }

            return (min, avg, max);
        }

        public List<string> SortWithLogs(int[] input)
        {
            var result = new List<string>();

            for (int i = 1; i < input.Length; i++)
            {
                bool swap = false;
                int j = i - 1;
                int from = 0;

                while (j >= 0 && input[j] > input[j + 1])
                {
                    if (!swap)
                    {
                        swap = true;
                        from = j + 1;
                    }
                    int tmp = input[j];
                    input[j] = input[j + 1];
                    input[j + 1] = tmp;
                    j--;
                }

                if (swap)
                    result.Add($"Swap elements at indices {j + 2} and {from + 1}.");
            }
            result.Add("No more swaps needed.");

            string inputStr = string.Empty;
            foreach (var i in input)
                inputStr += $"{i} ";
            inputStr = inputStr.Trim(' ');

            result.Add(inputStr);

            return result;
        }

        public void SortWithLogsWriteLogs(int[] input, StreamWriter writer)
        {
            for (int i = 0; i < input.Length; i++)
            {
                var minVal = input[i];
                var minValIndex = i;

                for (int j = i + 1; j < input.Length; j++)
                {
                    if (input[j] < minVal)
                    {
                        minVal = input[j];
                        minValIndex = j;
                    }
                }

                if (minValIndex != i)
                {
                    writer.WriteLine($"Swap elements at indices {i + 1} and {minValIndex + 1}.");
                    input[minValIndex] = input[i];
                    input[i] = minVal;
                }
            }

            writer.WriteLine("No more swaps needed.");

            writer.Write(input[0].ToString());
            for (int i = 1; i < input.Length; i++)
                writer.Write($" {input[i]}");
        }
    }
}
