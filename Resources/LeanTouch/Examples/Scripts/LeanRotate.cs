using UnityEngine;

namespace Lean.Touch
{
	// This script allows you to transform the current GameObject
	public class LeanRotate : MonoBehaviour
	{
		[Tooltip("Ignore fingers with StartedOverGui?")]
		public bool IgnoreGuiFingers;

		[Tooltip("Allows you to force rotation with a specific amount of fingers (0 = any)")]
		public int RequiredFingerCount;

		[Tooltip("Does rotation require an object to be selected?")]
		public LeanSelectable RequiredSelectable;

		[Tooltip("The camera we will be moving")]
		public Camera Camera;

		[Tooltip("The rotation axis used for non-relative rotations")]
		public Vector3 RotateAxis = Vector3.forward;

		[Tooltip("Should the rotation be performanced relative to the finger center?")]
		public bool Relative;

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
			// If we require a selectable and it isn't selected, cancel rotation
			if (RequiredSelectable != null && RequiredSelectable.IsSelected == false)
			{
				return;
			}

			// Get the fingers we want to use
			var fingers = LeanTouch.GetFingers(IgnoreGuiFingers, RequiredFingerCount);

			// Calculate the rotation values based on these fingers
			var center  = LeanGesture.GetScreenCenter(fingers);
			var degrees = LeanGesture.GetTwistDegrees(fingers);

			// Perform the rotation
			Rotate(center, degrees);
		}

		private void Rotate(Vector3 center, float degrees)
		{
			if (Relative == true)
			{
				// If camera is null, try and get the main camera, return true if a camera was found
				if (LeanTouch.GetCamera(ref Camera) == true)
				{
					// World position of the reference point
					var worldReferencePoint = Camera.ScreenToWorldPoint(center);
					
					// Rotate the transform around the world reference point
					transform.RotateAround(worldReferencePoint, Camera.transform.forward, degrees);
				}
			}
			else
			{
				transform.rotation *= Quaternion.AngleAxis(degrees, RotateAxis);
			}
		}
	}
}