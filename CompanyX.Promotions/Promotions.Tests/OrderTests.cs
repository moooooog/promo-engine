using FluentAssertions;
using FluentAssertions.Execution;
using Xunit;

namespace CompanyX.Promotions.Tests
{
    public class OrderTests
    {
        [Fact]
        public void GetSkuQuantity_SkuIdExists_ReturnsQuantity()
        {
            var order = new Order(new[] {new SkuQuantity("A", 2)});
            const int expectedQuantity = 2;

            var actualQuantity = order.GetSkuQuantity("A");

            actualQuantity.Should().Be(expectedQuantity);
        }

        [Fact]
        public void GetSkuQuantity_SkuIdDoesNotExist_ReturnsZero()
        {
            var order = new Order(new[] {new SkuQuantity("A", 2)});
            const int expectedQuantity = 0;

            var actualQuantity = order.GetSkuQuantity("B");

            actualQuantity.Should().Be(expectedQuantity);
        }

        [Fact]
        public void SetSkuQuantity_AddNewSkuQuantity_ShouldBeRetrievable()
        {
            var order = new Order(new SkuQuantity[] { });
            const int expectedQuantity = 5;

            order.SetSkuQuantity("B", 5);
            var actualQuantity = order.GetSkuQuantity("B");

            actualQuantity.Should().Be(expectedQuantity);
        }

        [Fact]
        public void SetSkuQuantity_ReplaceExistingSkuQuantity_ShouldBeRetrievable()
        {
            var order = new Order(new[] {new SkuQuantity("A", 2)});
            const int expectedQuantity = 5;

            order.SetSkuQuantity("A", 5);
            var actualQuantity = order.GetSkuQuantity("A");

            actualQuantity.Should().Be(expectedQuantity);
        }

        [Fact]
        public void Subtract_ZeroQuantity_DoesNotChangeOrder()
        {
            var order = new Order(new[] {new SkuQuantity("A", 2)});
            var itemsToSubtract = new[] {new SkuQuantity("A", 0)};
            const int expectedQuantity = 2;

            order.Subtract(itemsToSubtract);
            var actualQuantity = order.GetSkuQuantity("A");

            actualQuantity.Should().Be(expectedQuantity);
        }

        [Fact]
        public void Subtract_PositiveQuantity_ReducesSkuQuantity()
        {
            var order = new Order(new[] {new SkuQuantity("A", 5)});
            var itemsToSubtract = new[] {new SkuQuantity("A", 2)};
            const int expectedQuantity = 3;

            order.Subtract(itemsToSubtract);
            var actualQuantity = order.GetSkuQuantity("A");

            actualQuantity.Should().Be(expectedQuantity);
        }

        [Fact]
        public void Subtract_MultipleSkusQuantitiesRemoved_ReducesSkuQuantities()
        {
            var order = new Order(new[] {new SkuQuantity("A", 5), new SkuQuantity("B", 3)});
            var itemsToSubtract = new[] {new SkuQuantity("A", 2), new SkuQuantity("B", 1)};
            const int expectedQuantityA = 3;
            const int expectedQuantityB = 2;

            order.Subtract(itemsToSubtract);
            var actualQuantityA = order.GetSkuQuantity("A");
            var actualQuantityB = order.GetSkuQuantity("B");

            using (new AssertionScope())
            {
                actualQuantityA.Should().Be(expectedQuantityA);
                actualQuantityB.Should().Be(expectedQuantityB);
            }
        }
    }
}