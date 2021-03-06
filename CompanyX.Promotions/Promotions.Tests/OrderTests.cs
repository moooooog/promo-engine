using System;
using System.Collections.Generic;
using FluentAssertions;
using FluentAssertions.Execution;
using Xunit;

namespace CompanyX.Promotions.Tests
{
    public class OrderTests
    {
        [Fact]
        public void Constructor_NullItems_ThrowsException()
        {
            Func<Order> act = () => new Order(null);

            act.Should().Throw<ArgumentNullException>();
        }

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
        public void SetSkuQuantity_NegativeQuantity_ThrowsException()
        {
            var order = new Order(new SkuQuantity[] { });

            Action act = () => order.SetSkuQuantity("A", -1);

            act.Should().Throw<ArgumentOutOfRangeException>();
        }

        [Fact]
        public void Subtract_NullItemsToSubtract_DoesNotChangeOrder()
        {
            var order = new Order(new[] {new SkuQuantity("A", 2)});
            const IEnumerable<SkuQuantity> itemsToSubtract = null;
            const int expectedQuantity = 2;

            order.Subtract(itemsToSubtract);
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

        [Fact]
        public void IsEmpty_NotInitialisedWithItems_ReturnsTrue()
        {
            var order = new Order(new SkuQuantity[] { });

            var actual = order.IsEmpty();

            actual.Should().BeTrue();
        }

        [Fact]
        public void IsEmpty_InitialisedWithItems_ReturnsFalse()
        {
            var order = new Order(new[] {new SkuQuantity("A", 1)});

            var actual = order.IsEmpty();

            actual.Should().BeFalse();
        }

        [Fact]
        public void Clone_SimpleOrder_ClonedOrderIsADifferentObjectReference()
        {
            var order = new Order(new[] {new SkuQuantity("A", 3)});

            var clone = order.Clone();

            clone.Should().NotBeSameAs(order);
        }

        [Fact]
        public void Clone_OrderWithOneItem_ClonedOrderContainsItem()
        {
            var order = new Order(new[] {new SkuQuantity("A", 3)});
            var expectedQuantity = 3;

            var clone = order.Clone();

            var actualQuantity = clone.GetSkuQuantity("A");

            actualQuantity.Should().Be(expectedQuantity);
        }
    }
}