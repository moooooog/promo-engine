using System;
using FluentAssertions;
using Xunit;

namespace CompanyX.Promotions.Tests
{
    public class SkuIdTests
    {
        [Theory]
        [InlineData(null, "the id is null")]
        [InlineData("", "the id is empty")]
        [InlineData(" ", "the id is whitespace only")]
        public void Constructor_InvalidId_ThrowsException(string id, string because)
        {
            Func<SkuId> act = () => new SkuId(id);

            act.Should().Throw<ArgumentException>(because);
        }

        [Theory]
        [InlineData("A")]
        [InlineData("a")]
        public void ToString_ValidSku_ReturnsSkuAsString(string idString)
        {
            var id = new SkuId(idString);
            var expected = idString;

            var actual = id.ToString();

            actual.Should().Be(expected);
        }

        [Theory]
        [InlineData("A", "The letter and case match")]
        [InlineData("a", "The letter matches but different case")]
        public void Equals_EquivalentSkuIds_AreEqual(string idString, string because)
        {
            var id1 = new SkuId("A");
            var id2 = new SkuId(idString);

            var actual = id1.Equals(id2);

            actual.Should().BeTrue(because);
        }

        [Fact]
        public void Equals_SameObjectReference_AreEqual()
        {
            var id1 = new SkuId("A");
            var id2 = id1;

            var actual = id1.Equals(id2);

            actual.Should().BeTrue();
        }

        [Fact]
        public void Equals_NonEquivalentSkuIds_AreNotEqual()
        {
            var id1 = new SkuId("A");
            var id2 = new SkuId("B");

            var actual = id1.Equals(id2);

            actual.Should().BeFalse();
        }

        [Fact]
        public void GetHashCode_IdsWithDifferentCase_ReturnsIdenticalHashCodes()
        {
            var hashCode1 = new SkuId("A").GetHashCode();
            var hashCode2 = new SkuId("a").GetHashCode();

            hashCode2.Equals(hashCode1).Should().BeTrue();
        }

        [Fact]
        public void GetHashCode_DifferentIds_ReturnsDifferentHashCodes()
        {
            var hashCode1 = new SkuId("A").GetHashCode();
            var hashCode2 = new SkuId("B").GetHashCode();

            hashCode2.Equals(hashCode1).Should().BeFalse();
        }

        [Fact]
        public void ImplicitCast_IdAsString_CreatesSkuId()
        {
            var id = new SkuId("A");
            SkuId castId = "A";

            castId.Equals(id).Should().BeTrue();
        }
    }
}
