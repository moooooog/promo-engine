using System;
using CompanyX.Promotions.Rules;
using FluentAssertions;
using Xunit;

namespace CompanyX.Promotions.Tests.Rules.MultibuyPromoRuleTests
{
    public class SingleSkuTests
    {
        [Theory]
        [InlineData(null, "the sku id is null")]
        [InlineData("", "the sku id is empty")]
        [InlineData(" ", "the sku id is whitespace only")]
        public void Constructor_InvalidSkuId_ThrowsException(string skuId, string because)
        {
            Func<MultibuyPromoRule> act = () => new MultibuyPromoRule(skuId, 3, 1);

            act.Should().Throw<ArgumentException>(because);
        }

        [Theory]
        [InlineData(-1, "the unit count is negative")]
        [InlineData(0, "the unit count is zero")]
        public void Constructor_InvalidUnitCount_ThrowsException(int unitCount, string because)
        {
            Func<MultibuyPromoRule> act = () => new MultibuyPromoRule("A", unitCount, 1);

            act.Should().Throw<ArgumentOutOfRangeException>(because);
        }

        [Fact]
        public void Constructor_InvalidCombinedPrice_ThrowsException()
        {
            Func<MultibuyPromoRule> act = () => new MultibuyPromoRule("A", 1, -0.1m);

            act.Should().Throw<ArgumentOutOfRangeException>();
        }

        [Fact]
        public void Apply_OrderDoesNotContainSku_ReturnsZeroPrice()
        {
            var rule = new MultibuyPromoRule("A", 2, 10m);
            var order = new Order(new SkuQuantity[] { });
            const decimal expectedRulePrice = 0;

            var actualApplyResult = rule.Apply(order);

            actualApplyResult.RulePrice.Should().Be(expectedRulePrice);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        public void Apply_OrderContainsSkuButNoMultibuy_ReturnsZeroPrice(int unitCount)
        {
            var rule = new MultibuyPromoRule("A", 2, 10m);
            var order = new Order(new[] {new SkuQuantity("A", unitCount)});
            const decimal expectedRulePrice = 0;

            var actualApplyResult = rule.Apply(order);

            actualApplyResult.RulePrice.Should().Be(expectedRulePrice);
        }

        [Theory]
        [InlineData(2)]
        [InlineData(3)]
        public void Apply_OrderContainsSkuWithSingleMultibuy_ReturnsMultibuyPrice(int unitCount)
        {
            var rule = new MultibuyPromoRule("A", 2, 10m);
            var order = new Order(new[] {new SkuQuantity("A", unitCount)});
            const decimal expectedRulePrice = 10;

            var actualApplyResult = rule.Apply(order);

            actualApplyResult.RulePrice.Should().Be(expectedRulePrice);
        }

        [Theory]
        [InlineData(4)]
        [InlineData(5)]
        public void Apply_OrderContainsSkuWithMultipleMultibuy_ReturnsTotalPrice(int unitCount)
        {
            var rule = new MultibuyPromoRule("A", 2, 10m);
            var order = new Order(new[] {new SkuQuantity("A", unitCount)});
            const decimal expectedRulePrice = 20; // 2*10

            var actualApplyResult = rule.Apply(order);

            actualApplyResult.RulePrice.Should().Be(expectedRulePrice);
        }

        [Fact]
        public void Apply_OrderDoesNotContainSku_ReturnsNoSkusConsumed()
        {
            var rule = new MultibuyPromoRule("A", 2, 10m);
            var order = new Order(new SkuQuantity[] { });
            var expectedSkusConsumed = new[] {new SkuQuantity("A", 0)};

            var actualApplyResult = rule.Apply(order);

            actualApplyResult.SkusConsumed.Should().BeEquivalentTo(expectedSkusConsumed);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        public void Apply_OrderContainsSkuButNoMultibuy_ReturnsNoSkusConsumed(int unitCount)
        {
            var rule = new MultibuyPromoRule("A", 2, 10m);
            var order = new Order(new[] {new SkuQuantity("A", unitCount)});
            var expectedSkusConsumed = new[] {new SkuQuantity("A", 0)};

            var actualApplyResult = rule.Apply(order);

            actualApplyResult.SkusConsumed.Should().BeEquivalentTo(expectedSkusConsumed);
        }

        [Theory]
        [InlineData(2)]
        [InlineData(3)]
        public void Apply_OrderContainsSkuWithSingleMultibuy_ReturnsMultipleSkusConsumed(int unitCount)
        {
            var rule = new MultibuyPromoRule("A", 2, 10m);
            var order = new Order(new[] {new SkuQuantity("A", unitCount)});
            var expectedSkusConsumed = new[] {new SkuQuantity("A", 2)};

            var actualApplyResult = rule.Apply(order);

            actualApplyResult.SkusConsumed.Should().BeEquivalentTo(expectedSkusConsumed);
        }

        [Theory]
        [InlineData(4)]
        [InlineData(5)]
        public void Apply_OrderContainsSkuWithMultipleMultibuy_ReturnsTotalSkusConsumed(int unitCount)
        {
            var rule = new MultibuyPromoRule("A", 2, 10m);
            var order = new Order(new[] {new SkuQuantity("A", unitCount)});
            var expectedSkusConsumed = new[] {new SkuQuantity("A", 4)};

            var actualApplyResult = rule.Apply(order);

            actualApplyResult.SkusConsumed.Should().BeEquivalentTo(expectedSkusConsumed);
        }
    }
}