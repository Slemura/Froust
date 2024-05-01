using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using RpDev.Extensions.Unity;
using RpDev.Services.UI.Utils;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace RpDev.Services.UI
{
	[DisallowMultipleComponent]
	public class UIService : MonoBehaviour, IUIService
	{
		[SerializeField] private Transform _screenRoot;

		private UIScreenPrototypeProvider _prototypeProvider;

		private IObjectResolver _resolver; // TODO -> Factory

		private readonly Dictionary<Type, UIScreen> _shownScreens = new Dictionary<Type, UIScreen>();

		private bool _isPrototypeLoaded;
		
		[Inject]
		private void SetDependencies(IObjectResolver resolver)
		{
			_resolver = resolver;
		}

		private void Awake ()
		{
			_isPrototypeLoaded = false;
			LoadPrototypes().Forget();
		}

		private async UniTaskVoid LoadPrototypes()
		{
			_prototypeProvider = new UIScreenPrototypeProvider();
			await _prototypeProvider.LoadScreenPrototypesAsync();
			_isPrototypeLoaded = true;
		}

		public async UniTask<TScreen> SpawnScreen<TScreen> () where TScreen : UIScreen
		{
			if(_isPrototypeLoaded == false)
				await UniTask.WaitUntil(() => _isPrototypeLoaded);
			
			if (_shownScreens.TryGetValue(typeof(TScreen), out var screen))
				return (TScreen)screen;

			var prototype = _prototypeProvider.GetPrototype<TScreen>();

			screen = _resolver.Instantiate(prototype, _screenRoot)
			                  .WithGameObjectName(prototype.name);

			_shownScreens.Add(typeof(TScreen), screen);

			return (TScreen)screen;
		}

		public void DestroyScreen<TScreen> (TScreen screen) where TScreen : UIScreen
		{
			if (_shownScreens.TryGetValue(typeof(TScreen), out var shownScreen) == false)
				throw new Exception($"UI Screen '{typeof(TScreen).Name}' is not shown.");

			if (shownScreen != screen)
				throw new Exception($"UI Screen '{typeof(TScreen).Name}' is not the same as shown screen.");

			_shownScreens.Remove(typeof(TScreen));

			Destroy(screen.gameObject);
		}
	}
}
