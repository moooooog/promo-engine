using System;
using CompanyX.Promotions.Rules;
using FluentAssertions;
using Xunit;

namespace CompanyX.Promotions.Tests.Rules.MultibuyPromoRuleTests
{
    public class MultiSkuTests
    {
        [Fact]
        public void Constructor_ZeroUnitCount_ThrowsException()
        {
            // First rule item is valid, second is invalid
            var ruleItems = new[] {new SkuQuantity("A", 1), new SkuQuantity("B", 0)};
            Func<MultibuyPromoRule> act = () => new MultibuyPromoRule(ruleItems, 1);

            act.Should().Throw<ArgumentOutOfRangeException>();
        }

        [Fact]
        public void Constructor_InvalidCombinedPrice_ThrowsException()
        {
            var ruleItems = new[] {new SkuQuantity("A", 1), new SkuQuantity("B", 3)};
            Func<MultibuyPromoRule> act = () => new MultibuyPromoRule(ruleItems, -0.1m);

            act.Should().Throw<ArgumentOutOfRangeException>();
        }

        [Theory]
        [InlineData("A", "the id exactly matches another id")]
        [InlineData("a", "the id matches another id with a difference case")]
        public void Constructor_DuplicateSkuIds_ThrowsException(string skuId, string because)
        {
            var ruleItems = new[] {new SkuQuantity("A", 1), new SkuQuantity(skuId, 3)};
            Func<MultibuyPromoRule> act = () => new MultibuyPromoRule(ruleItems, 1);

            act.Should().Throw<ArgumentException>(because);
        }

        [Fact]
        public void Apply_OrderDoesNotContainSku_ReturnsZeroPrice()
        {
            var rule = new MultibuyPromoRule(new[] {new SkuQuantity("A", 1), new SkuQuantity("B", 1)}, 10m);
            var order = new Order(new SkuQuantity[] { });
            const decimal expectedRulePrice = 0;

            var actualApplyResult = rule.Apply(order);

            actualApplyResult.RulePrice.Should().Be(expectedRulePrice);
        }

        [Theory]
        [InlineData(0, 0)]
        [InlineData(1, 0)]
        [InlineData(0, 1)]
        public void Apply_OrderDoesNotContainMultiSkuMultibuy_ReturnsZeroPrice(int unitCountA, int unitCountB)
        {
            var rule = new MultibuyPromoRule(new[] {new SkuQuantity("A", 1), new SkuQuantity("B", 1)}, 10m);
            var order = new Order(new[] {new SkuQuantity("A", unitCountA), new SkuQuantity("B", unitCountB)});
            const decimal expectedRulePrice = 0;

            var actualApplyResult = rule.Apply(order);

            actualApplyResult.RulePrice.Should().Be(expectedRulePrice);
        }

        [Theory]
        [InlineData(1, 1)]
        [InlineData(2, 1)]
        [InlineData(1, 2)]
        public void Apply_OrderContainsSingleMultiSkuMultibuy_ReturnsMultibuyPrice(int unitCountA, int unitCountB)
        {
            var rule = new MultibuyPromoRule(new[] {new SkuQuantity("A", 1), new SkuQuantity("B", 1)}, 10m);
            var order = new Order(new[] {new SkuQuantity("A", unitCountA), new SkuQuantity("B", unitCountB)});
            const decimal expectedRulePrice = 10;

            var actualApplyResult = rule.Apply(order);

            actualApplyResult.RulePrice.Should().Be(expectedRulePrice);
        }

        [Theory]
        [InlineData(2, 2)]
        [InlineData(3, 2)]
        [InlineData(2, 3)]
        public void Apply_OrderContainsMultipleMultiSkuMultibuy_ReturnsTotalPrice(int unitCountA, int unitCountB)
        {
            var rule = new MultibuyPromoRule(new[] {new SkuQuantity("A", 1), new SkuQuantity("B", 1)}, 10m);
            var order = new Order(new[] {new SkuQuantity("A", unitCountA), new SkuQuantity("B", unitCountB)});
            const decimal expectedRulePrice = 20; // 2*10

            var actualApplyResult = rule.Apply(order);

            actualApplyResult.RulePrice.Should().Be(expectedRulePrice);
        }

        [Fact]
        public void Apply_OrderDoesNotContainSku_ReturnsNoSkusConsumed()
        {
            var rule = new MultibuyPromoRule(new[] {new SkuQuantity("A", 1), new SkuQuantity("B", 1)}, 10m);
            var order = new Order(new SkuQuantity[] { });
            var expectedSkusConsumed = new[] {new SkuQuantity("A", 0), new SkuQuantity("B", 0)};

            var actualApplyResult = rule.Apply(order);

            actualApplyResult.SkusConsumed.Should().BeEquivalentTo(expectedSkusConsumed);
        }

        [Theory]
        [InlineData(0, 0)]
        [InlineData(1, 0)]
        [InlineData(0, 1)]
        public void Apply_OrderDoesNotContainMultiSkuMultibuy_ReturnsNoSkusConsumed(int unitCountA, int unitCountB)
        {
            var rule = new MultibuyPromoRule(new[] {new SkuQuantity("A", 1), new SkuQuantity("B", 1)}, 10m);
            var order = new Order(new[] {new SkuQuantity("A", unitCountA), new SkuQuantity("B", unitCountB)});
            var expectedSkusConsumed = new[] {new SkuQuantity("A", 0), new SkuQuantity("B", 0)};

            var actualApplyResult = rule.Apply(order);

            actualApplyResult.SkusConsumed.Should().BeEquivalentTo(expectedSkusConsumed);
        }

        [Theory]
        [InlineData(1, 1)]
        [InlineData(2, 1)]
        [InlineData(1, 2)]
        public void Apply_OrderContainsSingleMultiSkuMultibuy_ReturnsTotalSkusConsumed(int unitCountA, int unitCountB)
        {
            var rule = new MultibuyPromoRule(new[] {new SkuQuantity("A", 1), new SkuQuantity("B", 1)}, 10m);
            var order = new Order(new[] {new SkuQuantity("A", unitCountA), new SkuQuantity("B", unitCountB)});
            var expectedSkusConsumed = new[] {new SkuQuantity("A", 1), new SkuQuantity("B", 1)};

            var actualApplyResult = rule.Apply(order);

            actualApplyResult.SkusConsumed.Should().BeEquivalentTo(expectedSkusConsumed);
        }

        [Theory]
        [InlineData(2, 2)]
        [InlineData(3, 2)]
        [InlineData(2, 3)]
        public void Apply_OrderContainsMultipleMultiSkuMultibuy_ReturnsTotalSkusConsumed(int unitCountA, int unitCountB)
        {
            var rule = new MultibuyPromoRule(new[] {new SkuQuantity("A", 1), new SkuQuantity("B", 1)}, 10m);
            var order = new Order(new[] {new SkuQuantity("A", unitCountA), new SkuQuantity("B", unitCountB)});
            var expectedSkusConsumed = new[] {new SkuQuantity("A", 2), new SkuQuantity("B", 2)};

            var actualApplyResult = rule.Apply(order);

            actualApplyResult.SkusConsumed.Should().BeEquivalentTo(expectedSkusConsumed);
        }

        [Fact]
        public void Apply_OrderContainsMultipleLargeMultiSkuMultibuy_ReturnsTotalPrice()
        {
            // Rule contains multiple A's and multiple B's
            var rule = new MultibuyPromoRule(new[] {new SkuQuantity("A", 3), new SkuQuantity("B", 2)}, 10m);
            var order = new Order(new[] {new SkuQuantity("A", 10), new SkuQuantity("B", 20)});
            const decimal expectedRulePrice = 30; // 3*10

            var actualApplyResult = rule.Apply(order);

            actualApplyResult.RulePrice.Should().Be(expectedRulePrice);
        }

        [Fact]
        public void Apply_OrderContainsMultipleLargeMultiSkuMultibuy_ReturnsTotalSkusConsumed()
        {
            // Rule contains multiple A's and multiple B's
            var rule = new MultibuyPromoRule(new[] {new SkuQuantity("A", 3), new SkuQuantity("B", 2)}, 10m);
            var order = new Order(new[] {new SkuQuantity("A", 10), new SkuQuantity("B", 20)});
            var expectedSkusConsumed = new[] {new SkuQuantity("A", 9), new SkuQuantity("B", 6)};

            var actualApplyResult = rule.Apply(order);

            actualApplyResult.SkusConsumed.Should().BeEquivalentTo(expectedSkusConsumed);
        }
    }
}