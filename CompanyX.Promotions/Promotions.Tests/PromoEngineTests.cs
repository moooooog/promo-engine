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
        public void CalculateOrderTotal_SingleA_ReturnsAUnitPrice()
        {
            var order = new Order(new Dictionary<string, int> {{"A", 1}});
            const decimal expected = 50m;

            var actual = _engine.CalculateOrderTotal(order);

            actual.Should().Be(expected);
        }

        [Theory]
        [InlineData(2, 100)]
        [InlineData(3, 150)]
        public void CalculateOrderTotal_MultipleAWithNoPromotion_ReturnsTotalBasedOnUnitPrice(int quantity,
            decimal expectedTotal)
        {
            var order = new Order(new Dictionary<string, int> {{"A", quantity}});

            var actual = _engine.CalculateOrderTotal(order);

            actual.Should().Be(expectedTotal);
        }

        [Theory]
        [InlineData(0, 0)]
        [InlineData(1, 30)]
        [InlineData(2, 60)]
        public void CalculateOrderTotal_ContainsOnlyB_ReturnsTotalBasedOnUnitPrice(int quantity, decimal expectedTotal)
        {
            var order = new Order(new Dictionary<string, int> {{"B", quantity}});

            var actual = _engine.CalculateOrderTotal(order);

            actual.Should().Be(expectedTotal);
        }

        [Theory]
        [InlineData(0, 0)]
        [InlineData(1, 20)]
        [InlineData(2, 40)]
        public void CalculateOrderTotal_ContainsOnlyC_ReturnsTotalBasedOnUnitPrice(int quantity, decimal expectedTotal)
        {
            var order = new Order(new Dictionary<string, int> { { "C", quantity } });

            var actual = _engine.CalculateOrderTotal(order);

            actual.Should().Be(expectedTotal);
        }

        [Theory]
        [InlineData(0, 0)]
        [InlineData(1, 15)]
        [InlineData(2, 30)]
        public void CalculateOrderTotal_ContainsOnlyD_ReturnsTotalBasedOnUnitPrice(int quantity, decimal expectedTotal)
        {
            var order = new Order(new Dictionary<string, int> { { "D", quantity } });

            var actual = _engine.CalculateOrderTotal(order);

            actual.Should().Be(expectedTotal);
        }

        /// <summary>
        /// This is "Scenario A" from the problem statement.
        /// </summary>
        [Fact]
        public void CalculateOrderTotal_CombinedSingleSkus_ReturnsTotalBasedOnUnitPrices()
        {
            var order = new Order(new Dictionary<string, int>
            {
                { "A", 1 },
                { "B", 1 },
                { "C", 1 }
            });
            const decimal expected = 100m;

            var actual = _engine.CalculateOrderTotal(order);

            actual.Should().Be(expected);
        }
    }
}