using System;
using RpDev.Extensions.Unity;
using Newtonsoft.Json;
using UnityEngine;

namespace RpDev.Extensions
{
    public static partial class Extensions
    {
        public static string Capitalize(this string input)
        {
            if (input == null)
                throw new NullReferenceException("Input string cannot be null.");

            if (input.Length == 0)
                return input;

            return char.ToUpper(input[0]) + input[1..];
        }

        public static string TrimLast(this string input)
        {
            if (input == null)
                throw new NullReferenceException("Input string cannot be null.");

            return input.Remove(input.Length - 1);
        }

        public static T FromJson<T>(this string input)
        {
            if (input == null)
                throw new NullReferenceException("Input string cannot be null.");

            return JsonConvert.DeserializeObject<T>(input);
        }

        public static string SplitCamelCase(this string input)
        {
            if (input == null)
                throw new NullReferenceException("Input string cannot be null.");

            return System.Text.RegularExpressions.Regex
                .Replace(input, "([A-Z])", " $1", System.Text.RegularExpressions.RegexOptions.Compiled).Trim();
        }

        public static string Color(this string input, Color color, bool editorOnly = false)
        {
            if (input == null)
                throw new NullReferenceException("Input string cannot be null.");

            var result = $"<color={color.Name()}>{input}</color>";

            if (editorOnly)
                return Application.isEditor ? result : input;

            return result;
        }

        public static string Bold(this string input, bool editorOnly = false)
        {
            if (input == null)
                throw new NullReferenceException("Input string cannot be null.");

            var result = $"<b>{input}</b>";

            if (editorOnly)
                return Application.isEditor ? result : input;

            return result;
        }
    }
}