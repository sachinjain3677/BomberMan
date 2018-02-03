using UnityEngine;

namespace Lean.Touch
{
	// This script allows you to transform the current GameObject with smoothing
	public class LeanTranslateSmooth : MonoBehaviour
	{
		[Tooltip("Ignore fingers with StartedOverGui?")]
		public bool IgnoreGuiFingers = true;

		[Tooltip("Allows you to force rotation with a specific amount of fingers (0 = any)")]
		public int RequiredFingerCount;

		[Tooltip("Does translation require an object to be selected?")]
		public LeanSelectable RequiredSelectable;
		
		[Tooltip("The camera the translation will be calculated using (default = MainCamera)")]
		public Camera Camera;

		[Tooltip("How smoothly this object moves to its target position")]
		public float Dampening = 10.0f;

		// The position we still need to add
		[HideInInspector]
		public Vector3 RemainingDelta;

#if UNITY_EDITOR
		protected virtual void Reset()
		{
			if (RequiredSelectable == null)
			{
				RequiredSelectable = GetComponent<LeanSelectable>();
			}
		}
#endif

		protected virtual void Update()
		{
			// If we require a selectable and it isn't selected, cancel translation
			if (RequiredSelectable != null && RequiredSelectable.IsSelected == false)
			{
				return;
			}

			// Get the fingers we want to use
			var fingers = LeanTouch.GetFingers(IgnoreGuiFingers, RequiredFingerCount, RequiredSelectable);

			// Calculate the screenDelta value based on these fingers
			var screenDelta = LeanGesture.GetScreenDelta(fingers);

			// Perform the translation
			Translate(screenDelta);
		}

		protected virtual void LateUpdate()
		{
			// Get t value
			var factor = LeanTouch.GetDampenFactor(Dampening, Time.deltaTime);

			// Dampen remainingDelta
			var newDelta = Vector3.Lerp(RemainingDelta, Vector3.zero, factor);

			// Shift this transform by the change in delta
			transform.position += RemainingDelta - newDelta;

			// Update remainingDelta with the dampened value
			RemainingDelta = newDelta;
		}

		private void Translate(Vector2 screenDelta)
		{
			// If camera is null, try and get the main camera, return true if a camera was found
			if (LeanTouch.GetCamera(ref Camera) == true)
			{
				// Store old position
				var oldPosition = transform.position;

				// Screen position of the transform
				var screenPosition = Camera.WorldToScreenPoint(oldPosition);
			
				// Add the deltaPosition
				screenPosition += (Vector3)screenDelta;
			
				// Convert back to world space
				var newPosition = Camera.ScreenToWorldPoint(screenPosition);
				var delPosition = newPosition - oldPosition;

				// Add to delta
				RemainingDelta += delPosition;
			}
		}
	}
}