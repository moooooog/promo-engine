using System;
using CompanyX.Promotions.Rules;
using FluentAssertions;
using Xunit;

namespace CompanyX.Promotions.Tests.Rules
{
    public class UnitPriceRuleTests
    {
        [Fact]
        public void Constructor_NullSku_ThrowsException()
        {
            Func<UnitPriceRule> act = () => new UnitPriceRule(null);

            act.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void Apply_OrderDoesNotContainsSku_ReturnsZeroPrice()
        {
            var rule = new UnitPriceRule(new Sku("A", 10));
            var order = new Order(new SkuQuantity[] { });
            const decimal expectedRulePrice = 0;

            var actualApplyResult = rule.Apply(order);

            actualApplyResult.RulePrice.Should().Be(expectedRulePrice);
        }

        [Fact]
        public void Apply_OrderContainsSkuWithZeroQuantity_ReturnsZeroPrice()
        {
            var rule = new UnitPriceRule(new Sku("A", 10));
            var order = new Order(new[] {new SkuQuantity("A", 0)});
            const decimal expectedRulePrice = 0;

            var actualApplyResult = rule.Apply(order);

            actualApplyResult.RulePrice.Should().Be(expectedRulePrice);
        }

        [Fact]
        public void Apply_OrderContainsSkuWithSingleQuantity_ReturnsUnitPrice()
        {
            var rule = new UnitPriceRule(new Sku("A", 10));
            var order = new Order(new[] {new SkuQuantity("A", 1)});
            const decimal expectedRulePrice = 10;

            var actualApplyResult = rule.Apply(order);

            actualApplyResult.RulePrice.Should().Be(expectedRulePrice);
        }

        [Fact]
        public void Apply_OrderContainsSkuWithMultipleQuantity_ReturnsTotalPrice()
        {
            var rule = new UnitPriceRule(new Sku("A", 10));
            var order = new Order(new[] {new SkuQuantity("A", 2)});
            const decimal expectedRulePrice = 20;

            var actualApplyResult = rule.Apply(order);

            actualApplyResult.RulePrice.Should().Be(expectedRulePrice);
        }

        [Fact]
        public void Apply_OrderDoesNotContainsSku_ReturnsNoSkusConsumed()
        {
            var rule = new UnitPriceRule(new Sku("A", 10));
            var order = new Order(new SkuQuantity[] { });
            var expectedSkusConsumed = new[] {new SkuQuantity("A", 0)};

            var actualApplyResult = rule.Apply(order);

            actualApplyResult.SkusConsumed.Should().BeEquivalentTo(expectedSkusConsumed);
        }

        [Fact]
        public void Apply_OrderContainsSkuWithZeroQuantity_ReturnsNoSkusConsumed()
        {
            var rule = new UnitPriceRule(new Sku("A", 10));
            var order = new Order(new[] {new SkuQuantity("A", 0)});
            var expectedSkusConsumed = new[] {new SkuQuantity("A", 0)};

            var actualApplyResult = rule.Apply(order);

            actualApplyResult.SkusConsumed.Should().BeEquivalentTo(expectedSkusConsumed);
        }

        [Fact]
        public void Apply_OrderContainsSkuWithSingleQuantity_ReturnsSingleSkuConsumed()
        {
            var rule = new UnitPriceRule(new Sku("A", 10));
            var order = new Order(new[] {new SkuQuantity("A", 1)});
            var expectedSkusConsumed = new[] {new SkuQuantity("A", 1)};

            var actualApplyResult = rule.Apply(order);

            actualApplyResult.SkusConsumed.Should().BeEquivalentTo(expectedSkusConsumed);
        }

        [Fact]
        public void Apply_OrderContainsSkuWithMultipleQuantity_ReturnsMultipleSkusConsumed()
        {
            var rule = new UnitPriceRule(new Sku("A", 10));
            var order = new Order(new[] {new SkuQuantity("A", 2)});
            var expectedSkusConsumed = new[] {new SkuQuantity("A", 2)};

            var actualApplyResult = rule.Apply(order);

            actualApplyResult.SkusConsumed.Should().BeEquivalentTo(expectedSkusConsumed);
        }
    }
}