using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace BigMansStuff.NAudio.Tests
{
    #region ByteAndFloatsConverter - Conversion Utility Structure (Byte[] <-> Float[])

    /// <summary>
    /// Helper Structure - Allows "C-Style forced pointer" conversion of Bytes array to Floats array and visa versa (C# does not allow this)
    /// The main benefit is performance - no need to iterate and convert each element in the array
    /// Taken from: http://stackoverflow.com/questions/619041/what-is-the-fastest-way-to-convert-a-float-to-a-byte
    /// </summary>
    [StructLayout(LayoutKind.Explicit)]
    public struct ByteAndFloatsConverter
    {
        [FieldOffset(0)]
        public Byte[] Bytes;

        [FieldOffset(0)]
        public float[] Floats;
    }

    #endregion
}
