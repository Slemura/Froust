// ReSharper disable UnusedType.Global

using System;
using UnityEngine;


namespace RpDev.Services.Preferences.Values
{
	public class PrefFloat : PrefValue<float>
	{
		public PrefFloat (string key, float defaultValue) : base(key, defaultValue) {}

		public PrefFloat (string key, Func<float> defaultValueGetter) : base(key, defaultValueGetter) {}

		private protected override float RetrieveValue ()
		{
			return PlayerPrefs.GetFloat(Key, DefaultValueGetter.Invoke());
		}

		private protected override void StoreValue (float value)
		{
			PlayerPrefs.SetFloat(Key, value);
		}
	}
}
