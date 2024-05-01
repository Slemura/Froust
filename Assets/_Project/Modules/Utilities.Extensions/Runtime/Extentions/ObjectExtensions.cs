using System;
using System.Collections.Generic;
using System.Reflection;
using Newtonsoft.Json;

namespace RpDev.Extensions
{
    public static partial class Extensions
    {
        public static string ToJson(this object obj)
        {
            return JsonConvert.SerializeObject(obj, Formatting.Indented);
        }

        public static bool IsInRange<T>(this T item, T start, T end) where T : IComparable<T>
        {
            return Comparer<T>.Default.Compare(item, start) >= 0 &&
                   Comparer<T>.Default.Compare(item, end) <= 0;
        }

        public static void SetNonPublicValue(this object obj,
            string name,
            object value,
            BindingFlags bindingAttr = BindingFlags.NonPublic | BindingFlags.Instance)
        {
            obj.GetType()
                .GetField(name, bindingAttr)?
                .SetValue(obj, value);
        }

        public static TValue GetNonPublicValue<TValue>(this object obj,
            string name,
            BindingFlags bindingAttr = BindingFlags.NonPublic | BindingFlags.Instance)
        {
            object valueObject = obj.GetType()
                .GetField(name, bindingAttr)?
                .GetValue(obj);

            if (valueObject is TValue value)
                return value;

            throw new ArgumentException();
        }

        public static void InvokeNonPublicMember(this object obj,
            string name,
            BindingFlags invokeAttr = BindingFlags.NonPublic | BindingFlags.InvokeMethod | BindingFlags.Instance,
            Binder binder = null,
            object[] args = null)
        {
            obj.GetType().InvokeMember(name, invokeAttr, binder, obj, args);
        }
    }
}