using Xunit;

namespace Algorithms.Mfti.Tests
{
    public class MergeSorterTests
    {
        [Fact]
        public void Merge_sorting_of_integers()
        {
            var numbers = new[] { 4, 6, 1, 7, 5, 6, 4 };
            var sorter = new MergeSorter();

            sorter.Srot(numbers);

            Assert.Equal(new[] { 1, 4, 4, 5, 6, 6, 7 }, numbers);
        }

        [Fact]
        public void Merge_get_inversion_count()
        {
            var numbers = new[] { 1, 8, 2, 1, 4, 7, 3, 2, 3, 6 };
            var sorter = new MergeSorter();

            int count = sorter.GetInversionCount(numbers);

            Assert.Equal(17, count);
        }
    }
}
