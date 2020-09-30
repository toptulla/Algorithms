using Xunit;

namespace Algorithms.Mfti.Tests
{
    public class NumberTests
    {
        [Theory]
        [InlineData("23", "11", "34")]
        [InlineData("-100", "1", "-99")]
        public void Sum_two_numbers(string a, string b, string result)
        {
            var aNumb = new Number(a);
            var bNumb = new Number(b);
            var resultNumb = aNumb + bNumb;
            Assert.Equal(result, resultNumb.ToString());
        }

        [Theory]
        [InlineData("23", "11", "144")]
        [InlineData("-100", "1", "-99")]
        public void Sum_two_numbers_with_exponentiation(string a, string b, string result)
        {
            var aNumb = new Number(a);
            var bNumb = new Number(b);
            var resultNumb = aNumb + bNumb * bNumb;
            Assert.Equal(result, resultNumb.ToString());
        }
    }
}
