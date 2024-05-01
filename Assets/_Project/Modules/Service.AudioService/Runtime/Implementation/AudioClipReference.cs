using System;
using JetBrains.Annotations;
using UnityEngine;


namespace RpDev.Services.AudioService
{
	[Serializable]
	public class AudioClipReference
	{
		[UsedImplicitly]
		[SerializeField] private AudioClipPack _audioClipPack;
		[SerializeField] private string _id;
		[SerializeField] private float  _volume = 1f;

		public string Id     => _id;
		public float  Volume => _volume;

		public static event Action<AudioPlaybackRequest> PlaybackRequested;

		// TODO audio: pitch
		private void PlayAs (AudioClipType audioClipType, float? volume, float pitch)
		{
			AudioPlaybackRequest request = new AudioPlaybackRequest(
				audioClipType,
				_id,
				volume ?? _volume,
				pitch
			);

			PlaybackRequested?.Invoke(request);
		}

		public void PlayAsUI (float? volume = null, float pitch = 1)
		{
			PlayAs(AudioClipType.UI, volume, pitch);
		}

		public void PlayAsSfx (float? volume = null, float pitch = 1)
		{
			PlayAs(AudioClipType.Sfx, volume, pitch);
		}

		// TODO audio
		// public void PlayAsMusic (float volume = 1f)
		// {
		// 	AudioPlaybackRequest request = new AudioPlaybackRequest {
		// 		Volume = volume
		// 	};
		//
		// 	PlaybackRequested?.Invoke(this, AudioClipType.Music, request);
		// }

		// public void PlayAsAmbient (float volume = 1f)
		// {
		// 	AudioPlaybackRequest request = new AudioPlaybackRequest {
		// 		Volume = volume
		// 	};
		//
		// 	PlaybackRequested?.Invoke(this, AudioClipType.Ambient, request);
		// }

		// public void PlayAsVoice (float volume = 1f)
		// {
		// 	AudioPlaybackRequest request = new AudioPlaybackRequest {
		// 		Volume = volume
		// 	};
		//
		// 	PlaybackRequested?.Invoke(this, AudioClipType.Voice, request);
		// }
		//
		// public void PlayAsFootsteps (float volume = 1f)
		// {
		// 	AudioPlaybackRequest request = new AudioPlaybackRequest {
		// 		Volume = volume
		// 	};
		//
		// 	PlaybackRequested?.Invoke(this, AudioClipType.Footsteps, request);
		// }
	}
}
