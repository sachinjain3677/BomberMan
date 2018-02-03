using UnityEngine;

namespace Lean.Touch
{
	// This script allows you to track & pedestral the current camera by dragging your finger(s)
	[RequireComponent(typeof(Camera))]
	public class LeanCameraMove : MonoBehaviour
	{
		[Tooltip("Ignore fingers with StartedOverGui?")]
		public bool IgnoreGuiFingers = true;

		[Tooltip("Ignore fingers if the finger count doesn't match? (0 = any)")]
		public int RequiredFingerCount;

		[Tooltip("The distance from the camera the world drag delta will be calculated from (this only matters for perspective cameras)")]
		public float Distance = 1.0f;

		protected virtual void LateUpdate()
		{
			// Get the fingers we want to use
			var fingers = LeanTouch.GetFingers(IgnoreGuiFingers, RequiredFingerCount);

			// Get the required camera
			var camera = GetComponent<Camera>();

			// Get the world delta of all the fingers
			var worldDelta = LeanGesture.GetWorldDelta(fingers, Distance, camera);

			// Pan the camera based on the world delta
			transform.position -= worldDelta;
		}
	}
}