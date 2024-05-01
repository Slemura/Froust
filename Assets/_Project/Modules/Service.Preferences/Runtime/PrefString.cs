using System;
using UnityEngine;

namespace RpDev.Services.Preferences.Values
{
    public class PrefString : PrefValue<string>
    {
        public PrefString(string key, string defaultValue) : base(key, defaultValue)
        {
        }

        public PrefString(string key, Func<string> defaultValueGetter) : base(key, defaultValueGetter)
        {
        }

        public bool IsNullOrEmpty => string.IsNullOrEmpty(Value);

        protected private override string RetrieveValue()
        {
            return PlayerPrefs.GetString(Key, DefaultValueGetter.Invoke());
        }

        protected private override void StoreValue(string value)
        {
            PlayerPrefs.SetString(Key, value);
        }
    }
}