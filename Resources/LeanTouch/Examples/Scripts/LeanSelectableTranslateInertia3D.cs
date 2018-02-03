using UnityEngine;

namespace Lean.Touch
{
	// This script allows you to transform the current GameObject with inertia
	[RequireComponent(typeof(Rigidbody))]
	public class LeanSelectableTranslateInertia3D : LeanSelectableBehaviour
	{
		[System.NonSerialized]
		private Rigidbody body;

		protected virtual void Update()
		{
			if (body == null)
			{
				body = GetComponent<Rigidbody>();
			}

			if (Selectable.IsSelected == true)
			{
				// Screen position of the transform
				var screenPosition = Camera.main.WorldToScreenPoint(transform.position);
			
				// Add the deltaPosition
				screenPosition += (Vector3)LeanGesture.GetScreenDelta();
			
				// Convert back to world space
				transform.position = Camera.main.ScreenToWorldPoint(screenPosition);

				// Reset velocity
				body.velocity = Vector3.zero;
			}
		}

		protected override void OnSelectUp(LeanFinger finger)
		{
			if (body == null)
			{
				body = GetComponent<Rigidbody>();
			}

			// Convert this GameObject's world position into screen coordinates and store it in a temp variable
			var screenPosition = Camera.main.WorldToScreenPoint(transform.position);
				
			// Modify screen position by the finger's delta screen position over the past 0.1 seconds
			screenPosition += (Vector3)finger.GetSnapshotScreenDelta(0.1f);
				
			// Convert the screen position into world coordinates and subtract it by the old position to find the world delta over the past 0.1 seconds
			var worldDelta = Camera.main.ScreenToWorldPoint(screenPosition) - transform.position;
				
			// Set the velocity and divide it by 0.1, because velocity is applied over 1 second, and our delta is currently only for 0.1 second
			body.velocity = worldDelta / 0.1f;
		}
	}
}