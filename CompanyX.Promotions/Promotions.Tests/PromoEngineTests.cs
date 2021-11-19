using System;
using System.Collections.Generic;
using FluentAssertions;
using Xunit;

namespace CompanyX.Promotions.Tests
{
    public class PromoEngineTests
    {
        private readonly PromoEngine _engine = new PromoEngine();

        [Fact]
        public void CalculateOrderTotal_NullOrder_ThrowsException()
        {
            Func<decimal> act = () => _engine.CalculateOrderTotal(null);

            act.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void CalculateOrderTotal_EmptyOrder_ReturnsZero()
        {
            var order = new Order(new Dictionary<char, int>());
            const decimal expected = 0m;

            var actual = _engine.CalculateOrderTotal(order);

            actual.Should().Be(expected);
        }
    }
}