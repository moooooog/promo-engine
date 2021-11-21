using System;
using FluentAssertions;
using Xunit;

namespace CompanyX.Promotions.Tests
{
    public class SkuQuantityTests
    {
        [Theory]
        [InlineData(null, "the id is null")]
        [InlineData("", "the id is empty")]
        [InlineData(" ", "the id is whitespace only")]
        public void Constructor_InvalidId_ThrowsException(string id, string because)
        {
            Func<SkuQuantity> act = () => new SkuQuantity(id, 10);

            act.Should().Throw<ArgumentException>(because);
        }

        [Fact]
        public void Constructor_ValidId_SetsId()
        {
            var actualSkuQuantity = new SkuQuantity("A", 1);

            actualSkuQuantity.UnitCount.Should().Be(1);
        }

        [Fact]
        public void Constructor_NegativeQuantity_ThrowsException()
        {
            Func<SkuQuantity> act = () => new SkuQuantity("A", -1);

            act.Should().Throw<ArgumentOutOfRangeException>();
        }

        [Theory]
        [InlineData(0, "the quantity is zero")]
        [InlineData(1, "the quantity is positive")]
        public void Constructor_NonNegativeQuantity_SetsQuantity(int quantity, string because)
        {
            var actualSkuQuantity = new SkuQuantity("A", quantity);

            actualSkuQuantity.UnitCount.Should().Be(quantity, because);
        }
    }
}