using CleanArchitecture.Domain;
using CleanArchitecture.Domain.Models;
using Xunit;

namespace UnitTests.Domain.Models
{
    public class PortfolioTests
    {
        [Fact]
        public void Portfolio_WithValidData_ShouldCreate()
        {
            // Arrange
            var name = "My Portfolio";
            var portfolio = new Portfolio(name);

            // Act
            var result = portfolio;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(name, result.Name.Value);
        }

        [Fact]
        public void Portfolio_WithEmptyName_ShouldThrowException()
        {
            // Arrange
            var name = string.Empty;

            // Act
            Portfolio act() => new(name);

            // Assert
            Assert.Throws<BusinessException>(act);
        }

        [Fact]
        public void Update_WithValidData_ShouldUpdate()
        {
            // Arrange
            var name = "My Portfolio";
            var portfolio = new Portfolio(name);
            var updatedName = "My Portfolio Updated";
            var updatedEnabled = false;

            // Act
            var result = portfolio.Update(updatedName, updatedEnabled);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(updatedName, result.Name.Value);
            Assert.Equal(updatedEnabled, result.Enabled);
        }
    }
}
