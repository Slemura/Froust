using System;
using UnityEngine;

namespace RpDev.Services.Preferences.Values
{
    public class PrefBool : PrefValue<bool>
    {
        public PrefBool(string key, bool defaultValue) : base(key, defaultValue)
        {
        }

        public PrefBool(string key, Func<bool> defaultValueGetter) : base(key, defaultValueGetter)
        {
        }

        protected private override bool RetrieveValue()
        {
            return PlayerPrefs.GetInt(Key, DefaultValueGetter.Invoke() ? 1 : 0) == 1;
        }

        protected private override void StoreValue(bool value)
        {
            PlayerPrefs.SetInt(Key, value ? 1 : 0);
        }
    }
}