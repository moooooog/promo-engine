using System;
using FluentAssertions;
using Xunit;

namespace CompanyX.Promotions.Tests
{
    public class SkuTests
    {
        [Theory]
        [InlineData(null, "the id is null")]
        [InlineData("", "the id is empty")]
        [InlineData(" ", "the id is whitespace only")]
        public void Constructor_InvalidId_ThrowsException(string id, string because)
        {
            Func<Sku> act = () => new Sku(id, 10);

            act.Should().Throw<ArgumentException>(because);
        }

        [Fact]
        public void Constructor_ValidId_SetsId()
        {
            var actualSku = new Sku("A", 1);

            actualSku.UnitPrice.Should().Be(1);
        }

        [Fact]
        public void Constructor_NegativeUnitPrice_ThrowsException()
        {
            Func<Sku> act = () => new Sku("A", -0.01m);

            act.Should().Throw<ArgumentOutOfRangeException>();
        }

        [Theory]
        [InlineData(0, "the unit price is zero")]
        [InlineData(0.01, "the unit price is positive")]
        public void Constructor_NonNegativeUnitPrice_SetsUnitPrice(decimal unitPrice, string because)
        {
            var actualSku = new Sku("A", unitPrice);

            actualSku.UnitPrice.Should().Be(unitPrice, because);
        }
    }
}