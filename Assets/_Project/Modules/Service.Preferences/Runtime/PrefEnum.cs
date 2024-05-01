using System;
using UnityEngine;

namespace RpDev.Services.Preferences.Values
{
    public class PrefEnum<TEnum> : PrefValue<TEnum> where TEnum : struct, Enum
    {
        public PrefEnum(string key, TEnum defaultValue) : base(key, defaultValue)
        {
        }

        public PrefEnum(string key, Func<TEnum> defaultValueGetter) : base(key, defaultValueGetter)
        {
        }

        protected private override TEnum RetrieveValue()
        {
            return Enum.TryParse(PlayerPrefs.GetString(Key), out TEnum result) ? result : DefaultValueGetter.Invoke();
        }

        protected private override void StoreValue(TEnum value)
        {
            PlayerPrefs.SetString(Key, value.ToString());
        }
    }
}