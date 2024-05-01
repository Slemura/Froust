using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace RpDev.Services.Preferences.Values
{
    public abstract class PrefValue<T>
    {
        protected readonly Func<T> DefaultValueGetter;
        protected readonly string Key;

        private event Action<T> Listeners = delegate { };

        protected private abstract T RetrieveValue();

        protected private abstract void StoreValue(T value);

        protected PrefValue(string key, T defaultValue)
        {
            this.Key = key;
            DefaultValueGetter = () => defaultValue;
        }

        protected PrefValue(string key, Func<T> defaultValueGetter)
        {
            this.Key = key;
            this.DefaultValueGetter = defaultValueGetter;
        }

        public T Value
        {
            get => RetrieveValue();
            set
            {
                var isValueChanged = EqualityComparer<T>.Default.Equals(Value, value) == false;

                if (isValueChanged == false)
                    return;

                StoreValue(value);
                Listeners?.Invoke(value);
            }
        }

        public void SetToDefault()
        {
            Value = DefaultValueGetter.Invoke();
        }

        public void AddListener(Action<T> onValueChanged)
        {
            if (Listeners.GetInvocationList().Contains(onValueChanged))
            {
                Debug.LogWarning($"{onValueChanged.Method.Name} is already registered");
                return;
            }

            Listeners += onValueChanged;
        }

        public void RemoveListener(Action<T> onValueChanged)
        {
            Listeners -= onValueChanged;
        }
    }
}