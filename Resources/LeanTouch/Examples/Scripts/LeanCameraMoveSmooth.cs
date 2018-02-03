using UnityEngine;

namespace Lean.Touch
{
	// This script allows you to smoothly track & pedestal the current camera by dragging your finger(s)
	[RequireComponent(typeof(Camera))]
	public class LeanCameraMoveSmooth : MonoBehaviour
	{
		[Tooltip("Ignore fingers with StartedOverGui?")]
		public bool IgnoreGuiFingers = true;

		[Tooltip("Ignore fingers if the finger count doesn't match? (0 = any)")]
		public int RequiredFingerCount;

		[Tooltip("The distance from the camera the world drag delta will be calculated from (this only matters for perspective cameras)")]
		public float Distance = 1.0f;

		[Tooltip("How quickly the zoom reaches the target value")]
		public float Dampening = 10.0f;

		[HideInInspector]
		public Vector3 RemainingDelta;

		protected virtual void LateUpdate()
		{
			// Get the fingers we want to use
			var fingers = LeanTouch.GetFingers(IgnoreGuiFingers, RequiredFingerCount);

			// Get the required camera
			var camera = GetComponent<Camera>();

			// Get the world delta of all the fingers
			var worldDelta = LeanGesture.GetWorldDelta(fingers, Distance, camera);

			// Pan the camera based on the world delta
			RemainingDelta -= worldDelta;

			// Get t value
			var factor = LeanTouch.GetDampenFactor(Dampening, Time.deltaTime);

			// Dampen remainingDelta
			var newDelta = Vector3.Lerp(RemainingDelta, Vector3.zero, factor);

			// Shift this transform by the change in delta
			transform.position += RemainingDelta - newDelta;

			// Update remainingDelta with the dampened value
			RemainingDelta = newDelta;
		}
	}
}