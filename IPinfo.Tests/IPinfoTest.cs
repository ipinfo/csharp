using Xunit;

// needed to call GetCountry method in test
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
            // Act
            string actual = CountryHelper.GetCountry(countryCode); // access to internal methods is provided in sdk's csproj

            // Assert
            Assert.Equal(expected, actual);
        }
    }    
}
