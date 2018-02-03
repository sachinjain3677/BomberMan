using UnityEngine;

namespace Lean.Touch
{
	// This script allows you to zoom a camera in and out based on the pinch gesture
	// This supports both perspective and orthographic cameras
	[ExecuteInEditMode]
	public class LeanCameraZoomSmooth : LeanCameraZoom
	{
		[Tooltip("How quickly the zoom reaches the target value")]
		public float Dampening = 10.0f;

		private float currentZoom;
		
		protected virtual void OnEnable()
		{
			currentZoom = Zoom;
		}

		protected override void LateUpdate()
		{
			// Use the LateUpdate code from LeanCameraZoom
			base.LateUpdate();

			// Get t value
			var factor = LeanTouch.GetDampenFactor(Dampening, Time.deltaTime);

			// Lerp the current values to the target ones
			currentZoom = Mathf.Lerp(currentZoom, Zoom, factor);

			// Set the new zoom
			SetZoom(currentZoom);
		}
	}
}