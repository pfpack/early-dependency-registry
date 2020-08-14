﻿#nullable enable

namespace System
{
    partial struct Optional<T>
    {
        public static bool Same(in Optional<T> optionalA, in Optional<T> optionalB)
            =>
            Box<T>.Same(optionalA.box, optionalB.box);

        public bool Same(in Optional<T> other)
            =>
            Same(this, other);

        public int GetSamenessHashCode()
            =>
            HashCode.Combine(SamenessContract, GetBoxSamenessHashCode());

        private static Type SamenessContract => typeof(Optional<T>);

        private int GetBoxSamenessHashCode()
            =>
            box switch { not null => box.GetSamenessHashCode(), _ => default };
    }
}
