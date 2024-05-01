using System;
using UnityEngine;
using UnityEngine.SceneManagement;


namespace RpDev.Services.AudioService
{
	public sealed class AudioGroupInfo : IDisposable
	{
		public AudioSource AudioSource {get;}

		public AudioGroupInfo (AudioSource audioSource, bool stopOnSceneChange)
		{
			AudioSource = audioSource;

			if (stopOnSceneChange)
				SceneManager.activeSceneChanged += StopOnSceneChange;
		}

		private void StopOnSceneChange (Scene _, Scene __) => AudioSource.Stop();

		public void Dispose ()
		{
			SceneManager.activeSceneChanged -= StopOnSceneChange;
		}
	}
}
