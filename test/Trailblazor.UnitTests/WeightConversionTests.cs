using Trailblazor.Shared.Models;
using Xunit;

namespace Trailblazor.UnitTests
{
    public class WeightConversionTests
    {
        [Theory]
        [InlineData(10, 0.01)]
        [InlineData(55, 0.06)] 
        [InlineData(1234, 1.23)]
        [InlineData(0, 0)]
        public void GramsToKilograms_ReturnsKilograms(decimal input, decimal expected)
        {
            var actual = Weight.GramsToKilograms(input);
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(10, 0.35)]
        [InlineData(55, 1.94)]
        [InlineData(1234, 43.53)]
        [InlineData(0, 0)]
        public void GramsToOunces_ReturnsOunces(decimal input, decimal expected)
        {
            var actual = Weight.GramsToOunces(input);
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(10, 0.02)]
        [InlineData(55, 0.12)]
        [InlineData(1234, 2.72)]
        [InlineData(0, 0)]
        public void GramsToPounds_ReturnsPounds(decimal input, decimal expected)
        {
            var actual = Weight.GramsToPounds(input);
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(10, 10000)]
        [InlineData(55, 55000)]
        [InlineData(1234, 1234000)]
        [InlineData(0, 0)]
        public void KilogramsToGrams_ReturnsGrams(decimal input, decimal expected)
        {
            var actual = Weight.KilogramsToGrams(input);
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(10, 352.74)]
        [InlineData(55, 1940.07)]
        [InlineData(1234, 43528.07)]
        [InlineData(0, 0)]
        public void KilogramsToOunces_ReturnsOunces(decimal input, decimal expected)
        {
            var actual = Weight.KilogramsToOunces(input);
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(10, 22.05)]
        [InlineData(55, 121.25)]
        [InlineData(1234, 2720.50)]
        [InlineData(0, 0)]
        public void KilogramsToPounds_ReturnsPounds(decimal input, decimal expected)
        {
            var actual = Weight.KilogramsToPounds(input);
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(10, 283.50)]
        [InlineData(55, 1559.22)]
        [InlineData(1234, 34983.31)]
        [InlineData(0, 0)]
        public void OuncesToGrams_ReturnsGrams(decimal input, decimal expected)
        {
            var actual = Weight.OuncesToGrams(input);
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(10, 0.28)]
        [InlineData(55, 1.56)]
        [InlineData(1234, 34.98)]
        [InlineData(0, 0)]
        public void OuncesToKilograms_ReturnsKilograms(decimal input, decimal expected)
        {
            var actual = Weight.OuncesToKilograms(input);
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(10, 0.63)]
        [InlineData(55, 3.44)]
        [InlineData(1234, 77.13)]
        [InlineData(0, 0)]
        public void OuncesToPounds_ReturnsPounds(decimal input, decimal expected)
        {
            var actual = Weight.OuncesToPounds(input);
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(10, 4535.92)]
        [InlineData(55, 24947.58)]
        [InlineData(1234, 559732.98)]
        [InlineData(0, 0)]
        public void PoundsToGrams_ReturnsGrams(decimal input, decimal expected)
        {
            var actual = Weight.PoundsToGrams(input);
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(10, 4.54)]
        [InlineData(55, 24.95)]
        [InlineData(1234, 559.73)]
        [InlineData(0, 0)]
        public void PoundsToKilograms_ReturnsKilograms(decimal input, decimal expected)
        {
            var actual = Weight.PoundsToKilograms(input);
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(10, 160)]
        [InlineData(55, 880)]
        [InlineData(1234, 19744)]
        [InlineData(0, 0)]
        public void PoundsToOunces_ReturnsOunces(decimal input, decimal expected)
        {
            var actual = Weight.PoundsToOunces(input);
            Assert.Equal(expected, actual);
        }
    }
}