#nullable enable

using System;

namespace PrimeFuncPack
{
    partial class Dependency<T1, T2, T3, T4, T5, T6, T7, T8>
    {
        public static implicit operator Func<IServiceProvider, T7>(
            Dependency<T1, T2, T3, T4, T5, T6, T7, T8> dependency)
            =>
            throw new NotImplementedException();
    }
}