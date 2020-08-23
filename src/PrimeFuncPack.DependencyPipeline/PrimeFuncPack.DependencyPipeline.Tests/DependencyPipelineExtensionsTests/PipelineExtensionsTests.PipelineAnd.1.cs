﻿#nullable enable

using Moq;
using PrimeFuncPack.DependencyPipeline.Extensions;
using PrimeFuncPack.DependencyPipeline.Tests.Stubs;
using PrimeFuncPack.DependencyPipeline.Tests.TestEntities;
using PrimeFuncPack.UnitTest.Moq;
using System;
using Xunit;
using static PrimeFuncPack.UnitTest.Data.DataGenerator;

namespace PrimeFuncPack.DependencyPipeline.Tests
{
    partial class PipelineExtensionsTests
    {
        [Fact]
        public void AndFirst_SourcePipelineIsNull_ExpectArgumentNullException()
        {
            IDependencyPipeline<RefType> sourcePipeline = null!;

            var mockOtherResolver = MockFuncFactory.CreateMockResolver(new StructType());
            var otherPipeline = new StubDependencyPipeline<StructType>(mockOtherResolver.Object);

            var ex = Assert.Throws<ArgumentNullException>(() => _ = sourcePipeline.And(otherPipeline));
            Assert.Equal("sourcePipeline", ex.ParamName);
        }

        [Fact]
        public void AndFirst_OtherPipelineIsNull_ExpectArgumentNullException()
        {
            var mockSourceResolver = MockFuncFactory.CreateMockResolver(new RefType());
            var sourcePipeline = new StubDependencyPipeline<RefType>(mockSourceResolver.Object);

            var ex = Assert.Throws<ArgumentNullException>(() => _ = sourcePipeline.And<RefType, StructType>(null!));
            Assert.Equal("otherPipeline", ex.ParamName);
        }

        [Fact]
        public void AndFirst_ThenResolve_ExpectCallResolveSourceOnce()
        {
            var mockSourceResolver = MockFuncFactory.CreateMockResolver(new RefType());
            var sourcePipeline = new StubDependencyPipeline<RefType>(mockSourceResolver.Object);

            var mockOtherResolver = MockFuncFactory.CreateMockResolver(new StructType());
            var otherPipeline = new StubDependencyPipeline<StructType>(mockOtherResolver.Object);

            var actualPipeline = sourcePipeline.And(otherPipeline);

            var serviceProvider = Mock.Of<IServiceProvider>();
            _ = actualPipeline.Resolve(serviceProvider);

            mockSourceResolver.Verify(r => r.Resolve(serviceProvider), Times.Once);
        }

        [Fact]
        public void AndFirst_ThenResolve_ExpectCallResolveOtherOnce()
        {
            var mockSourceResolver = MockFuncFactory.CreateMockResolver(new RefType());
            var sourcePipeline = new StubDependencyPipeline<RefType>(mockSourceResolver.Object);

            var mockOtherResolver = MockFuncFactory.CreateMockResolver(new StructType());
            var otherPipeline = new StubDependencyPipeline<StructType>(mockOtherResolver.Object);

            var actualPipeline = sourcePipeline.And(otherPipeline);

            var serviceProvider = Mock.Of<IServiceProvider>();
            _ = actualPipeline.Resolve(serviceProvider);

            mockOtherResolver.Verify(r => r.Resolve(serviceProvider), Times.Once);
        }

        [Theory]
        [MemberData(nameof(TestEntityTestData.RefTypeTestSource), MemberType = typeof(TestEntityTestData))]
        public void AndFirst_ThenResolve_ExpectResolvedtupleValue(
            in RefType sourceValue)
        {
            var mockSourceResolver = MockFuncFactory.CreateMockResolver(sourceValue);
            var sourcePipeline = new StubDependencyPipeline<RefType>(mockSourceResolver.Object);

            var otherValue = new StructType
            {
                Text = GenerateText()
            };

            var mockOtherResolver = MockFuncFactory.CreateMockResolver(otherValue);
            var otherPipeline = new StubDependencyPipeline<StructType>(mockOtherResolver.Object);

            var actualPipeline = sourcePipeline.And(otherPipeline);

            var serviceProvider = Mock.Of<IServiceProvider>();
            var actual = actualPipeline.Resolve(serviceProvider);

            Assert.Equal(sourceValue, actual.First);
            Assert.Equal(otherValue, actual.Second);
        }
    }
}
