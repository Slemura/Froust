using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Froust.Runtime.UI
{
    [AddComponentMenu("UI/Switcher", 32)]
    [ExecuteAlways]
    [RequireComponent(typeof(RectTransform))]
    public class Switcher : UIBehaviour, IPointerClickHandler, ISubmitHandler, ICanvasElement
    {
        private enum SwitchTransition
        {
            None,
            Tween
        }

        [Serializable]
        public class SwitchEvent : UnityEvent<bool> { }
        
        #region Set in Inspector

        [SerializeField] private bool _interactable = true;

        [Space] 
        [SerializeField] private Color _onColor;

        [Space] 
        [SerializeField] private Image _background;
        [SerializeField] private Image _handleGraphics;

        [field: Space]
        [field: Tooltip("Is the switch currently on or off?")]
        [field: SerializeField]
        public bool IsOn { get; private set; }

        [SerializeField] private SwitchTransition _switchTransition = SwitchTransition.Tween;
        [SerializeField] private RectTransform _handle;

        [Space] 
        [SerializeField] public SwitchEvent _onValueChanged = new SwitchEvent();

        #endregion Set in Inspector
        
        private Tween _tween;
        private bool _delayedUpdateVisuals;

        #if UNITY_EDITOR
        protected override void OnValidate()
        {
            base.OnValidate();

            if (IsActive())
                _delayedUpdateVisuals = true;

            if (!UnityEditor.PrefabUtility.IsPartOfPrefabAsset(this) && !Application.isPlaying)
                CanvasUpdateRegistry.RegisterCanvasElementForLayoutRebuild(this);
        }
        #endif

        protected override void Awake()
        {
            base.Awake();

            if (Application.isPlaying == false)
                return;

            const float duration = 0.15f;

            _tween = DOTween.Sequence()
                .Append(_handle.DOAnchorMin(Vector2.right, duration).From(Vector2.zero).SetRelative())
                .Join(_handle.DOAnchorMax(Vector2.right, duration).From(Vector2.zero).SetRelative())
                .Join(_background.DOColor(_onColor, duration))
                .SetEase(Ease.InOutSine)
                .Pause()
                .SetAutoKill(false)
                .SetLink(gameObject, LinkBehaviour.KillOnDestroy);
        }

        protected override void Start()
        {
            PlayEffect(true);
        }

        protected override void OnEnable()
        {
            base.OnEnable();

            PlayEffect(true);
        }

        protected virtual void Update()
        {
            if (Application.isPlaying == false || _delayedUpdateVisuals == false)
                return;

            _delayedUpdateVisuals = false;
            PlayEffect(true);
        }

        public virtual void Rebuild(CanvasUpdate executing)
        {
            #if UNITY_EDITOR
            if (executing == CanvasUpdate.Prelayout)
                _onValueChanged.Invoke(IsOn);
            #endif
        }

        public void LayoutComplete() { }

        public void GraphicUpdateComplete() { }

        public void SetIsOnWithoutNotify(bool value)
        {
            Set(value, false, true);
        }

        private void Set(bool value, bool sendCallback = true, bool instant = false)
        {
            if (IsOn == value)
                return;

            IsOn = value;

            PlayEffect(instant || _switchTransition == SwitchTransition.None);

            if (sendCallback == false)
                return;

            _onValueChanged.Invoke(IsOn);
        }

        private void PlayEffect(bool instant)
        {
            if (_handle == null)
                return;

            #if UNITY_EDITOR
            if (Application.isPlaying == false)
            {
                _handle.anchorMin = new Vector2(IsOn ? 1 : 0, _handle.anchorMin.y);
                _handle.anchorMax = new Vector2(IsOn ? 1 : 0, _handle.anchorMax.y);
                return;
            }
            #endif

            if (instant)
            {
                _handle.anchorMin = new Vector2(IsOn ? 1 : 0, _handle.anchorMin.y);
                _handle.anchorMax = new Vector2(IsOn ? 1 : 0, _handle.anchorMax.y);

                if (_tween != null)
                    _tween.fullPosition = IsOn ? _tween.Duration() : 0;
            }
            else
            {
                if (IsOn)
                {
                    _tween.PlayForward();
                }
                else
                {
                    _tween.PlayBackwards();
                }
            }
        }

        private void InternalToggle()
        {
            if (IsActive() == false || _interactable == false)
                return;

            IsOn = !IsOn;

            PlayEffect(_switchTransition == SwitchTransition.None);

            _onValueChanged?.Invoke(IsOn);
        }

        public virtual void OnPointerClick(PointerEventData eventData)
        {
            if (eventData.button != PointerEventData.InputButton.Left)
                return;

            InternalToggle();
        }

        public virtual void OnSubmit(BaseEventData eventData)
        {
            InternalToggle();
        }
    }
}