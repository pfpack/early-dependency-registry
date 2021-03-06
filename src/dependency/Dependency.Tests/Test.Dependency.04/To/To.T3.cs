#nullable enable

using PrimeFuncPack.UnitTest;
using Xunit;
using static PrimeFuncPack.UnitTest.TestData;

namespace PrimeFuncPack.Tests
{
    partial class FourDependencyTest
    {
        [Theory]
        [MemberData(nameof(TestEntitySource.RefTypes), MemberType = typeof(TestEntitySource))]
        public void ToThird_ExpectResolvedValueIsEqualToThirdSource(
            RefType? thirdSource)
        {
            var source = Dependency.Create(
                _ => default(StructType?), _ => MinusFifteenIdSomeStringNameRecord, _ => thirdSource, _ => ZeroIdRefType);

            var actual = source.ToThird();

            var actualValue = actual.Resolve();
            Assert.Equal(thirdSource, actualValue);
        }
    }
}