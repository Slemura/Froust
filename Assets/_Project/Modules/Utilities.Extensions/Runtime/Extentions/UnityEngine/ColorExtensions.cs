using System;
using System.Globalization;
using UnityEngine;
using Random = UnityEngine.Random;

namespace RpDev.Extensions.Unity
{
    public static class ColorExtensions
    {
        /// <summary>
        /// Gets the hexadecimal representation of the RGB values of this <see cref="Color"/>.
        /// </summary>
        /// <returns>The hexadecimal representation of the RGB values of this <see cref="Color"/>.</returns>
        public static string Name(this Color self)
        {
            return self.ToHexRGB();
        }

        /// <summary>
        /// Returns a new <see cref="Color"/> with the specified alpha value.
        /// </summary>
        /// <param name="value">The alpha value to set for the new <see cref="Color"/>. Must be between 0 and 1, inclusive.</param>
        /// <returns>A new <see cref="Color"/> with the specified alpha value.</returns>
        public static Color WithAlpha(this Color self, float value)
        {
            self.a = Mathf.Clamp01(value);
            return self;
        }

        /// <summary>
        /// Returns a new <see cref="Color"/> with the specified red value.
        /// </summary>
        /// <param name="value">The red value to set for the new <see cref="Color"/>. Must be between 0 and 1, inclusive.</param>
        /// <returns>A new <see cref="Color"/> with the specified red value.</returns>
        public static Color WithRed(this Color self, float value)
        {
            self.r = Mathf.Clamp01(value);
            return self;
        }

        /// <summary>
        /// Returns a new <see cref="Color"/> with the specified green value.
        /// </summary>
        /// <param name="value">The green value to set for the new <see cref="Color"/>. Must be between 0 and 1, inclusive.</param>
        /// <returns>A new <see cref="Color"/> with the specified green value.</returns>
        public static Color WithGreen(this Color self, float value)
        {
            self.g = Mathf.Clamp01(value);
            return self;
        }

        /// <summary>
        /// Returns a new <see cref="Color"/> with the specified blue value.
        /// </summary>
        /// <param name="value">The blue value to set for the new <see cref="Color"/>. Must be between 0 and 1, inclusive.</param>
        /// <returns>A new <see cref="Color"/> with the specified blue value.</returns>
        public static Color WithBlue(this Color self, float value)
        {
            self.b = Mathf.Clamp01(value);
            return self;
        }

        /// <summary>
        /// Gets the hexadecimal representation of the RGB values of this <see cref="Color"/>.
        /// </summary>
        /// <returns>The hexadecimal representation of the RGB values of this <see cref="Color"/>, in the format "#RRGGBB".</returns>
        public static string ToHexRGB(this Color self)
        {
            return $"#{ColorUtility.ToHtmlStringRGB(self)}";
        }

        /// <summary>
        /// Gets the hexadecimal representation of the RGBA values of this <see cref="Color"/>.
        /// </summary>
        /// <returns>The hexadecimal representation of the RGBA values of this <see cref="Color"/>, in the format "#RRGGBBAA".</returns>
        public static string ToHexRGBA(this Color self)
        {
            return $"#{ColorUtility.ToHtmlStringRGBA(self)}";
        }

        /// <summary>
        /// Gets the hue value of this <see cref="Color"/> in the HSV color space.
        /// </summary>
        /// <returns>The hue value of this <see cref="Color"/>, in the range 0-1.</returns>
        public static float Hue(this Color self)
        {
            Color.RGBToHSV(self, out float hue, out float _, out float _);
            return hue;
        }

        /// <summary>
        /// Gets the saturation of this <see cref="Color"/> in the HSV color space.
        /// </summary>
        /// <returns>The saturation of this <see cref="Color"/>, in the range 0-1.</returns>
        public static float Saturation(this Color self)
        {
            Color.RGBToHSV(self, out float _, out float saturation, out float _);
            return saturation;
        }

        /// <summary>
        /// Gets the value (brightness) of this <see cref="Color"/> in the HSV color space.
        /// </summary>
        /// <returns>The value (brightness) of this <see cref="Color"/>, in the range 0-1.</returns>
        public static float Value(this Color self)
        {
            Color.RGBToHSV(self, out float _, out float _, out float value);
            return value;
        }

        /// <summary>
        /// Lightens this <see cref="Color"/> by a specified amount.
        /// </summary>
        /// <param name="factor">The amount to lighten this <see cref="Color"/> by. Must be between 0 and 1, inclusive.</param>
        public static void Lighten(this ref Color self, float factor = 0.2f)
        {
            self = Color.Lerp(self, Color.white, factor.Clamped01());
        }

        /// <summary>
        /// Returns a lighter version of this <see cref="Color"/> by a specified amount.
        /// </summary>
        /// <param name="factor">The amount to lighten this <see cref="Color"/> by. Must be between 0 and 1, inclusive.</param>
        /// <returns>A lighter version of this <see cref="Color"/> by the specified amount.</returns>
        public static Color Lighter(this Color self, float factor = 0.2f)
        {
            return Color.Lerp(self, Color.white, factor.Clamped01());
        }

        /// <summary>
        /// Darkens this <see cref="Color"/> by a specified amount.
        /// </summary>
        /// <param name="factor">The amount to darken this <see cref="Color"/> by. Must be between 0 and 1, inclusive.</param>
        public static void Darken(this ref Color self, float factor = 0.2f)
        {
            self = Color.Lerp(self, Color.black, factor.Clamped01());
        }

        /// <summary>
        /// Returns a darker version of this <see cref="Color"/> by a specified amount.
        /// </summary>
        /// <param name="factor">The amount to darken this <see cref="Color"/> by. Must be between 0 and 1, inclusive.</param>
        /// <returns>A darker version of this <see cref="Color"/> by the specified amount.</returns>
        public static Color Darker(this Color self, float factor = 0.2f)
        {
            return Color.Lerp(self, Color.black, factor.Clamped01());
        }

        /// <summary>
        /// Randomizes the specified channels of this <see cref="Color"/>.
        /// </summary>
        /// <param name="red">If set to <c>true</c>, the red channel will be randomized.</param>
        /// <param name="green">If set to <c>true</c>, the green channel will be randomized.</param>
        /// <param name="blue">If set to <c>true</c>, the blue channel will be randomized.</param>
        /// <param name="alpha">If set to <c>true</c>, the alpha channel will be randomized.</param>
        /// <exception cref="ArgumentException">Thrown when all parameters are set to <c>false</c>.</exception>
        public static void Randomize(this ref Color self, bool red = true, bool green = true, bool blue = true,
            bool alpha = false)
        {
            if (red == false && green == false && blue == false && alpha == false)
                throw new ArgumentException("At least one color channel must be set to true.");

            self.r = red ? GetNewRandomValue(self.r) : self.r;
            self.g = green ? GetNewRandomValue(self.g) : self.g;
            self.b = blue ? GetNewRandomValue(self.b) : self.b;
            self.a = alpha ? GetNewRandomValue(self.a) : self.a;
        }

        /// <summary>
        /// Returns a new <see cref="Color"/> with randomly chosen values for the specified color channels.
        /// </summary>
        /// <param name="red">If set to <c>true</c>, the red channel will be randomized.</param>
        /// <param name="green">If set to <c>true</c>, the green channel will be randomized.</param>
        /// <param name="blue">If set to <c>true</c>, the blue channel will be randomized.</param>
        /// <param name="alpha">If set to <c>true</c>, the alpha channel will be randomized.</param>
        /// <returns>A new <see cref="Color"/> with randomly chosen values for the specified color channels.</returns>
        /// <exception cref="ArgumentException">Thrown if all parameters are set to false.</exception>
        public static Color Randomized(this Color self, bool red = true, bool green = true, bool blue = true,
            bool alpha = false)
        {
            if (red == false && green == false && blue == false && alpha == false)
                throw new ArgumentException("At least one color channel must be set to true.");

            Color result = new Color(self.r, self.g, self.b, self.a);

            result.r = red ? GetNewRandomValue(self.r) : result.r;
            result.g = green ? GetNewRandomValue(self.g) : result.g;
            result.b = blue ? GetNewRandomValue(self.b) : result.b;
            result.a = alpha ? GetNewRandomValue(self.a) : result.a;

            return result;
        }

        /// <summary>
        /// Returns the red, green, and blue components of this <see cref="Color"/> as a tuple.
        /// </summary>
        /// <returns>A tuple containing the red, green, and blue components of this <see cref="Color"/>.</returns>
        public static (float red, float green, float blue) ToTupleRGB(this Color self)
        {
            return (self.r, self.g, self.b);
        }

        /// <summary>
        /// Returns the red, green, blue and alpha components of this <see cref="Color"/> as a tuple.
        /// </summary>
        /// <returns>A tuple containing the red, green, blue and alpha components of this <see cref="Color"/>.</returns>
        public static (float red, float green, float blue, float alpha) ToTupleRGBA(this Color self)
        {
            return (self.r, self.g, self.b, self.a);
        }

        /// <summary>
        /// Converts a hexadecimal string to a <see cref="Color"/>.
        /// </summary>
        /// <param name="hexString">The hexadecimal string to convert. Must be in the format "#RRGGBB" or "#RRGGBBAA".</param>
        public static void FromHex(this ref Color color, string hexString)
        {
            if (hexString.StartsWith("#"))
                hexString = hexString.Substring(1);

            if (hexString.Length != 6 && hexString.Length != 8)
                throw new ArgumentException(
                    "Invalid hex string format. The string must have 6 or 8 characters, including the '#' symbol.");

            float r = int.Parse(hexString.Substring(0, 2), NumberStyles.HexNumber) / 255f;
            float g = int.Parse(hexString.Substring(2, 2), NumberStyles.HexNumber) / 255f;
            float b = int.Parse(hexString.Substring(4, 2), NumberStyles.HexNumber) / 255f;

            float a = hexString.Length == 8
                ? int.Parse(hexString.Substring(6, 2), NumberStyles.HexNumber) / 255f
                : 1f;

            color.r = r;
            color.g = g;
            color.b = b;
            color.a = a;
        }

        /// <summary>
        /// Converts a <see cref="Color"/> from linear space to gamma space.
        /// </summary>
        /// <returns>A new <see cref="Color"/> object in gamma space.</returns>
        public static Color LinearToGamma(this Color self)
        {
            return new Color(
                Mathf.LinearToGammaSpace(self.r),
                Mathf.LinearToGammaSpace(self.g),
                Mathf.LinearToGammaSpace(self.b),
                Mathf.LinearToGammaSpace(self.a)
            );
        }

        /// <summary>
        /// Converts a <see cref="Color"/> from gamma space to linear space.
        /// </summary>
        /// <returns>A new <see cref="Color"/> object in linear space.</returns>
        public static Color GammaToLinear(this ref Color self)
        {
            return new Color(
                Mathf.GammaToLinearSpace(self.r),
                Mathf.GammaToLinearSpace(self.g),
                Mathf.GammaToLinearSpace(self.b),
                Mathf.GammaToLinearSpace(self.a)
            );
        }

        private static float GetNewRandomValue(float originalValue)
        {
            float randomValue;

            do
            {
                randomValue = Random.value;
            } while (randomValue.Equals(originalValue));

            return randomValue;
        }
    }
}