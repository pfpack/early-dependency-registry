#nullable enable

using System;
using PrimeFuncPack.UnitTest;
using Xunit;
using static PrimeFuncPack.UnitTest.TestData;

namespace PrimeFuncPack.Tests
{
    partial class SevenDependencyTest
    {
        [Theory]
        [MemberData(nameof(TestEntitySource.RecordTypes), MemberType = typeof(TestEntitySource))]
        public void ToFourth_ExpectResolvedValueIsEqualToFourthSource(
            RecordType fourthSource)
        {
            var source = Dependency.Create(
                _ => ThreeWhiteSpacesString,
                _ => PlusFifteenIdRefType,
                _ => new { Id = PlusFifteen },
                _ => fourthSource,
                _ => DateTimeKind.Unspecified,
                _ => SomeTextStructType,
                _ => true);

            var actual = source.ToFourth();

            var actualValue = actual.Resolve();
            Assert.Equal(fourthSource, actualValue);
        }
    }
}