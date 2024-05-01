using System;
using System.Globalization;
using TMPro;
using UnityEngine;

namespace Froust.Runtime.UI
{
    [DisallowMultipleComponent]
    public class FpsTracker : MonoBehaviour
    {
        [SerializeField] [Min(float.Epsilon)] private float _refreshRate = 0.5f;

        private TMP_Text _fpsText;

        private int _frameCache;
        private float _timeCache;
        private double _framerate;

        private void Awake() => _fpsText = GetComponent<TMP_Text>();
        private void Start() => UpdateText();
        private void Update() => UpdateText();

        private void UpdateText()
        {
            if (_timeCache < _refreshRate)
            {
                _timeCache += Time.unscaledDeltaTime;
                _frameCache++;
            }
            else
            {
                _framerate = _frameCache / _timeCache;
                _frameCache = 0;
                _timeCache = 0;

                _fpsText.SetText($"FPS: {Math.Round(_framerate, 1).ToString(CultureInfo.InvariantCulture)}");
            }
        }
    }
}