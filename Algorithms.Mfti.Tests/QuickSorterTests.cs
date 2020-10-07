using Xunit;

namespace Algorithms.Mfti.Tests
{
    public class QuickSorterTests
    {
        [Fact]
        public void Quick_sorting_of_integers()
        {
            var numbers = new[] { 7, 1, 2, 3, 1, 5, 8, 9, 6, 3, 0 };
            var sorter = new QuickSorter();

            sorter.Sort(numbers);

            Assert.Equal(new[] { 0, 1, 1, 2, 3, 3, 5, 6, 7, 8, 9 }, numbers);
        }

        [Fact]
        public void Get_k_stats_some_items_of_array()
        {
            var numbers = new[] { 1, 2, 800005, -516268571, 1331571109 };
            var sorter = new QuickSorter();

            sorter.GetKStats(numbers, 2, 3);

            Assert.Equal(2, numbers[2]);
            Assert.Equal(800005, numbers[3]);
        }

        [Fact]
        public void Get_k_stats_all_items_of_array()
        {
            var numbers = new[] { 7, 1, 2, 3, 1, 5, -8, 9, 6, 3, 0 };

            var sorter = new QuickSorter();

            sorter.GetKStats(numbers, 0, 10);

            Assert.Equal(new[] { -8, 0, 1, 1, 2, 3, 3, 5, 6, 7, 9 }, numbers);
        }
    }
}
