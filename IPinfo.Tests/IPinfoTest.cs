using Xunit;

using IPinfo.Utilities;

namespace IPinfo.Tests
{
    public class IPinfoTest
    {
        [Theory]
        [InlineData("PK", "Pakistan")]
        [InlineData("US", "United States")]
        public void TestGetCountry(string countryCode, string expected)
        {
            // Arrange
            CountryHelper.Init();
            
            // Act
            string actual = CountryHelper.GetCountry(countryCode);

            // Assert
            Assert.Equal(expected, actual);
        }
    }    
}