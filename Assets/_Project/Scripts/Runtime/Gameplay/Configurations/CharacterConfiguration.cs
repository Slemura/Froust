using System;
using Froust.Level.Views;
using UnityEngine;

namespace Froust.Level.Configurations
{
    [Serializable]
    public class CharacterConfiguration<T> where T : RigidbodyView
    {
        [SerializeField] private T _characterView;
        [SerializeField] private float _pushBackForce;
        [SerializeField] private float _responsePushForce;

        public T CharacterView => _characterView;
        public float PushBackForce => _pushBackForce;
        public float ResponsePushForce => _responsePushForce;
    }
}