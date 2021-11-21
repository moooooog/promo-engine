using System;
using FluentAssertions;
using Xunit;

namespace CompanyX.Promotions.Tests
{
    public class SkuTests
    {
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void Constructor_InvalidId_ThrowsException(string id)
        {
            Func<Sku> act = () => new Sku(id, 10);

            act.Should().Throw<ArgumentException>();
        }

        [Fact]
        public void Constructor_NegativeUnitPrice_ThrowsException()
        {
            Func<Sku> act = () => new Sku("A", -1);

            act.Should().Throw<ArgumentOutOfRangeException>();
        }
    }
}
