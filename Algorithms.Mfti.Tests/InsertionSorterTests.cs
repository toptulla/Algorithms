using Xunit;

namespace Algorithms.Mfti.Tests
{
    public class InsertionSorterTests
    {
        [Fact]
        public void Insertion_sorting_of_integers()
        {
            var numbers = new[] { 4, 6, 1, 7, 5, 6, 4 };
            var sorter = new InsertionSorter();

            sorter.Sort(numbers);
            
            Assert.Equal(new[] { 1, 4, 4, 5, 6, 6, 7 }, numbers);
        }

        [Fact]
        public void Insertion_sorting_of_integers_with_positions()
        {
            var numbers = new[] { 1, 8, 4, 2, 3, 7, 5, 6, 9, 0 };
            var sorter = new InsertionSorter();

            var positions = sorter.SortWithPositions(numbers);

            Assert.Equal(new[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 }, numbers);
            Assert.Equal(new[] { 1, 2, 2, 2, 3, 5, 5, 6, 9, 1 }, positions);
        }

        [Fact]
        public void Insertion_sorting_with_stats()
        {
            var numbers = new[] { 10.00, 8.70, 0.01, 5.00, 3.00 };
            var sorter = new InsertionSorter();

            var stats = sorter.SortWithStats(numbers);

            Assert.Equal(3, stats.min);
            Assert.Equal(4, stats.avg);
            Assert.Equal(1, stats.max);
        }

        [Fact]
        public void Insertion_sorting_with_logs()
        {
            var numbers = new[] { 3, 1, 4, 2, 2 };
            var sorter = new InsertionSorter();

            var logs = sorter.SortWithLogs(numbers);

            Assert.Equal("Swap elements at indices 1 and 2.", logs[0]);
            Assert.Equal("Swap elements at indices 2 and 4.", logs[1]);
            Assert.Equal("Swap elements at indices 3 and 5.", logs[2]);
            Assert.Equal("No more swaps needed.", logs[3]);
        }
    }
}
