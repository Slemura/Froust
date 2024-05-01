using UnityEngine;
using UnityEngine.UI;


namespace Froust.Runtime.UI {
	[RequireComponent(typeof(Graphic))]
	public class ChangeYPosition : MonoBehaviour {
		#region Set in Inspector
		[SerializeField] private float offset;
		#endregion Set in Inspector

		private RectTransform graphicRectTransform;

		private void Awake () {
			graphicRectTransform = GetComponent <Graphic>().rectTransform;
		}

		// ReSharper disable once Unity.RedundantEventFunction
		private void Start () {}

		private Vector2 initialPosition;

		public void Change () {
			initialPosition = graphicRectTransform.anchoredPosition;

			graphicRectTransform.anchoredPosition += Vector2.down * offset;
		}

		public void Restore () {
			graphicRectTransform.anchoredPosition = initialPosition;
		}
	}
}
