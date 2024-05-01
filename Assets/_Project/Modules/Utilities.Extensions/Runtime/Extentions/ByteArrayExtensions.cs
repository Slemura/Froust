using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;

namespace RpDev.Extensions
{
    public static partial class Extensions
    {
        public static byte[] Compressed(this byte[] bytes)
        {
            var output = new MemoryStream();

            using (var stream = new DeflateStream(output, CompressionLevel.Fastest))
            {
                stream.Write(bytes, 0, bytes.Length);
            }

            return output.ToArray();
        }

        public static sbyte[] Compressed(this sbyte[] signedBytes)
        {
            return (sbyte[])(Array)Compressed((byte[])(Array)signedBytes);
        }

        public static byte[] Decompressed(this byte[] bytes)
        {
            var input = new MemoryStream(bytes);
            var output = new MemoryStream();

            using (var stream = new DeflateStream(input, CompressionMode.Decompress))
            {
                stream.CopyTo(output);
            }

            return output.ToArray();
        }

        public static sbyte[] Decompressed(this sbyte[] signedBytes)
        {
            return (sbyte[])(Array)Decompressed((byte[])(Array)signedBytes);
        }

        public static byte[] SelectBlock(this byte[] bytes, int start, int length)
        {
            var block = new byte[length];
            Buffer.BlockCopy(bytes, start, block, 0, length);
            return block;
        }

        public static byte[][] Split(this byte[] bytes, byte separator)
        {
            var result = new List<byte[]>();

            var blockStart = int.MinValue;
            var blockLength = 0;

            for (var i = 0; i <= bytes.Length; i++)
            {
                if (i == bytes.Length || bytes[i].Equals(separator))
                {
                    if (blockStart >= 0)
                        result.Add(SelectBlock(bytes, blockStart, blockLength));

                    blockStart = int.MinValue;
                    blockLength = 0;
                    continue;
                }

                if (blockStart < 0)
                    blockStart = i;

                blockLength++;
            }

            return result.ToArray();
        }
    }
}