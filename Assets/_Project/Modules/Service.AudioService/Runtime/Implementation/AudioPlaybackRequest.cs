namespace RpDev.Services.AudioService
{
	public struct AudioPlaybackRequest
	{
		public readonly AudioClipType AudioClipType;
		public readonly string        AudioClipId;
		public readonly float         Volume;
		public readonly float?        Pitch; // TODO audio

		public AudioPlaybackRequest (
			AudioClipType audioClipType,
			string audioClipId,
			float volume,
			float pitch
		)
		{
			AudioClipType = audioClipType;
			AudioClipId   = audioClipId;
			Volume        = volume;
			Pitch         = pitch;
		}
	}
}
