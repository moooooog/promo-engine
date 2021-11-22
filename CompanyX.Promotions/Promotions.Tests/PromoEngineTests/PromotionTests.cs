using System.Collections.Generic;
using System.Linq;
using CompanyX.Promotions.Rules;
using FluentAssertions;
using Xunit;

namespace CompanyX.Promotions.Tests.PromoEngineTests
{
    public class PromotionTests
    {
        private readonly IPromoEngine _engine;

        public PromotionTests()
        {
            var unitPriceRules = new List<IRule>
            {
                new UnitPriceRule(new Sku("A", 50)),
                new UnitPriceRule(new Sku("B", 30)),
                new UnitPriceRule(new Sku("C", 20)),
                new UnitPriceRule(new Sku("D", 15))
            };

            var promotionRuleA = new MultibuyPromoRule(new SkuQuantity("A", 3), 130);
            var promotionRuleB = new MultibuyPromoRule(new SkuQuantity("B", 2), 45);
            var promotionRuleCAndD = new MultibuyPromoRule(
                new[]
                {
                    new SkuQuantity("C", 1),
                    new SkuQuantity("D", 1)
                },
                30);

            var allRules = new[]
                {
                    promotionRuleA,
                    promotionRuleB,
                    promotionRuleCAndD
                }
                .Concat(unitPriceRules);

            _engine = new PromoEngine(allRules);
        }

        [Fact]
        public void CalculateOrderTotal_ScenarioA_ReturnsCorrectTotal()
        {
            var order = new Order(new[]
            {
                new SkuQuantity("A", 1),
                new SkuQuantity("B", 1),
                new SkuQuantity("C", 1)
            });
            const decimal expected = 100;

            var actual = _engine.CalculateOrderTotal(order);

            actual.Should().Be(expected);
        }

        [Fact]
        public void CalculateOrderTotal_ScenarioB_ReturnsCorrectTotal()
        {
            var order = new Order(new[]
            {
                new SkuQuantity("A", 5),
                new SkuQuantity("B", 5),
                new SkuQuantity("C", 1)
            });
            const decimal expected = 370;

            var actual = _engine.CalculateOrderTotal(order);

            actual.Should().Be(expected);
        }

        [Fact]
        public void CalculateOrderTotal_ScenarioC_ReturnsCorrectTotal()
        {
            var order = new Order(new[]
            {
                new SkuQuantity("A", 3),
                new SkuQuantity("B", 5),
                new SkuQuantity("C", 1),
                new SkuQuantity("D", 1)
            });
            const decimal expected = 280;

            var actual = _engine.CalculateOrderTotal(order);

            actual.Should().Be(expected);
        }
    }
}