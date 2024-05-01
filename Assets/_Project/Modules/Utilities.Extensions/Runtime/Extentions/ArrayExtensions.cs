using System;
using System.Linq;

namespace RpDev.Extensions
{
    public static partial class Extensions
    {
        public static T[] Fill<T>(this T[] array, T with) where T : struct
        {
            for (var i = 0; i < array.Length; i++)
                array[i] = with;

            return array;
        }

        public static T[] Fill<T>(this T[] array, Func<T> with) where T : struct
        {
            for (var i = 0; i < array.Length; i++)
                array[i] = with.Invoke();

            return array;
        }

        public static T[,] Fill<T>(this T[,] matrix, T with) where T : struct
        {
            for (var i = 0; i < matrix.GetLength(0); i++)
            {
                for (var j = 0; j < matrix.GetLength(1); j++)
                {
                    matrix[i, j] = with;
                }
            }

            return matrix;
        }

        public static T[,] ToSquare<T>(this T[] flatArray, int width)
        {
            var height = (int)Math.Ceiling(flatArray.Length / (double)width);
            var result = new T[width, height];

            for (var i = 0; i < flatArray.Length; i++)
                result[i % height, height - i / height - 1] = flatArray[i];

            return result;
        }

        public static T[,] Rotate<T>(this T[,] matrix, int width, int height)
        {
            var result = new T[height, width];

            for (var y = 0; y < height; y++)
            for (var x = 0; x < width; x++)
                result[y, x] = matrix[width - x - 1, y];

            return result;
        }

        public static T[] Flatten<T>(this T[,] squareArray)
        {
            return squareArray.Cast<T>().ToArray();
        }
    }
}