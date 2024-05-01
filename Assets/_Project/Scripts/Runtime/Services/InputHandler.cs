using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Froust.Runtime.Services
{
    [RequireComponent(typeof(EventSystem))]
    public class InputHandler : MonoBehaviour
    {
        public bool Enabled => EventSystem.enabled;
        public GameObject SelectedGameObject => EventSystem.currentSelectedGameObject;
        public EventSystem EventSystem { get; private set; }

        private void Awake()
        {
            EventSystem = GetComponent<EventSystem>();
        }

        public bool IsPointerOverGameObject(int pointerId = -1)
        {
            return EventSystem.IsPointerOverGameObject(pointerId);
        }

        public void SuspendInput()
        {
            EventSystem.enabled = false;
        }

        public void ResumeInput()
        {
            EventSystem.enabled = true;
        }

        public void SetSelectedGameObject(GameObject selected)
        {
            EventSystem.SetSelectedGameObject(selected);
        }

        public IDisposable InputSuspended()
        {
            return new InputSuspendedScope(this);
        }

        private sealed class InputSuspendedScope : IDisposable
        {
            private readonly InputHandler _inputHandler;

            public InputSuspendedScope(InputHandler inputHandler)
            {
                _inputHandler = inputHandler;

                _inputHandler.SuspendInput();
            }

            public void Dispose()
            {
                _inputHandler.ResumeInput();
            }
        }
    }
}