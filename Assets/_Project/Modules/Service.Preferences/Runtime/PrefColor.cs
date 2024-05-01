using System;
using Newtonsoft.Json;
using UnityEngine;

namespace RpDev.Services.Preferences.Values
{
	public class PrefColor : PrefValue<Color>
	{
		public PrefColor (string key, Color defaultValue) : base(key, defaultValue) {}

		public PrefColor (string key, Func<Color> defaultValueGetter) : base(key, defaultValueGetter) {}

		protected private override Color RetrieveValue ()
		{
			return PlayerPrefs.HasKey(Key)
				? JsonConvert.DeserializeObject<Color>(PlayerPrefs.GetString(Key))
				: DefaultValueGetter.Invoke();
		}

		protected private override void StoreValue (Color value)
		{
			PlayerPrefs.SetString(Key, JsonConvert.SerializeObject(value));
		}
	}
}
