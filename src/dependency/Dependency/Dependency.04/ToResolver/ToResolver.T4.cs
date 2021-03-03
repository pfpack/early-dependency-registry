#nullable enable

using System;

namespace PrimeFuncPack
{
    partial class Dependency<T1, T2, T3, T4>
    {
        public Func<IServiceProvider, T4> ToFourthResolver()
            =>
            fourthResolver;
    }
}