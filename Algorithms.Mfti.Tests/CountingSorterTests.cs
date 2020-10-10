using Xunit;

namespace Algorithms.Mfti.Tests
{
    public class CountingSorterTests
    {
        [Fact]
        public void Counting_sort_of_integers()
        {
            var numbers = new[] { 4, 6, 1, 7, 5, 6, 4 };
            var sorter = new CountingSorter();

            sorter.Sort(numbers, 1, 7);

            Assert.Equal(new[] { 1, 4, 4, 5, 6, 6, 7 }, numbers);
        }

        [Fact]
        public void Counting_integers()
        {
            var a = new[] { 4, 6, 1, 7, 5, 6, 4 };
            var b = new[] { 5, 3, 7 };
            var c = new int[a.Length * b.Length];

            int min = int.MaxValue;
            int max = 0;
            int index = 0;
            for (int i = 0; i < a.Length; i++)
            {
                for (int j = 0; j < b.Length; j++)
                {
                    int value = a[i] * b[j];
                    c[index] = value;
                    index++;

                    if (value > max)
                        max = value;
                }
            }

            var sorter = new CountingSorter();

            sorter.Sort(c, min, max);

            int sum = c[0];
            for (int i = 10; i < c.Length; i += 10)
                sum += c[i];

            Assert.Equal(73, sum);
        }

        [Fact]
        public void Counting_integers_another()
        {
            var a = new[] { 7, 1, 4, 9 };
            var b = new[] { 2, 7, 8, 11 };
            var c = new int[a.Length * b.Length];

            int min = int.MaxValue;
            int max = 0;
            int index = 0;
            for (int i = 0; i < a.Length; i++)
            {
                for (int j = 0; j < b.Length; j++)
                {
                    int value = a[i] * b[j];
                    c[index] = value;
                    index++;

                    if (value > max)
                        max = value;

                    if (value < min)
                        min = value;
                }
            }

            var sorter = new CountingSorter();

            sorter.Sort(c, min, max);

            int sum = c[0];
            for (int i = 10; i < c.Length; i += 10)
                sum += c[i];

            Assert.Equal(51, sum);
        }

        [Fact]
        public void Counting_integers_another1()
        {
            var a = new[] { 7, 1, 4, 40000 };
            var b = new[] { 40000, 7, 8, 11 };
            var c = new int[a.Length * b.Length];

            int min = int.MaxValue;
            int max = 0;
            int index = 0;
            for (int i = 0; i < a.Length; i++)
            {
                for (int j = 0; j < b.Length; j++)
                {
                    int value = a[i] * b[j];
                    c[index] = value;
                    index++;

                    if (value > max)
                        max = value;

                    if (value < min)
                        min = value;
                }
            }

            var sorter = new CountingSorter();
            sorter.Sort(c, min, max);

            int sum = c[0];
            for (int i = 10; i < c.Length; i += 10)
                sum += c[i];

            Assert.Equal(51, sum);
        }
    }
}
