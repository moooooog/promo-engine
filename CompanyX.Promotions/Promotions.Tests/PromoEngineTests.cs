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
            var order = new Order(new Dictionary<string, int>());
            const decimal expected = 0m;

            var actual = _engine.CalculateOrderTotal(order);

            actual.Should().Be(expected);
        }

        [Fact]
        public void CalculateOrderTotal_SingleA_Returns50()
        {
            var order = new Order(new Dictionary<string, int> {{"A", 1}});
            const decimal expected = 50m;

            var actual = _engine.CalculateOrderTotal(order);

            actual.Should().Be(expected);
        }

        [Fact]
        public void CalculateOrderTotal_TwoAs_Returns100()
        {
            var order = new Order(new Dictionary<string, int> {{"A", 2}});
            const decimal expected = 100m;

            var actual = _engine.CalculateOrderTotal(order);

            actual.Should().Be(expected);
        }
    }
}