// ReSharper disable UnusedType.Global

using System;
using UnityEngine;


namespace RpDev.Services.Preferences.Values
{
	public class PrefInt : PrefValue<int>
	{
		public PrefInt (string key, int defaultValue) : base(key, defaultValue) {}

		public PrefInt (string key, Func<int> defaultValueGetter) : base(key, defaultValueGetter) {}

		protected private override int RetrieveValue ()
		{
			return PlayerPrefs.GetInt(Key, DefaultValueGetter.Invoke());
		}

		protected private override void StoreValue (int value)
		{
			PlayerPrefs.SetInt(Key, value);
		}
	}
}
