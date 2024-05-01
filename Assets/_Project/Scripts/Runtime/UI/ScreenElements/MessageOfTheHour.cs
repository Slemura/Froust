using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;


namespace Froust.Runtime.UI
{
	[RequireComponent(typeof(TMP_Text))]
	public class MessageOfTheHour : MonoBehaviour
	{
		private TMP_Text _text;

		private readonly string[] _positiveAdvices = {
			"Embrace change and adapt to new challenges.",
			"Always be kind and compassionate to others.",
			"Pursue your passions and follow your dreams.",
			"Practice gratitude for the little things in life.",
			"Take care of your physical and mental health.",
			"Learn from your mistakes and keep moving forward.",
			"Stay curious and never stop learning.",
			"Believe in yourself and your abilities.",
			"Find joy in the simple moments.",
			"Set meaningful goals and work towards them.",
			"Be patient. Good things take time.",
			"Stay humble and open-minded.",
			"Help others whenever you can.",
			"Stay true to your values.",
			"Take breaks and prioritize self-care.",
			"Be a good listener.",
			"Focus on solutions, not problems.",
			"Keep a positive attitude, even in tough times.",
			"Celebrate your achievements, no matter how small.",
			"Treat yourself with kindness and self-compassion.",
			"Foster a sense of wonder and awe.",
		};

		private void Awake ()
		{
			_text = GetComponent<TMP_Text>();
		}

		private void Start ()
		{
			string advice = _positiveAdvices[Random.Range(0, _positiveAdvices.Length)];

			_text.text = string.Format(_text.text, advice);
		}
	}
}
