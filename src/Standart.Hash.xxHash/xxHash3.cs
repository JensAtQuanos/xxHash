// ReSharper disable InconsistentNaming

using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Standart.Hash.xxHash
{
    public static partial class xxHash3
    {
        /// <summary>
        /// Compute xxHash for the data byte array
        /// </summary>
        /// <param name="data">The source of data</param>
        /// <param name="length">The length of the data for hashing</param>
        /// <param name="seed">The seed number</param>
        /// <returns>hash</returns>
        public static unsafe ulong ComputeHash(byte[] data, int length, ulong seed = 0)
        {
            Debug.Assert(data != null);
            Debug.Assert(length >= 0);
            Debug.Assert(length <= data.Length);
            
            fixed (byte* ptr = &data[0])
            {
                return UnsafeComputeHash(ptr, length, seed);
            }
        }  
        
        /// <summary>
        /// Compute xxHash for the data byte span 
        /// </summary>
        /// <param name="data">The source of data</param>
        /// <param name="length">The length of the data for hashing</param>
        /// <param name="seed">The seed number</param>
        /// <returns>hash</returns>
        public static unsafe ulong ComputeHash(Span<byte> data, int length, ulong seed = 0)
        {
            Debug.Assert(data != null);
            Debug.Assert(length >= 0);
            Debug.Assert(length <= data.Length);

            fixed (byte* ptr = &data[0])
            {
                return UnsafeComputeHash(ptr, length, seed);
            }
        }
        
        /// <summary>
        /// Compute xxHash for the data byte span 
        /// </summary>
        /// <param name="data">The source of data</param>
        /// <param name="length">The length of the data for hashing</param>
        /// <param name="seed">The seed number</param>
        /// <returns>hash</returns>
        public static unsafe ulong ComputeHash(ReadOnlySpan<byte> data, int length, ulong seed = 0)
        {
            Debug.Assert(data != null);
            Debug.Assert(length >= 0);
            Debug.Assert(length <= data.Length);

            fixed (byte* ptr = &data[0])
            {
                return UnsafeComputeHash(ptr, length, seed);
            }
        }
        
        /// <summary>
        /// Compute xxHash for the string 
        /// </summary>
        /// <param name="str">The source of data</param>
        /// <param name="seed">The seed number</param>
        /// <returns>hash</returns>
        public static unsafe ulong ComputeHash(string str, ulong seed = 0)
        {
            Debug.Assert(str != null);
            
            fixed (char* c = str)
            {
                byte* ptr = (byte*) c;
                int length = str.Length * 2;
                
                return UnsafeComputeHash(ptr, length, seed);
            }
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static unsafe ulong UnsafeComputeHash(byte* input, int len, ulong seed)
        {
            fixed (byte* secret = &XXH3_SECRET[0])
            {
                // Use inlined version
                return XXH3_64bits_internal(input, len, seed, secret, XXH3_SECRET_DEFAULT_SIZE);
            }
        }
    }
}