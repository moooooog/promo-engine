using System;
using System.Collections.Generic;
using CompanyX.Promotions.Rules;
using FluentAssertions;
using Xunit;

namespace CompanyX.Promotions.Tests.PromoEngineTests
{
    public class NoPromotionTests
    {
        // Engine contains no promotion-based rules (just the unit price ones)
        private readonly IPromoEngine _engine = new PromoEngine(
            new List<IRule>
            {
                new UnitPriceRule(new Sku("A", 50)),
                new UnitPriceRule(new Sku("B", 30)),
                new UnitPriceRule(new Sku("C", 20)),
                new UnitPriceRule(new Sku("D", 15))
            });

        [Fact]
        public void CalculateOrderTotal_NullOrder_ThrowsException()
        {
            Func<decimal> act = () => _engine.CalculateOrderTotal(null);

            act.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void CalculateOrderTotal_EmptyOrder_ReturnsZero()
        {
            var order = new Order(Array.Empty<SkuQuantity>());
            const decimal expected = 0;

            var actual = _engine.CalculateOrderTotal(order);

            actual.Should().Be(expected);
        }

        [Fact]
        public void CalculateOrderTotal_SingleA_ReturnsAUnitPrice()
        {
            var order = new Order(new[] {new SkuQuantity("A", 1)});
            const decimal expected = 50;

            var actual = _engine.CalculateOrderTotal(order);

            actual.Should().Be(expected);
        }

        [Theory]
        [InlineData(2, 100)]
        [InlineData(3, 150)]
        public void CalculateOrderTotal_MultipleAWithNoPromotion_ReturnsTotalBasedOnUnitPrice(int quantity,
            decimal expectedTotal)
        {
            var order = new Order(new[] {new SkuQuantity("A", quantity)});

            var actual = _engine.CalculateOrderTotal(order);

            actual.Should().Be(expectedTotal);
        }

        [Theory]
        [InlineData(0, 0)]
        [InlineData(1, 30)]
        [InlineData(2, 60)]
        public void CalculateOrderTotal_ContainsOnlyB_ReturnsTotalBasedOnUnitPrice(int quantity, decimal expectedTotal)
        {
            var order = new Order(new[] {new SkuQuantity("B", quantity)});

            var actual = _engine.CalculateOrderTotal(order);

            actual.Should().Be(expectedTotal);
        }

        [Theory]
        [InlineData(0, 0)]
        [InlineData(1, 20)]
        [InlineData(2, 40)]
        public void CalculateOrderTotal_ContainsOnlyC_ReturnsTotalBasedOnUnitPrice(int quantity, decimal expectedTotal)
        {
            var order = new Order(new[] {new SkuQuantity("C", quantity)});

            var actual = _engine.CalculateOrderTotal(order);

            actual.Should().Be(expectedTotal);
        }

        [Theory]
        [InlineData(0, 0)]
        [InlineData(1, 15)]
        [InlineData(2, 30)]
        public void CalculateOrderTotal_ContainsOnlyD_ReturnsTotalBasedOnUnitPrice(int quantity, decimal expectedTotal)
        {
            var order = new Order(new[] {new SkuQuantity("D", quantity)});

            var actual = _engine.CalculateOrderTotal(order);

            actual.Should().Be(expectedTotal);
        }

        [Fact]
        public void CalculateOrderTotal_CombinedSingleUnitPerSku_ReturnsTotalBasedOnUnitPrices()
        {
            var order = new Order(new[]
            {
                new SkuQuantity("A", 1),
                new SkuQuantity("B", 1),
                new SkuQuantity("C", 1)
            });
            const decimal expected = 100; // 50 + 30 + 20

            var actual = _engine.CalculateOrderTotal(order);

            actual.Should().Be(expected);
        }

        [Fact]
        public void CalculateOrderTotal_CombinedMultipleUnitsPerSku_ReturnsTotalBasedOnUnitPricesAndMultiples()
        {
            var order = new Order(new[]
            {
                new SkuQuantity("A", 2),
                new SkuQuantity("B", 3)
            });
            const decimal expected = 190; // 2*50 + 3*30;

            var actual = _engine.CalculateOrderTotal(order);

            actual.Should().Be(expected);
        }

        [Fact]
        public void CalculateOrderTotal_UnprocessedItem_ThrowsException()
        {
            var order = new Order(new[]
            {
                new SkuQuantity("A", 2),
                new SkuQuantity("Z", 3)
            });

            Func<decimal> act = () => _engine.CalculateOrderTotal(order);

            act.Should().Throw<PromoEngineException>();
        }
    }
}