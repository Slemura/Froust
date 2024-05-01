using UnityEngine;


namespace RpDev.Services.AudioService
{
	public class MusicPlayer : MonoBehaviour
	{
		[SerializeField, Range(0, 1)] private float _volume = 1f;

		[SerializeField] private AudioClipPack _music;

		private void Start ()
		{
			_music.PlayRandomAsMusic(_volume);
		}
	}
}
