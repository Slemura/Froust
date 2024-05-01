using System.Collections.Generic;

namespace RpDev.Extensions
{
    public static partial class Extensions
    {
        public static void Rotate<T>(this Queue<T> queue)
        {
            queue.Enqueue(queue.Dequeue());
        }
    }
}