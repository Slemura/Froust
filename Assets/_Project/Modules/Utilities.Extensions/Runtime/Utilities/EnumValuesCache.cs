using System;
using System.Collections.Generic;


namespace RpDev.Utilities {
	public static class EnumValuesCache {
		
		private static readonly Dictionary <Type, Array> Cache = new Dictionary <Type, Array>();
		
		private static Array GetValues<T> () where T : Enum {
			var enumType = typeof(T);

			if (Cache.TryGetValue(enumType, out var values))
				return values;

			values = Enum.GetValues(enumType);
			Cache.Add(enumType, values);

			return values;
		}

		public static T GetValueAt<T> (int index) where T : Enum {
			var values = GetValues <T>();
			return (T)values.GetValue(index);
		}

		public static int Length<T> () where T : Enum {
			var values = GetValues <T>();
			return values.Length;
		}
	}
}
